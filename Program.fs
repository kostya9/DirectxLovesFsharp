// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System
open WinInterop
open System.IO
open System.Text
open System.Runtime.InteropServices
open System.Diagnostics
open System.Threading

type D3dPipeline = {
    IsDebug: bool;
    Device: D3dInterop.ID3D12Device;
    CommandAllocators: D3dInterop.ID3D12CommandAllocator[];
    CommandQueue: D3dInterop.ID3D12CommandQueue;
    SwapChain : D3dInterop.IDXGISwapChain3;
    RtvDescriptors: D3dInterop.ID3D12Resource[];
    RtvDescriptorSize: uint;
    Heap: D3dInterop.ID3D12DescriptorHeap;
    Factory: D3dInterop.IDXGIFactory2;
    DebugLayer: D3dInterop.ID3D12Debug;
} with override this.ToString() = "D3dPipeline"

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type Float3 = {
    x: single;
    y: single;
    z: single;
}

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type Float4 = {
    x: single;
    y: single;
    z: single;
    w: single;
}

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type Vertex = {
    position: Float3;
    color: Float4;
}

let (width, height) = (800u, 800u)

let aspectRatio = single width / single height
                       
let loadPipeline (window: Window) isDebug  =

    let mutable debugLayer: D3dInterop.ID3D12Debug = null
    if isDebug then do
        D3dInterop.D3D12GetDebugInterface(typeof<D3dInterop.ID3D12Debug>.GUID, &debugLayer)
        debugLayer.EnableDebugLayer()

    let mutable factory: D3dInterop.IDXGIFactory2 = null
    D3dInterop.CreateDXGIFactory2(0x01u, typeof<D3dInterop.IDXGIFactory2>.GUID, &factory)

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
    swapChainDesc.BufferCount <- frameCount;
    swapChainDesc.Width <- width
    swapChainDesc.Height <- height
    swapChainDesc.Format <- D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM
    swapChainDesc.BufferUsage <- D3dInterop.DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT
    swapChainDesc.SwapEffect <- D3dInterop.DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_DISCARD
    swapChainDesc.SampleDesc.Count <- 1u
    swapChainDesc.Flags <- 0u

    let mutable swapChain: D3dInterop.IDXGISwapChain3 = null
    factory.CreateSwapChainForHwnd(commandQueue, window.Handle, &swapChainDesc, 0L, null, &swapChain)

    factory.MakeWindowAssociation(window.Handle, 0x1u)

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
                let offset = i * rtvDescriptorSize
                shiftedRtcHandle.ptr <- rtvHandle.ptr + nativeint offset

                let mutable buffer: D3dInterop.ID3D12Resource = null
                swapChain.GetBuffer(i, typeof<D3dInterop.ID3D12Resource>.GUID, &buffer)
                device.CreateRenderTargetView(buffer, 0n, shiftedRtcHandle)

                let mutable allocator: D3dInterop.ID3D12CommandAllocator = null
                device.CreateCommandAllocator(D3dInterop.D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, typeof<D3dInterop.ID3D12CommandAllocator>.GUID, &allocator)
                (buffer, allocator)
        |]
    

    { 
        Device = device; 
        IsDebug = true;
        CommandAllocators = rtvDescriptors |> Array.map snd; 
        CommandQueue = commandQueue;
        SwapChain = swapChain;
        RtvDescriptors = rtvDescriptors |> Array.map fst;
        Heap = heap;
        Factory = factory;
        RtvDescriptorSize = rtvDescriptorSize;
        DebugLayer = debugLayer
    }

type D3dAssets = {
    Fence: D3dInterop.ID3D12Fence;
    FenceEvent: WinInterop.Handle;
    Pipeline: D3dPipeline;
    VertexBuffer: D3dInterop.ID3D12Resource;
    VertexShader: D3dInterop.ID3DBlob;
    VertexBufferView: D3dInterop.D3D12_VERTEX_BUFFER_VIEW;
    PixelShader: D3dInterop.ID3DBlob;
    PipelineState: D3dInterop.ID3D12PipelineState;
    CommandList: D3dInterop.ID3D12GraphicsCommandList;
    RootSignature: D3dInterop.ID3D12RootSignature;
} with override this.ToString() = "D3dAssets"

type D3dRenderingState = {
    Assets: D3dAssets;
    FenceValues: uint64[];
    FrameIdx: int;
} with override this.ToString() = "D3dRenderingState"

let loadAssets (pipeline): D3dAssets = 
    let mutable signatureDesc = D3dInterop.D3D12_ROOT_SIGNATURE_DESC()
    signatureDesc.Flags <- D3dInterop.D3D12_ROOT_SIGNATURE_FLAGS.D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT

    let mutable signature: D3dInterop.ID3DBlob = null
    let mutable errorBlob: D3dInterop.ID3DBlob = null

    D3dInterop.D3D12SerializeRootSignature(&signatureDesc, D3dInterop.D3D_ROOT_SIGNATURE_VERSION.D3D_ROOT_SIGNATURE_VERSION_1, &signature, &errorBlob)

    let bufferSize = signature.GetBufferSize()
    let bufferPointer = signature.GetBufferPointer()
    let mutable rootSignature: D3dInterop.ID3D12RootSignature = null
    pipeline.Device.CreateRootSignature(0u, bufferPointer, bufferSize, typeof<D3dInterop.ID3D12RootSignature>.GUID, &rootSignature)
    Marshal.ReleaseComObject(signature) |> ignore

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

        let span = new ReadOnlySpan<byte>(errStart.ToPointer(), errSize)
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

    let mutable inputElementDescs: D3dInterop.D3D12_INPUT_ELEMENT_DESC[] = [|
            D3dInterop.D3D12_INPUT_ELEMENT_DESC(
                SemanticName = "POSITION",
                SemanticIndex = 0u,
                Format = D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT,
                InputSlot = 0u,
                AlignedByteOffset = 0u,
                InputSlotClass = D3dInterop.D3D12_INPUT_CLASSIFICATION.D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA,
                InstanceDataStepRate = 0u
            )
            D3dInterop.D3D12_INPUT_ELEMENT_DESC(   
                SemanticName = "COLOR",
                SemanticIndex = 0u,
                Format = D3dInterop.DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT,
                InputSlot = 0u,
                AlignedByteOffset = 12u,
                InputSlotClass = D3dInterop.D3D12_INPUT_CLASSIFICATION.D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA,
                InstanceDataStepRate = 0u
            )
        |]

    let getRasterizerState() = 
        let mutable rasterizerState = D3dInterop.D3D12_RASTERIZER_DESC()
        rasterizerState.FillMode <- D3dInterop.D3D12_FILL_MODE.D3D12_FILL_MODE_SOLID
        rasterizerState.CullMode <- D3dInterop.D3D12_CULL_MODE.D3D12_CULL_MODE_BACK
        rasterizerState.FrontCounterClockwise <- false
        rasterizerState.DepthBias <- 0
        rasterizerState.DepthBiasClamp <- 0.0f
        rasterizerState.SlopeScaledDepthBias <- 0.0f
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
            D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_RED
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_BLUE
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_GREEN
            ||| D3dInterop.D3D12_COLOR_WRITE_ENABLE.D3D12_COLOR_WRITE_ENABLE_ALPHA

        blendState.RenderTarget <- [|
            for _ in 0..7 do
                defaultRenderTargetBlendDesc
        |]

        blendState

    // Since we cant marshal the whole array (or idk how to) implicitly as a pointer
    // We marshal it manually to native memory
    let structSize = Marshal.SizeOf(inputElementDescs.[0])
    let marshalledStructLocation = Marshal.AllocHGlobal(2 * structSize)
    Marshal.StructureToPtr(inputElementDescs.[0], marshalledStructLocation, false)
    Marshal.StructureToPtr(inputElementDescs.[1], marshalledStructLocation + nativeint structSize, false)

    let rootSignaturePtr = Marshal.GetComInterfaceForObject<D3dInterop.ID3D12RootSignature, D3dInterop.ID3D12RootSignature>(rootSignature)
    let mutable psoDesc = D3dInterop.D3D12_GRAPHICS_PIPELINE_STATE_DESC()
    psoDesc.InputLayout.pInputElementDescs <- marshalledStructLocation
    psoDesc.InputLayout.NumElements <- uint inputElementDescs.Length
    psoDesc.pRootSignature <- rootSignaturePtr
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
    let guid = typeof<D3dInterop.ID3D12PipelineState>.GUID
    let device = pipeline.Device
    device.CreateGraphicsPipelineState(&psoDesc, guid, &pipelineState)

    Marshal.FreeHGlobal(marshalledStructLocation)

    let mutable commandList: D3dInterop.ID3D12GraphicsCommandList = null
    device.CreateCommandList(0u, 
        D3dInterop.D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, 
        pipeline.CommandAllocators.[0],
        pipelineState,
        typeof<D3dInterop.ID3D12GraphicsCommandList>.GUID,
        &commandList)

    // CommandList is created in open state, so we reset it
    commandList.Close()

    let vertices: Vertex[] = [|
        {
            position = { x = 0f; y = 0.25f; z = 0f };
            color = { x = 1f; y = 0f; z = 0f; w = 1f }
        }
        {
            position = { x = 0.25f; y = -0.25f; z = 0f };
            color = { x = 0f; y = 1f; z = 0f; w = 1f }
        }
        {
            position = { x = -0.25f; y = -0.25f; z = 0f };
            color = { x = 0f; y = 0f; z = 1f; w = 1f }
        }
    |]
    let verticesSize = sizeof<Vertex> * vertices.Length

    let mutable props = D3dInterop.D3D12_HEAP_PROPERTIES()
    props.CPUPageProperty <- D3dInterop.D3D12_CPU_PAGE_PROPERTY.D3D12_CPU_PAGE_PROPERTY_UNKNOWN
    props.MemoryPoolPreference <- D3dInterop.D3D12_MEMORY_POOL.D3D12_MEMORY_POOL_UNKNOWN
    props.Type <- D3dInterop.D3D12_HEAP_TYPE.D3D12_HEAP_TYPE_UPLOAD
    props.CreationNodeMask <- 1u
    props.VisibleNodeMask <- 1u

    let mutable resourceDesc = D3dInterop.D3D12_RESOURCE_DESC()
    resourceDesc.Dimension <- D3dInterop.D3D12_RESOURCE_DIMENSION.D3D12_RESOURCE_DIMENSION_BUFFER
    resourceDesc.Alignment <- 0UL
    resourceDesc.Width <- verticesSize |> uint64
    resourceDesc.Height <- 1u
    resourceDesc.DepthOrArraySize <- 1us
    resourceDesc.MipLevels <- 1us
    resourceDesc.Format <- D3dInterop.DXGI_FORMAT.DXGI_FORMAT_UNKNOWN
    resourceDesc.SampleDesc.Count <- 1u
    resourceDesc.SampleDesc.Quality <- 0u
    resourceDesc.Layout <- D3dInterop.D3D12_TEXTURE_LAYOUT.D3D12_TEXTURE_LAYOUT_ROW_MAJOR
    resourceDesc.Flags <- D3dInterop.D3D12_RESOURCE_FLAGS.D3D12_RESOURCE_FLAG_NONE

    let mutable vertexBuffer: D3dInterop.ID3D12Resource = null
    device.CreateCommittedResource(&props, 
        D3dInterop.D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_NONE,
        &resourceDesc,
        D3dInterop.KnownResourceStates.D3D12_RESOURCE_STATE_GENERIC_READ,
        0n,
        typeof<D3dInterop.ID3D12Resource>.GUID,
        &vertexBuffer)

    let mutable readRange = D3dInterop.D3D12_RANGE()
    readRange.Begin <- 0un
    readRange.End <- 0un

    let mutable pVertexDataBegin = Unchecked.defaultof<voidptr>
    vertexBuffer.Map(0u, &readRange, &pVertexDataBegin)

    let verticesDataSpan = new Span<Vertex>(pVertexDataBegin, vertices.Length)
    vertices.CopyTo(verticesDataSpan)

    vertexBuffer.Unmap(0u, 0n)

    let mutable vertexBufferView = D3dInterop.D3D12_VERTEX_BUFFER_VIEW()
    vertexBufferView.BufferLocation <- vertexBuffer.GetGPUVirtualAddress()
    vertexBufferView.SizeInBytes <- verticesSize |> uint
    vertexBufferView.StrideInBytes <- sizeof<Vertex> |> uint

    let mutable fence: D3dInterop.ID3D12Fence = null
    device.CreateFence(0UL, 
        D3dInterop.D3D12_FENCE_FLAGS.D3D12_FENCE_FLAG_NONE,
        typeof<D3dInterop.ID3D12Fence>.GUID,
        &fence)

    let fenceEvent = WinInterop.External.CreateEventW(null, false, false, null)

    {
        VertexBufferView = vertexBufferView;
        VertexBuffer = vertexBuffer;
        VertexShader = vertexShader;
        PixelShader = pixelShader;
        Pipeline = pipeline; 
        Fence = fence;
        FenceEvent = fenceEvent;
        PipelineState = pipelineState;
        CommandList = commandList;
        RootSignature = rootSignature;
    }
   

let infiniteTimeout = 0xFFFFFFFFu

let fillTriangle (span: Span<Vertex>) (triangle: Game.Triangle) =
    span.[0] <- { 
        position = { x = triangle.Top.X; y = triangle.Top.Y; z = 0.0f }; 
        color = { x = 1f; y = 0f; z = 0f; w = 1f } 
    }

    span.[1] <- { 
        position = { x = triangle.Left.X; y = triangle.Left.Y; z = 0.0f }; 
        color = { x = 0f; y = 1f; z = 0f; w = 1f }
    }

    span.[2] <- { 
        position = { x = triangle.Right.X; y = triangle.Right.Y; z = 0.0f }; 
        color = { x = 0f; y = 0f; z = 1f; w = 1f }
    }

let uploadTriangle (vertexBuffer: D3dInterop.ID3D12Resource) triangle = 
    let verticesSize = sizeof<Vertex> * 3
    let verticesPtr = NativeInterop.NativePtr.stackalloc<byte> verticesSize
    let verticesSpan = Span<Vertex>(verticesPtr |> NativeInterop.NativePtr.toVoidPtr, 3)
    fillTriangle verticesSpan triangle

    let mutable readRange = D3dInterop.D3D12_RANGE()
    readRange.Begin <- 0un
    readRange.End <- 0un

    let mutable pVertexDataBegin = Unchecked.defaultof<voidptr>
    vertexBuffer.Map(0u, &readRange, &pVertexDataBegin)

    let verticesDataSpan = new Span<Vertex>(pVertexDataBegin, verticesSpan.Length)
    verticesSpan.CopyTo(verticesDataSpan)

    vertexBuffer.Unmap(0u, 0n)

let populateCommandList renderState (gameState: Game.GameState) = 
    uploadTriangle renderState.Assets.VertexBuffer gameState.Triangle

    let commandAllocator = renderState.Assets.Pipeline.CommandAllocators.[renderState.FrameIdx]
    commandAllocator.Reset()

    let pso = renderState.Assets.PipelineState
    let commandList = renderState.Assets.CommandList
    commandList.Reset(commandAllocator, pso)

    commandList.SetGraphicsRootSignature(renderState.Assets.RootSignature)

    let mutable viewPort = D3dInterop.D3D12_VIEWPORT()
    viewPort.Width <- width |> single
    viewPort.Height <- height |> single
    viewPort.MinDepth <- 0.0f
    viewPort.MaxDepth <- 1.0f
    commandList.RSSetViewports(1u, &viewPort)

    let mutable scissorRect = D3dInterop.D3D12_RECT()
    scissorRect.right <- width |> int
    scissorRect.bottom <- height |> int
    commandList.RSSetScissorRects(1u, &scissorRect)

    let frameIdx = renderState.FrameIdx
    let renderTarget = renderState.Assets.Pipeline.RtvDescriptors.[frameIdx]
    let renderTargetPtr = Marshal.GetComInterfaceForObject<D3dInterop.ID3D12Resource, D3dInterop.ID3D12Resource>(renderTarget)

    let mutable startBarrier = D3dInterop.D3D12_RESOURCE_BARRIER()
    startBarrier.Type <- D3dInterop.D3D12_RESOURCE_BARRIER_TYPE.D3D12_RESOURCE_BARRIER_TYPE_TRANSITION
    startBarrier.Flags <- D3dInterop.D3D12_RESOURCE_BARRIER_FLAGS.D3D12_RESOURCE_BARRIER_FLAG_NONE
    startBarrier.Union.Transition.pResource <- renderTargetPtr
    startBarrier.Union.Transition.StateBefore <- D3dInterop.D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT
    startBarrier.Union.Transition.StateAfter <- D3dInterop.D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET

    let allSubresources = 0xffffffffu
    startBarrier.Union.Transition.Subresource <- allSubresources
    commandList.ResourceBarrier(1u, &startBarrier)

    let heap = renderState.Assets.Pipeline.Heap
    let mutable rtvHandle = D3dInterop.D3D12_CPU_DESCRIPTOR_HANDLE()
    heap.GetCPUDescriptorHandleForHeapStart(&rtvHandle)

    let rtvDescriptorSize = renderState.Assets.Pipeline.RtvDescriptorSize
    let offset = uint frameIdx * rtvDescriptorSize
    rtvHandle.ptr <- rtvHandle.ptr + nativeint offset
    commandList.OMSetRenderTargets(1u, &rtvHandle, false, 0n)

    let clearColor = [| 0.0f; 0.2f; 0.4f; 1.0f |]
    commandList.ClearRenderTargetView(rtvHandle, clearColor, 0u, 0n)

    commandList.IASetPrimitiveTopology(D3dInterop.D3D12_PRIMITIVE_TOPOLOGY.D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST)
    commandList.IASetVertexBuffers(0u, 1u, &renderState.Assets.VertexBufferView)
    commandList.DrawInstanced(3u, 1u, 0u, 0u)

    let mutable endBarrier = D3dInterop.D3D12_RESOURCE_BARRIER()
    endBarrier.Type <- D3dInterop.D3D12_RESOURCE_BARRIER_TYPE.D3D12_RESOURCE_BARRIER_TYPE_TRANSITION
    endBarrier.Flags <- D3dInterop.D3D12_RESOURCE_BARRIER_FLAGS.D3D12_RESOURCE_BARRIER_FLAG_NONE
    endBarrier.Union.Transition.pResource <- renderTargetPtr
    endBarrier.Union.Transition.StateBefore <- D3dInterop.D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET
    endBarrier.Union.Transition.StateAfter <- D3dInterop.D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT

    let allSubresources = 0xffffffffu
    endBarrier.Union.Transition.Subresource <- allSubresources
    commandList.ResourceBarrier(1u, &endBarrier)

    commandList.Close()

    ()

let waitForGpu renderState = 
    let fence = renderState.Assets.Fence
    let fenceValue = renderState.FenceValues.[renderState.FrameIdx]
    let commandQueue = renderState.Assets.Pipeline.CommandQueue
    commandQueue.Signal(fence, fenceValue)

    let fenceEvent = renderState.Assets.FenceEvent
    fence.SetEventOnCompletion(fenceValue, fenceEvent)
    WinInterop.External.WaitForSingleObjectEx(fenceEvent, infiniteTimeout, false) |> ignore

    renderState.FenceValues.[renderState.FrameIdx] <- fenceValue + 1UL

    renderState


let moveToNextFrame renderState = 
    let fence = renderState.Assets.Fence
    let fenceValue = renderState.FenceValues.[renderState.FrameIdx]
    let commandQueue = renderState.Assets.Pipeline.CommandQueue
    commandQueue.Signal(fence, fenceValue)

    let swapChain = renderState.Assets.Pipeline.SwapChain
    let nextFrameIdx = swapChain.GetCurrentBackBufferIndex() |> int

    let nextFenceValue = renderState.FenceValues.[nextFrameIdx]
    if fence.GetCompletedValue() < nextFenceValue then do
        let fenceEvent = renderState.Assets.FenceEvent
        fence.SetEventOnCompletion(nextFenceValue, fenceEvent)
        WinInterop.External.WaitForSingleObjectEx(fenceEvent, infiniteTimeout, false) |> ignore

    renderState.FenceValues.[nextFrameIdx] <- fenceValue + 1UL

    { renderState with FrameIdx = nextFrameIdx }

type WindowsCallbackState =
    val mutable renderingState: ValueOption<D3dRenderingState>
    val mutable gameState: Game.GameState

    new(gameState) = { renderingState = ValueNone; gameState = gameState }

let update msTimeElapsed oldState =
    Game.update oldState msTimeElapsed


let render gameState renderState =
    populateCommandList renderState gameState

    // Execute the command list.
    let commandList = renderState.Assets.CommandList
    let commandList: D3dInterop.ID3D12CommandList[] = [| commandList |]

    let commandQueue = renderState.Assets.Pipeline.CommandQueue
    commandQueue.ExecuteCommandLists(1u, commandList);

    // Present the frame.
    renderState.Assets.Pipeline.SwapChain.Present(1u, 0u)
    moveToNextFrame(renderState)

let destroy(assets) = 
    ()

let (|Field|_|) field x = if field = x then Some () else None

let mutable running = true
let windowCallback (callbackState: WindowsCallbackState) (hwnd: nativeint) (uMsg: uint) (wParam: unativeint) (lParam: nativeint) =
    //Console.WriteLine("Got " + string uMsg)

    match uMsg with
    | Field WinInterop.WindowMsgType.WM_CREATE ->
        0n

    | Field WinInterop.WindowMsgType.WM_DESTROY
    | Field WinInterop.WindowMsgType.WM_CLOSE
    | Field WinInterop.WindowMsgType.WM_QUIT ->
        running <- false

        0n
    | Field WinInterop.WindowMsgType.WM_PAINT ->

        // We don't need to paint anything, but this maked windows happy
        let mutable ps = WinInterop.External.PAINTSTRUCT()
        let hdc = WinInterop.External.BeginPaint(hwnd, &ps)
        WinInterop.External.EndPaint(hwnd, &ps) |> ignore
            

        0n
    | _ -> External.DefWindowProcA(hwnd, uMsg, wParam, lParam)

let mkWindowWithProc state =
    let callbackDelegate = WinInterop.WindowProc(windowCallback state)
    WinInterop.makeWindow callbackDelegate

let initRendering window (renderState: WindowsCallbackState) =
    let mkRenderState assets =
        let frameIdx = assets.Pipeline.SwapChain.GetCurrentBackBufferIndex() |> int
        let fenceValues = [| 0UL; 0UL |]

        fenceValues.[frameIdx] <- 1UL 

        { Assets = assets; FenceValues = fenceValues; FrameIdx = frameIdx }

    let isDebug = true
    renderState.renderingState <-
        loadPipeline window isDebug
        |> loadAssets
        |> mkRenderState
        |> render renderState.gameState
        |> ValueSome

    WinInterop.showWindow window

    renderState

[<EntryPoint>]
let main argv =
    let state = WindowsCallbackState(Game.mkGameState())
    let window = mkWindowWithProc(state)

    let targetFps = 60.0
    let targetMsPerFrame = 1000.0 / targetFps

    let state = initRendering window state

    // Thanks @handmadehero for this trick!
    // This sets scheduler granularity to 1 ms, sets us up for rendering at correct FPS
    // And not melting the CPU by using Thread.Sleep, and waking up at the perfect time
    let result = WinInterop.External.timeBeginPeriod(1un)
    if result <> WinInterop.External.winMmmNoError then do
        Console.WriteLine("Could not set scheduler frequency")
        running <- false

    let mutable messages: WinInterop.External.MSG = WinInterop.External.MSG()
    let sw = Stopwatch.StartNew()
    while running do
        
        let mutable handledMessages = 0
        // Get all messages remaining in queue
        while WinInterop.External.PeekMessageW (&messages, 0n, 0u, 0u, 0x0001u) <> 0 do
            let uMsg = messages.message

            match uMsg with
            | Field WinInterop.WindowMsgType.WM_DESTROY
            | Field WinInterop.WindowMsgType.WM_QUIT
            | Field WinInterop.WindowMsgType.WM_CLOSE ->
                running <- false

            | _ -> // Translate virtual-key messages into character messages 
                WinInterop.External.TranslateMessage &messages |> ignore
                // Send message to WindowProcedure
                WinInterop.External.DispatchMessageW &messages |> ignore

            handledMessages <- handledMessages + 1

        state.gameState <- 
            state.gameState
            |> update targetMsPerFrame

        state.renderingState <- 
            state.renderingState
            |> ValueOption.map (fun r -> render state.gameState r)

        let timeForUpdateAndRender = sw.Elapsed;

        let delta = targetMsPerFrame - sw.Elapsed.TotalMilliseconds
        if delta > 0.0 then do
            
            let toSleep = int delta - 1

            // Give up CPU until we actually need it
            if toSleep > 0 then do
                Thread.Sleep(toSleep)

            // Lets wait all those microseconds that thread sleep did not handle
            while targetMsPerFrame - sw.Elapsed.TotalMilliseconds > 0.0 do
                ()

        // We are starting to count next frame time from here
        // We want out measurements to be accurate, and we are sure that now is the time we are aiming for
        let elapsedInLastFrame = sw.Elapsed
        sw.Restart()

        Console.WriteLine($"""Elapsed in loop: {elapsedInLastFrame.TotalMilliseconds.ToString("0.00")}ms. Needed for loop: {timeForUpdateAndRender.TotalMilliseconds.ToString("0.00")}. Handled msgs: {handledMessages}""")

    Console.WriteLine("Exiting...")

    0

// https://github.com/smourier/DirectN
// https://docs.microsoft.com/en-us/windows/win32/direct3d12/creating-a-basic-direct3d-12-component