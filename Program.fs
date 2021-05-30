// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System
open System.Runtime.InteropServices;

module D3dInterop =
    type DXGI_GPU_PREFERENCE =
        | DXGI_GPU_PREFERENCE_UNSPECIFIED = 0
        | DXGI_GPU_PREFERENCE_MINIMUM_POWER = 1
        | DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE = 2

    type D3D_FEATURE_LEVEL =
        | D3D_FEATURE_LEVEL_1_0_CORE = 0x1000
        | D3D_FEATURE_LEVEL_9_1 = 0x9100
        | D3D_FEATURE_LEVEL_9_2 = 0x9200
        | D3D_FEATURE_LEVEL_9_3 = 0x9300
        | D3D_FEATURE_LEVEL_10_0 = 0xa000
        | D3D_FEATURE_LEVEL_10_1 = 0xa100
        | D3D_FEATURE_LEVEL_11_0 = 0xb000
        | D3D_FEATURE_LEVEL_11_1 = 0xb100
        | D3D_FEATURE_LEVEL_12_0 = 0xc000
        | D3D_FEATURE_LEVEL_12_1 = 0xc100
        | D3D_FEATURE_LEVEL_12_2 = 0xc200

    type D3D12_COMMAND_LIST_TYPE = 
        | D3D12_COMMAND_LIST_TYPE_DIRECT = 0
        | D3D12_COMMAND_LIST_TYPE_BUNDLE = 1
        | D3D12_COMMAND_LIST_TYPE_COMPUTE = 2
        | D3D12_COMMAND_LIST_TYPE_COPY = 3
        | D3D12_COMMAND_LIST_TYPE_VIDEO_DECODE = 4
        | D3D12_COMMAND_LIST_TYPE_VIDEO_PROCESS = 5
        | D3D12_COMMAND_LIST_TYPE_VIDEO_ENCODE = 6
    
    [<Flags>]
    type D3D12_COMMAND_QUEUE_FLAGS = 
        | D3D12_COMMAND_QUEUE_FLAG_NONE = 0x00000000
        | D3D12_COMMAND_QUEUE_FLAG_DISABLE_GPU_TIMEOUT = 0x00000001

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type D3D12_COMMAND_QUEUE_DESC =
        val mutable Type: D3D12_COMMAND_LIST_TYPE
        val mutable Priority: int
        val mutable Flags: D3D12_COMMAND_QUEUE_FLAGS
        val mutable NodeMask: uint

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type DXGI_RATIONAL =
        val mutable Numerator: uint
        val mutable Denominator: uint

    type DXGI_FORMAT = 
        | DXGI_FORMAT_R8G8B8A8_UNORM = 28
        | DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29

    type DXGI_MODE_SCANLINE_ORDER =
        | DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED = 0
        | DXGI_MODE_SCANLINE_ORDER_PROGRESSIVE = 1
        | DXGI_MODE_SCANLINE_ORDER_UPPER_FIELD_FIRST = 2
        | DXGI_MODE_SCANLINE_ORDER_LOWER_FIELD_FIRST = 3

    type DXGI_MODE_SCALING = 
        | DXGI_MODE_SCALING_UNSPECIFIED = 0
        | DXGI_MODE_SCALING_CENTERED = 1
        | DXGI_MODE_SCALING_STRETCHED = 2

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type DXGI_MODE_DESC =
        val mutable Width: uint
        val mutable Height: uint
        val mutable RefreshRate: DXGI_RATIONAL
        val mutable Format: DXGI_FORMAT
        val mutable ScanlineOrdering: DXGI_MODE_SCANLINE_ORDER
        val mutable Scaling: DXGI_MODE_SCALING

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type DXGI_SAMPLE_DESC = 
        val mutable Count: uint
        val mutable Quality: uint

    [<Flags>]
    type DXGI_USAGE = 
        | DXGI_USAGE_SHADER_INPUT = 0x00000010u
        | DXGI_USAGE_RENDER_TARGET_OUTPUT = 0x00000020u
        | DXGI_USAGE_BACK_BUFFER = 0x00000040u
        | DXGI_USAGE_SHARED = 0x00000080u
        | DXGI_USAGE_READ_ONLY = 0x00000100u
        | DXGI_USAGE_DISCARD_ON_PRESENT = 0x00000200u
        | DXGI_USAGE_UNORDERED_ACCESS = 0x00000400u

    type DXGI_SWAP_EFFECT =
        | DXGI_SWAP_EFFECT_DISCARD = 0
        | DXGI_SWAP_EFFECT_SEQUENTIAL = 1
        | DXGI_SWAP_EFFECT_FLIP_SEQUENTIAL = 3
        | DXGI_SWAP_EFFECT_FLIP_DISCARD = 4

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type DXGI_SWAP_CHAIN_DESC =
        val mutable BufferDesc: DXGI_MODE_DESC
        val mutable SampleDesc: DXGI_SAMPLE_DESC
        val mutable BufferUsage: DXGI_USAGE
        val mutable BufferCount: uint
        val mutable OutputWindow: nativeint
        val mutable Windowed: bool
        val mutable SwapEffect: DXGI_SWAP_EFFECT
        val mutable Flags: uint

    type DXGI_SCALING = 
    | DXGI_SCALING_STRETCH = 0
    | DXGI_SCALING_NONE = 1
    | DXGI_SCALING_ASPECT_RATIO_STRETCH = 2

    type DXGI_ALPHA_MODE =
    | DXGI_ALPHA_MODE_UNSPECIFIED = 0
    | DXGI_ALPHA_MODE_PREMULTIPLIED=  1
    | DXGI_ALPHA_MODE_STRAIGHT = 2
    | DXGI_ALPHA_MODE_IGNORE = 3
    | DXGI_ALPHA_MODE_FORCE_DWORD = 0xffffffff

    [<StructLayout(LayoutKind.Sequential)>]
    [<Struct>]
    type DXGI_SWAP_CHAIN_DESC1 =
        val mutable Width: uint
        val mutable Height: uint
        val mutable Format: DXGI_FORMAT
        val mutable Stereo: bool
        val mutable SampleDesc: DXGI_SAMPLE_DESC
        val mutable BufferUsage: DXGI_USAGE
        val mutable BufferCount: uint
        val mutable Scaling: DXGI_SCALING
        val mutable SwapEffect: DXGI_SWAP_EFFECT
        val mutable AlphaMode: DXGI_ALPHA_MODE
        val mutable Flags: uint

    [<AllowNullLiteral>]
    [<Guid("344488b7-6846-474b-b989-f027448245e0")>]
    [<ComImport>]
    type ID3D12Debug =

        [<PreserveSig>]
        abstract member EnableDebugLayer: unit -> unit

    [<AllowNullLiteral>]
    [<Guid("2411e7e1-12ac-4ccf-bd14-9798e8534dc0")>]
    [<ComImport>]
    type IDXGIAdapter = interface end

    [<AllowNullLiteral>]
    [<Guid("0ec870a6-5d7e-4c22-8cfc-5baae07616ed")>]
    [<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
    type ID3D12CommandQueue = interface end

    [<AllowNullLiteral>]
    [<Guid("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a")>]
    [<ComImport>]
    type IDXGISwapChain = interface end

    [<AllowNullLiteral>]
    [<Guid("790a45f7-0d42-4876-983a-0a55cfe6f4aa")>]
    [<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
    type IDXGISwapChain1 = interface end

    // Well, at some point I will probably need to specify these
    type D3D12_GRAPHICS_PIPELINE_STATE_DESC = unit
    type D3D12_COMPUTE_PIPELINE_STATE_DESC = unit
    type ID3D12CommandAllocator = unit
    type ID3D12PipelineState = unit
    type D3D12_FEATURE = unit

    type D3D12_DESCRIPTOR_HEAP_TYPE = 
        | D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV = 0
        | D3D12_DESCRIPTOR_HEAP_TYPE_SAMPLER = 1
        | D3D12_DESCRIPTOR_HEAP_TYPE_RTV = 2
        | D3D12_DESCRIPTOR_HEAP_TYPE_DSV = 3
        | D3D12_DESCRIPTOR_HEAP_TYPE_NUM_TYPES = 4

    [<Flags>]
    type D3D12_DESCRIPTOR_HEAP_FLAGS = 
        | D3D12_DESCRIPTOR_HEAP_FLAG_NONE = 0x00000000
        | D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE = 0x00000001

    [<Struct>]
    [<StructLayout(LayoutKind.Sequential)>]
    type D3D12_DESCRIPTOR_HEAP_DESC =
        val mutable Type: D3D12_DESCRIPTOR_HEAP_TYPE
        val mutable NumDescriptors: uint
        val mutable Flags: D3D12_DESCRIPTOR_HEAP_FLAGS
        val mutable NodeMask: uint

    [<Struct>]
    [<StructLayout(LayoutKind.Sequential)>]
    type D3D12_CPU_DESCRIPTOR_HANDLE = 
        val mutable ptr: nativeint

        (*member this.Offset (offsetScaledByIncrementSize: int) = 
            this.ptr <- this.ptr + unativeint offsetScaledByIncrementSize
            ()*)


    [<Struct>]
    [<StructLayout(LayoutKind.Sequential)>]
    type D3D12_GPU_DESCRIPTOR_HANDLE = 
        val mutable ptr: unativeint

    [<AllowNullLiteral>]
    [<Guid("8efb471d-616c-4f49-90f7-127bb763fa51")>]
    [<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
    type ID3D12DescriptorHeap =
        abstract member SetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid *
            DataSize: uint * 
            pData: IntPtr
                -> unit

        abstract member SetPrivateDataInterface:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid  *
            pUnknown: IntPtr
                -> unit

        abstract member GetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid * 
            pDataSize: byref<uint> * 
            pData: nativeint
                -> unit

        abstract member GetParent:
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
            [<MarshalAs(UnmanagedType.IUnknown)>] ppParent: byref<Object> 
                -> unit

        abstract member GetDevice: 
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppvDevice: byref<Object>
                -> unit

        abstract member GetDesc: unit -> D3D12_DESCRIPTOR_HEAP_DESC
       
        [<PreserveSig>]
        abstract member GetCPUDescriptorHandleForHeapStart: byref<D3D12_CPU_DESCRIPTOR_HANDLE> -> unit
        
        [<PreserveSig>]
        abstract member GetGPUDescriptorHandleForHeapStart: unit -> D3D12_GPU_DESCRIPTOR_HANDLE

    [<AllowNullLiteral>]
    [<Guid("189819f1-1db6-4b57-be54-1821339b85f7")>]
    [<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
    type ID3D12Device =
        abstract member SetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid *DataSize: uint * pData: IntPtr
                -> unit

        abstract member SetPrivateDataInterface:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid  *pUnknown: IntPtr
                -> unit

        abstract member GetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid * pDataSize: byref<uint> * pData: nativeint
                -> unit

        abstract member GetParent:
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * [<MarshalAs(UnmanagedType.IUnknown)>] ppParent: byref<Object> 
                -> unit

        abstract member GetNodeCount: unit -> uint

        abstract member CreateCommandQueue:
            pDesc: byref<D3D12_COMMAND_QUEUE_DESC> *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            ppCommandQueue: byref<ID3D12CommandQueue>
                -> unit

        abstract member CreateCommandAllocator: 
            ``type``: D3D12_COMMAND_LIST_TYPE *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppCommandAllocator: byref<Object>
                -> unit

        abstract member CreateGraphicsPipelineState:
            pDesc: byref<D3D12_GRAPHICS_PIPELINE_STATE_DESC> *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppPipelineState: byref<Object>
                -> unit

        abstract member CreateComputePipelineState:
            pDesc: byref<D3D12_COMPUTE_PIPELINE_STATE_DESC> *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppPipelineState: byref<Object>
                -> unit

        abstract member CreateCommandList:
            nodeMask: uint *
            ``type``: D3D12_COMMAND_LIST_TYPE *
            pCommandAllocator: byref<ID3D12CommandAllocator> *
            pInitialState: byref<ID3D12PipelineState> *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppCommandList: byref<Object>
                -> unit

        abstract member CheckFeatureSupport:
            Feature: D3D12_FEATURE *
            pFeatureSupportData: nativeint *
            FeatureSupportDataSize: uint
                -> unit

        abstract member CreateDescriptorHeap:
            pDescriptorHeapDesc: byref<D3D12_DESCRIPTOR_HEAP_DESC> *
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
            ppvHeap: byref<ID3D12DescriptorHeap>
                -> unit

        abstract member GetDescriptorHandleIncrementSize:
            DescriptorHeapType: D3D12_DESCRIPTOR_HEAP_TYPE
                -> unit

    [<AllowNullLiteral>]
    type IDXGIOutput = interface end

    [<AllowNullLiteral>]
    [<Guid("50c83a1c-e072-4c48-87b0-3630fa36a6d0")>]
    [<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
    type IDXGIFactory2 =
        abstract member SetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid *
            DataSize: uint * 
            pData: IntPtr
              -> unit

        abstract member SetPrivateDataInterface:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid  *
            pUnknown: IntPtr
                -> unit

        abstract member GetPrivateData:
            [<MarshalAs(UnmanagedType.LPStruct)>] Name: Guid * 
            pDataSize: byref<uint> * pData: nativeint
                -> unit

        abstract member GetParent:
            [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
            [<MarshalAs(UnmanagedType.IUnknown)>] ppParent: byref<Object> 
                -> unit
            
        [<PreserveSig>]
        abstract member EnumAdapters:
            Adapter: uint *
            ppAdapter: byref<IDXGIAdapter>
                -> unativeint

        abstract member MakeWindowAssociation:
            WindowHandle: WinInterop.HWND *
            Flags: uint
                -> unit

        abstract member GetWindowAssociation:
            WindowHandle: byref<WinInterop.HWND>
                -> unit

        abstract member CreateSwapChain:
            pDevice: ID3D12CommandQueue *
            pDesc: byref<DXGI_SWAP_CHAIN_DESC> *
            [<MarshalAs(UnmanagedType.IUnknown)>] ppSwapChain: byref<Object>
                -> unit

        abstract member CreateSoftwareAdapter:
            Module: nativeint * 
            ppAdapter: byref<IDXGIAdapter> 
                -> unit

            [<PreserveSig>]
        abstract member EnumAdapters1: 
            Adapter: uint32 * 
            ppAdapter: byref<IDXGIAdapter>
                -> unativeint

        abstract member IsCurrent: 
            unit -> bool

        abstract member IsWindowedStereoEnabled:
            unit -> bool

        abstract member CreateSwapChainForHwnd: 
            pDevice: ID3D12CommandQueue * 
            hWnd: nativeint * 
            pDesc: byref<DXGI_SWAP_CHAIN_DESC1> * 
            pFullscreenDesc: int64 * 
            pRestrictToOutput: IDXGIOutput * 
            ppSwapChain: byref<IDXGISwapChain1>
                -> unit

        abstract member CreateSwapChainForCoreWindow:
            [<MarshalAs(UnmanagedType.IUnknown)>] pDevice: Object *
            [<MarshalAs(UnmanagedType.IUnknown)>] pWindow: Object * 
            pDesc: byref<DXGI_SWAP_CHAIN_DESC1> * 
            pRestrictToOutput: IDXGIOutput *
            ppSwapChain: byref<IDXGISwapChain1>
                -> unit
                (*
        abstract member HRESULT GetSharedResourceAdapterLuid(/* [annotation] _In_ */ IntPtr hResource, /* [annotation] _Out_ */ out LUID pLuid);
        abstract member HRESULT RegisterStereoStatusWindow(/* [annotation][in] _In_ */ IntPtr WindowHandle, /* [annotation][in] _In_ */ uint wMsg, /* [annotation][out] _Out_ */ out uint pdwCookie);
        abstract member HRESULT RegisterStereoStatusEvent(/* [annotation][in] _In_ */ IntPtr hEvent, /* [annotation][out] _Out_ */ out uint pdwCookie);
        abstract member void UnregisterStereoStatus(/* [annotation][in] _In_ */ uint dwCookie);
        abstract member HRESULT RegisterOcclusionStatusWindow(/* [annotation][in] _In_ */ IntPtr WindowHandle, /* [annotation][in] _In_ */ uint wMsg, /* [annotation][out] _Out_ */ out uint pdwCookie);
        abstract member HRESULT RegisterOcclusionStatusEvent(/* [annotation][in] _In_ */ IntPtr hEvent, /* [annotation][out] _Out_ */ out uint pdwCookie);
        abstract member void UnregisterOcclusionStatus(/* [annotation][in] _In_ */ uint dwCookie);
        abstract member HRESULT CreateSwapChainForComposition(/* [annotation][in] _In_ */ [MarshalAs(UnmanagedType.IUnknown)] object pDevice, /* [annotation][in] _In_ */ ref DXGI_SWAP_CHAIN_DESC1 pDesc, /* [annotation][in] _In_opt_ */ IDXGIOutput pRestrictToOutput, /* [annotation][out] _COM_Outptr_ */ out IDXGISwapChain1 ppSwapChain);*)
    [<DllImport("dxgi", ExactSpelling = true, PreserveSig = false)>]
    extern void CreateDXGIFactory2(
        uint Flags,
        [<MarshalAs(UnmanagedType.LPStruct)>] [<In>] Guid riid, 
        IDXGIFactory2& ppFactory)

    [<DllImport("d3d12", ExactSpelling = true, PreserveSig = false)>]
    extern void D3D12CreateDevice(
        [<In>] IDXGIAdapter pAdapter, 
        D3D_FEATURE_LEVEL MinimumFeatureLevel,
        [<MarshalAs(UnmanagedType.LPStruct)>] Guid riid,
        ID3D12Device& ppDevice)

    [<DllImport("d3d12", ExactSpelling = true, PreserveSig = false)>]
    extern void D3D12GetDebugInterface(
        [<MarshalAs(UnmanagedType.LPStruct)>] Guid riid,
        ID3D12Debug& ppvDebug)

let loadPipeline(window: WinInterop.Window) =

    let mutable debugLayer: D3dInterop.ID3D12Debug = null
    let mutable result = 0un
    D3dInterop.D3D12GetDebugInterface(typeof<D3dInterop.ID3D12Debug>.GUID, &debugLayer)
    if result <> 0un then do failwith $"Got result {result}"
    debugLayer.EnableDebugLayer()

    let mutable factory: D3dInterop.IDXGIFactory2 = null
    D3dInterop.CreateDXGIFactory2(0x01u, typeof<D3dInterop.IDXGIFactory2>.GUID, &factory)
    if result <> 0un then do failwith $"Got result {result}"
    
    // factory.MakeWindowAssociation(window.Handle, 0x1u)

    let mutable idx = 0u
    let mutable adapter: D3dInterop.IDXGIAdapter = null
    while adapter = null do
        result <- factory.EnumAdapters(idx, &adapter)
        idx <- idx + 1u
        if result <> 0un then
            do failwith $"Got result {result}"

        if idx >= 1000u then
            do failwith "Could not find adapter in 1000 tries"

    let mutable device: D3dInterop.ID3D12Device = null
    D3dInterop.D3D12CreateDevice(null, D3dInterop.D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_12_0, typeof<D3dInterop.ID3D12Device>.GUID, &device)
    if result <> 0un then do failwith $"Got result {result}"

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

    let mutable rtvHandle = D3dInterop.D3D12_CPU_DESCRIPTOR_HANDLE()
    heap.GetCPUDescriptorHandleForHeapStart(&rtvHandle)
    (*
    let rtvDescriptors = 
        [|
            for i in 0u..frameCount ->
                let mutable buffer: Object = null
                swapChain.GetBuffer()
        |]*)

    ()

let loadAssets() = 
    ()
    

let init() =
    let window = WinInterop.makeWindow()
    loadPipeline window
    loadAssets()
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