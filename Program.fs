﻿// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System
open WinInterop
open System.IO
open System.Text

type D3dPipeline = {
    IsDebug: bool;
    Device: D3dInterop.ID3D12Device;
}

let loadPipeline (window: Window) isDebug  =

    if isDebug then do
        let mutable debugLayer: D3dInterop.ID3D12Debug = null
        let mutable result = 0un
        D3dInterop.D3D12GetDebugInterface(typeof<D3dInterop.ID3D12Debug>.GUID, &debugLayer)
        if result <> 0un then do failwith $"Got result {result}"
        debugLayer.EnableDebugLayer()

    let mutable factory: D3dInterop.IDXGIFactory2 = null
    D3dInterop.CreateDXGIFactory2(0x01u, typeof<D3dInterop.IDXGIFactory2>.GUID, &factory)
    
    // factory.MakeWindowAssociation(window.Handle, 0x1u)

    let mutable idx = 0u
    let mutable adapter: D3dInterop.IDXGIAdapter = null
    while adapter = null do
        let result = factory.EnumAdapters(idx, &adapter)
        idx <- idx + 1u

        if result <> 0un then
            do failwith $"Got result {result}"

        if idx >= 1000u then
            do failwith "Could not find adapter in 1000 tries"

    let mutable device: D3dInterop.ID3D12Device = null
    D3dInterop.D3D12CreateDevice(null, D3dInterop.D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0, typeof<D3dInterop.ID3D12Device>.GUID, &device)

    let mutable desc = D3dInterop.D3D12_COMMAND_QUEUE_DESC()
    desc.Flags <- D3dInterop.D3D12_COMMAND_QUEUE_FLAGS.D3D12_COMMAND_QUEUE_FLAG_NONE
    desc.Type <-  D3dInterop.D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT

    let mutable commandQueue: D3dInterop.ID3D12CommandQueue = null
    device.CreateCommandQueue(&desc, typeof<D3dInterop.ID3D12CommandQueue>.GUID, &commandQueue)
    let mutable swapChainDesc = D3dInterop.DXGI_SWAP_CHAIN_DESC1()
    let frameCount = 2u
    let (width, height) = (800u, 600u)
    swapChainDesc.BufferCount <- frameCount;
    swapChainDesc.Width <- width
    swapChainDesc.Height <- height
    swapChainDesc.Format <- D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM
    swapChainDesc.BufferUsage <- D3dInterop.DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT
    swapChainDesc.SwapEffect <- D3dInterop.DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_DISCARD
    swapChainDesc.SampleDesc.Count <- 1u
    swapChainDesc.Flags <- 0u

    let mutable swapChain: D3dInterop.IDXGISwapChain1 = null
    factory.CreateSwapChainForHwnd(commandQueue, window.Handle, &swapChainDesc, 0L, null, &swapChain)

    let mutable heapDesc = D3dInterop.D3D12_DESCRIPTOR_HEAP_DESC()
    heapDesc.NumDescriptors <- frameCount
    heapDesc.Type <- D3dInterop.D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_RTV
    heapDesc.Flags <- D3dInterop.D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_NONE

    let mutable heap: D3dInterop.ID3D12DescriptorHeap = null
    device.CreateDescriptorHeap(&heapDesc, typeof<D3dInterop.ID3D12DescriptorHeap>.GUID, &heap)

    let rtvDescriptorSize = device.GetDescriptorHandleIncrementSize(D3dInterop.D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_RTV)

    let mutable rtvHandle = D3dInterop.D3D12_CPU_DESCRIPTOR_HANDLE()
    heap.GetCPUDescriptorHandleForHeapStart(&rtvHandle)
    
    let rtvDescriptors = 
        [|
            for i in 0u..frameCount - 1u ->
                let mutable shiftedRtcHandle = D3dInterop.D3D12_CPU_DESCRIPTOR_HANDLE()
                let offset = unativeint i * rtvDescriptorSize
                shiftedRtcHandle.ptr <- rtvHandle.ptr + nativeint offset

                let mutable buffer: D3dInterop.ID3D12Resource = null
                swapChain.GetBuffer(i, typeof<D3dInterop.ID3D12Resource>.GUID, &buffer)
                device.CreateRenderTargetView(buffer, 0n, rtvHandle)
                buffer
        |]

    let mutable allocator: D3dInterop.ID3D12CommandAllocator = null
    device.CreateCommandAllocator(D3dInterop.D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, typeof<D3dInterop.ID3D12CommandAllocator>.GUID, &allocator)

    { Device = device; IsDebug = true }

let loadAssets pipeline = 
    let mutable signatureDesc = D3dInterop.D3D12_ROOT_SIGNATURE_DESC()
    signatureDesc.Flags <- D3dInterop.D3D12_ROOT_SIGNATURE_FLAGS.D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT

    let mutable signature: D3dInterop.ID3DBlob = null
    let mutable errorBlob: D3dInterop.ID3DBlob = null

    D3dInterop.D3D12SerializeRootSignature(&signatureDesc, D3dInterop.D3D_ROOT_SIGNATURE_VERSION.D3D_ROOT_SIGNATURE_VERSION_1, &signature, &errorBlob)

    let bufferSize = signature.GetBufferSize()
    let bufferPointer = signature.GetBufferPointer()
    let mutable rootSignature: D3dInterop.ID3D12RootSignature = null
    pipeline.Device.CreateRootSignature(0u, bufferPointer, bufferSize, typeof<D3dInterop.ID3D12RootSignature>.GUID, &rootSignature)

    let mutable error: D3dInterop.ID3DBlob = null
    let mutable vertexShader: D3dInterop.ID3DBlob = null
    let mutable pixelShader: D3dInterop.ID3DBlob = null

    let compileFlags = 
        if pipeline.IsDebug 
        then (D3dInterop.D3DCOMPILE.D3DCOMPILE_DEBUG ||| D3dInterop.D3DCOMPILE.D3DCOMPILE_SKIP_OPTIMIZATION)
        else 0u

    let readShaderError (errorBlob: D3dInterop.ID3DBlob) = 
        let errStart = errorBlob.GetBufferPointer()
        let errSize = errorBlob.GetBufferSize() |> int

        let span = new ReadOnlySpan<byte>(NativeInterop.NativePtr.ofNativeInt<byte>(errStart) |> NativeInterop.NativePtr.toVoidPtr, errSize)
        let errText = Encoding.UTF8.GetString(span);

        Console.WriteLine(errText)

        errText

    let path = Path.GetFullPath("shaders/shader.hlsl")
    let err = D3dInterop.D3DCompileFromFile(path, 0n, 0n, "VSMain", "vs_5_0", compileFlags, 0u, &vertexShader, &error)

    if err <> 0un then do
        readShaderError error
        |> failwith

    let err = D3dInterop.D3DCompileFromFile(path, 0n, 0n, "PSMain", "ps_5_0", compileFlags, 0u, &pixelShader, &error)
    if err <> 0un then do
        readShaderError error
        |> failwith

    (*let mutable inputElementDescs: D3dInterop.D3D12_INPUT_ELEMENT_DESC[] = [|
            {   
                SemanticName = "POSITION";
                SemanticIndex = 0u;
                Format = D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT;
                InputSlot = 0u;
                AlignedByteOffset = 0u;
                InputSlotClass = D3dInterop.D3D12_INPUT_CLASSIFICATION.D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA;
                InstanceDataStepRate = 0u;
            }
            {   
                SemanticName = "COLOR";
                SemanticIndex = 0u;
                Format = D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT;
                InputSlot = 0u;
                AlignedByteOffset = 12u;
                InputSlotClass = D3dInterop.D3D12_INPUT_CLASSIFICATION.D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA;
                InstanceDataStepRate = 0u;
            }
        |]

    let getRasterizerState() = 
        let mutable rasterizerState = D3dInterop.D3D12_RASTERIZER_DESC()
        rasterizerState.FillMode <- D3dInterop.D3D12_FILL_MODE.D3D12_FILL_MODE_SOLID
        rasterizerState.CullMode <- D3dInterop.D3D12_CULL_MODE.D3D12_CULL_MODE_BACK
        rasterizerState.FrontCounterClockwise <- false
        rasterizerState.DepthBias <- 0
        rasterizerState.DepthBiasClamp <- 0.0
        rasterizerState.SlopeScaledDepthBias <- 0.0
        rasterizerState.DepthClipEnable <- true
        rasterizerState.MultisampleEnable <- false
        rasterizerState.AntialiasedLineEnable <- false
        rasterizerState.ForcedSampleCount <- 0u
        rasterizerState.ConservativeRaster <- D3dInterop.D3D12_CONSERVATIVE_RASTERIZATION_MODE.D3D12_CONSERVATIVE_RASTERIZATION_MODE_OFF
        rasterizerState

    let getBlendState() = 
        let mutable blendState = D3dInterop.D3D12_BLEND_DESC()
        blendState.AlphaToCoverageEnable <- false
        blendState.IndependentBlendEnable <- false

        let mutable defaultRenderTargetBlendDesc = D3dInterop.D3D12_RENDER_TARGET_BLEND_DESC()
        defaultRenderTargetBlendDesc.BlendEnable <- false
        defaultRenderTargetBlendDesc.LogicOpEnable <- false
        defaultRenderTargetBlendDesc.SrcBlend <- D3dInterop.D3D12_BLEND.D3D12_BLEND_ONE
        defaultRenderTargetBlendDesc.DestBlend <- D3dInterop.D3D12_BLEND.D3D12_BLEND_ZERO
        defaultRenderTargetBlendDesc.BlendOp <- D3dInterop.D3D12_BLEND_OP.D3D12_BLEND_OP_ADD
        defaultRenderTargetBlendDesc.SrcBlendAlpha <- D3dInterop.D3D12_BLEND.D3D12_BLEND_ONE
        defaultRenderTargetBlendDesc.DestBlendAlpha <- D3dInterop.D3D12_BLEND.D3D12_BLEND_ZERO
        defaultRenderTargetBlendDesc.BlendOpAlpha <- D3dInterop.D3D12_BLEND_OP.D3D12_BLEND_OP_ADD
        defaultRenderTargetBlendDesc.LogicOp <- D3dInterop.D3D12_LOGIC_OP.D3D12_LOGIC_OP_NOOP
        defaultRenderTargetBlendDesc.RenderTargetWriteMask <- 
            D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_ALPHA
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_BLUE
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_GREEN
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_ALPHA

        blendState.RenderTarget <- [|
            for i in 0..7 do
                defaultRenderTargetBlendDesc
        |]

        blendState

    use firstElPtr = fixed inputElementDescs.[0].SemanticName

    let mutable psoDesc = D3dInterop.D3D12_GRAPHICS_PIPELINE_STATE_DESC()
    psoDesc.InputLayout.pInputElementDescs <- NativeInterop.NativePtr.toNativeInt(firstElPtr)
    psoDesc.InputLayout.NumElements <- uint inputElementDescs.Length
    psoDesc.pRootSignature <- rootSignature
    psoDesc.VS.pShaderBytecode <- vertexShader.GetBufferPointer()
    psoDesc.VS.BytecodeLength <- vertexShader.GetBufferSize()
    psoDesc.PS.pShaderBytecode <- pixelShader.GetBufferPointer()
    psoDesc.PS.BytecodeLength <- pixelShader.GetBufferSize()
    psoDesc.RasterizerState <- getRasterizerState()
    psoDesc.BlendState <- getBlendState()
    psoDesc.DepthStencilState.DepthEnable <- false
    psoDesc.DepthStencilState.StencilEnable <- false
    psoDesc.SampleMask <- UInt32.MaxValue
    psoDesc.PrimitiveTopologyType <- D3dInterop.D3D12_PRIMITIVE_TOPOLOGY_TYPE.D3D12_PRIMITIVE_TOPOLOGY_TYPE_TRIANGLE
    psoDesc.NumRenderTargets <- 1u
    psoDesc.RTVFormats <- [| 
        for i in 0..7 do
            if i = 0 
            then D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM
            else D3dInterop.DXGI_FORMAT.DXGI_FORMAT_UNKNOWN
        |]
    
    psoDesc.SampleDesc.Count <- 1u

    let mutable pipelineState: D3dInterop.ID3D12PipelineState = null
    pipeline.Device.CreateGraphicsPipelineState(&psoDesc, typeof<D3dInterop.ID3D12PipelineState>.GUID, &pipelineState)*)

    ()
    

let init() =
    let window = WinInterop.makeWindow()

    let isDebug = true
    loadPipeline window isDebug
    |> loadAssets

    ()

let update() = 
    ()

let render() =
    ()

let destroy() = 
    ()

[<EntryPoint>]
let main argv =
    init()

    let mutable messages: WinInterop.External.MSG = WinInterop.External.MSG()
    // Run the message loop. It will run until GetMessage() returns 0
    while WinInterop.External.GetMessageA (&messages, 0n, 0un, 0un) do
        // Translate virtual-key messages into character messages 
        WinInterop.External.TranslateMessage &messages |> ignore
        // Send message to WindowProcedure
        WinInterop.External.DispatchMessageA &messages |> ignore

    Console.WriteLine("Press anything to exit...")
    Console.ReadKey() |> ignore

    0

// https://github.com/smourier/DirectN
// https://docs.microsoft.com/en-us/windows/win32/direct3d12/creating-a-basic-direct3d-12-component