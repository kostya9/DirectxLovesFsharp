// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System
open WinInterop
open System.IO

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


    let path = Path.GetFullPath("shaders/shader.hlsl")
    //D3dInterop.D3DCompileFromFile(path, 0n, 0n, "VSMain", "vs_5_0", compileFlags, 0u, &vertexShader, &error)
    //D3dInterop.D3DCompileFromFile(path, 0n, 0n, "VSMain", "vs_5_0", compileFlags, 0u, &vertexShader, &error)
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