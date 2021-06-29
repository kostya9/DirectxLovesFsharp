module D3dInterop

open System
open System.Runtime.InteropServices

[<RequireQualifiedAccess>]
module D3DCOMPILE = 
    let D3DCOMPILE_DEBUG = 1u <<< 0

    let D3DCOMPILE_SKIP_OPTIMIZATION = 1u <<< 2

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
    | DXGI_FORMAT_UNKNOWN = 0
    | DXGI_FORMAT_R32G32B32A32_FLOAT = 2
    | DXGI_FORMAT_R32G32B32_FLOAT = 6
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
[<Guid("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a")>]
[<ComImport>]
type IDXGISwapChain = interface end

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_RANGE =
    val mutable Begin: unativeint
    val mutable End: unativeint

// Well, at some point I will probably need to specify these
type D3D12_COMPUTE_PIPELINE_STATE_DESC = unit
type D3D12_FEATURE = unit
type D3D12_DEPTH_STENCIL_VIEW_DESC = struct end
type D3D12_SAMPLER_DESC = struct end
type D3D12_RESOURCE_ALLOCATION_INFO = struct end
type D3D12_CLEAR_VALUE = struct end
type D3D12_HEAP_DESC = struct end
type ID3D12Heap = interface end
type ID3D12DeviceChild = interface end
type SECURITY_ATTRIBUTES = struct end 
type ID3D12Pageable = interface end

type D3D12_TEXTURE_COPY_LOCATION = struct end
type D3D12_BOX = struct end
type D3D12_TILE_COPY_FLAGS = struct end

type D3D12_TILED_RESOURCE_COORDINATE = struct end
type D3D12_TILE_REGION_SIZE = struct end
type D3D12_TILE_RANGE_FLAGS = struct end
type D3D12_TILE_MAPPING_FLAGS = struct end

[<AllowNullLiteral>]
[<Guid("7116d91c-e7e4-47ce-b8c6-ec8168f437e5")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12CommandList = interface end

[<Flags>]
type D3D12_FENCE_FLAGS =
    | D3D12_FENCE_FLAG_NONE = 0
    | D3D12_FENCE_FLAG_SHARED = 0x1
    | D3D12_FENCE_FLAG_SHARED_CROSS_ADAPTER = 0x2
    | D3D12_FENCE_FLAG_NON_MONITORED = 0x4


[<AllowNullLiteral>]
[<Guid("0a753dcf-c4d8-4b91-adf6-be5a60d95a76")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12Fence =
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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    [<PreserveSig>]
    abstract member GetCompletedValue:
        unit -> uint64
    
    abstract member SetEventOnCompletion:
        Value: uint64 *
        hEvent: WinInterop.Handle
            -> unit
    
    abstract member Signal:
        Value: uint64 -> unit


[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_VERTEX_BUFFER_VIEW = 
    val mutable BufferLocation: uint64
    val mutable SizeInBytes: uint
    val mutable StrideInBytes: uint

[<Flags>]
type D3D12_RESOURCE_STATES =
    | D3D12_RESOURCE_STATE_COMMON = 0
    | D3D12_RESOURCE_STATE_VERTEX_AND_CONSTANT_BUFFER = 0x1
    | D3D12_RESOURCE_STATE_INDEX_BUFFER = 0x2
    | D3D12_RESOURCE_STATE_RENDER_TARGET = 0x4
    | D3D12_RESOURCE_STATE_UNORDERED_ACCESS = 0x8
    | D3D12_RESOURCE_STATE_DEPTH_WRITE = 0x10
    | D3D12_RESOURCE_STATE_DEPTH_READ = 0x20
    | D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE = 0x40
    | D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE = 0x80
    | D3D12_RESOURCE_STATE_STREAM_OUT = 0x100
    | D3D12_RESOURCE_STATE_INDIRECT_ARGUMENT = 0x200
    | D3D12_RESOURCE_STATE_COPY_DEST = 0x400
    | D3D12_RESOURCE_STATE_COPY_SOURCE = 0x800
    | D3D12_RESOURCE_STATE_RESOLVE_DEST = 0x1000
    | D3D12_RESOURCE_STATE_RESOLVE_SOURCE = 0x2000
    | D3D12_RESOURCE_STATE_RAYTRACING_ACCELERATION_STRUCTURE = 0x400000
    | D3D12_RESOURCE_STATE_SHADING_RATE_SOURCE = 0x1000000
    | D3D12_RESOURCE_STATE_PRESENT = 0
    | D3D12_RESOURCE_STATE_PREDICATION = 0x200
    | D3D12_RESOURCE_STATE_VIDEO_DECODE_READ = 0x10000
    | D3D12_RESOURCE_STATE_VIDEO_DECODE_WRITE = 0x20000
    | D3D12_RESOURCE_STATE_VIDEO_PROCESS_READ = 0x40000
    | D3D12_RESOURCE_STATE_VIDEO_PROCESS_WRITE = 0x80000
    | D3D12_RESOURCE_STATE_VIDEO_ENCODE_READ = 0x200000
    | D3D12_RESOURCE_STATE_VIDEO_ENCODE_WRITE = 0x800000

module KnownResourceStates = 
    let D3D12_RESOURCE_STATE_GENERIC_READ = (
        D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_VERTEX_AND_CONSTANT_BUFFER 
        ||| D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_INDEX_BUFFER 
        ||| D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_NON_PIXEL_SHADER_RESOURCE 
        ||| D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PIXEL_SHADER_RESOURCE 
        ||| D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_INDIRECT_ARGUMENT 
        ||| D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_COPY_SOURCE)

type D3D12_RESOURCE_DIMENSION = 
    | D3D12_RESOURCE_DIMENSION_UNKNOWN = 0
    | D3D12_RESOURCE_DIMENSION_BUFFER = 1
    | D3D12_RESOURCE_DIMENSION_TEXTURE1D = 2
    | D3D12_RESOURCE_DIMENSION_TEXTURE2D = 3
    | D3D12_RESOURCE_DIMENSION_TEXTURE3D = 4

type D3D12_TEXTURE_LAYOUT = 
    | D3D12_TEXTURE_LAYOUT_UNKNOWN = 0
    | D3D12_TEXTURE_LAYOUT_ROW_MAJOR = 1
    | D3D12_TEXTURE_LAYOUT_64KB_UNDEFINED_SWIZZLE = 2
    | D3D12_TEXTURE_LAYOUT_64KB_STANDARD_SWIZZLE = 3

[<Flags>]
type D3D12_RESOURCE_FLAGS = 
    | D3D12_RESOURCE_FLAG_NONE = 0
    | D3D12_RESOURCE_FLAG_ALLOW_RENDER_TARGET = 0x1
    | D3D12_RESOURCE_FLAG_ALLOW_DEPTH_STENCIL = 0x2
    | D3D12_RESOURCE_FLAG_ALLOW_UNORDERED_ACCESS = 0x4
    | D3D12_RESOURCE_FLAG_DENY_SHADER_RESOURCE = 0x8
    | D3D12_RESOURCE_FLAG_ALLOW_CROSS_ADAPTER = 0x10
    | D3D12_RESOURCE_FLAG_ALLOW_SIMULTANEOUS_ACCESS = 0x20
    | D3D12_RESOURCE_FLAG_VIDEO_DECODE_REFERENCE_ONLY = 0x40

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RESOURCE_DESC = 
    val mutable Dimension: D3D12_RESOURCE_DIMENSION
    val mutable Alignment: uint64
    val mutable Width: uint64
    val mutable Height: uint
    val mutable DepthOrArraySize: uint16
    val mutable MipLevels: uint16
    val mutable Format: DXGI_FORMAT
    val mutable SampleDesc: DXGI_SAMPLE_DESC
    val mutable Layout: D3D12_TEXTURE_LAYOUT
    val mutable Flags: D3D12_RESOURCE_FLAGS

[<AllowNullLiteral>]
[<Guid("696442be-a72e-4059-bc79-5b5c98040fad")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12Resource =
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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    abstract member Map:
        Subresource : uint *
        pReadRange: byref<D3D12_RANGE> *
        ppData: byref<voidptr> 
            -> unit
    
    abstract member Unmap:
        Subresource : uint *
        pWrittenRange: nativeint // byref<D3D12_RANGE>, but idk how to pass nullptr
            -> unit

    [<PreserveSig>]
    abstract member GetDesc:
        unit -> D3D12_RESOURCE_DESC
    
    [<PreserveSig>]
    abstract member GetGPUVirtualAddress:
        unit -> uint64


type D3D12_HEAP_FLAGS = 
    | D3D12_HEAP_FLAG_NONE = 0
    | D3D12_HEAP_FLAG_SHARED = 0x1
    | D3D12_HEAP_FLAG_DENY_BUFFERS = 0x4
    | D3D12_HEAP_FLAG_ALLOW_DISPLAY = 0x8
    | D3D12_HEAP_FLAG_SHARED_CROSS_ADAPTER = 0x20
    | D3D12_HEAP_FLAG_DENY_RT_DS_TEXTURES = 0x40
    | D3D12_HEAP_FLAG_DENY_NON_RT_DS_TEXTURES = 0x80
    | D3D12_HEAP_FLAG_HARDWARE_PROTECTED = 0x100
    | D3D12_HEAP_FLAG_ALLOW_WRITE_WATCH = 0x200
    | D3D12_HEAP_FLAG_ALLOW_SHADER_ATOMICS = 0x400
    | D3D12_HEAP_FLAG_CREATE_NOT_RESIDENT = 0x800
    | D3D12_HEAP_FLAG_CREATE_NOT_ZEROED = 0x1000
    | D3D12_HEAP_FLAG_ALLOW_ALL_BUFFERS_AND_TEXTURES = 0
    | D3D12_HEAP_FLAG_ALLOW_ONLY_BUFFERS = 0xc0
    | D3D12_HEAP_FLAG_ALLOW_ONLY_NON_RT_DS_TEXTURES = 0x44
    | D3D12_HEAP_FLAG_ALLOW_ONLY_RT_DS_TEXTURES = 0x84

type D3D12_HEAP_TYPE =
    | D3D12_HEAP_TYPE_DEFAULT = 1
    | D3D12_HEAP_TYPE_UPLOAD = 2
    | D3D12_HEAP_TYPE_READBACK = 3
    | D3D12_HEAP_TYPE_CUSTOM = 4

type D3D12_CPU_PAGE_PROPERTY = 
    | D3D12_CPU_PAGE_PROPERTY_UNKNOWN = 0
    | D3D12_CPU_PAGE_PROPERTY_NOT_AVAILABLE = 1
    | D3D12_CPU_PAGE_PROPERTY_WRITE_COMBINE = 2
    | D3D12_CPU_PAGE_PROPERTY_WRITE_BACK = 3

type D3D12_MEMORY_POOL = 
    | D3D12_MEMORY_POOL_UNKNOWN = 0
    | D3D12_MEMORY_POOL_L0 = 1
    | D3D12_MEMORY_POOL_L1 = 2

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_HEAP_PROPERTIES =
    val mutable Type: D3D12_HEAP_TYPE
    val mutable CPUPageProperty: D3D12_CPU_PAGE_PROPERTY
    val mutable MemoryPoolPreference: D3D12_MEMORY_POOL
    val mutable CreationNodeMask: uint
    val mutable VisibleNodeMask: uint

[<AllowNullLiteral>]
[<Guid("765a30f3-f624-4c6f-a828-ace948622445")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12PipelineState = interface end

type D3D_ROOT_SIGNATURE_VERSION = 
    | D3D_ROOT_SIGNATURE_VERSION_1 = 0x1
    | D3D_ROOT_SIGNATURE_VERSION_1_0 = 0x1
    | D3D_ROOT_SIGNATURE_VERSION_1_1 = 0x2

type D3D12_ROOT_SIGNATURE_FLAGS = 
    | D3D12_ROOT_SIGNATURE_FLAG_NONE                                  = 0x0
    | D3D12_ROOT_SIGNATURE_FLAG_ALLOW_INPUT_ASSEMBLER_INPUT_LAYOUT    = 0x1
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_VERTEX_SHADER_ROOT_ACCESS        = 0x2
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_HULL_SHADER_ROOT_ACCESS          = 0x4
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_DOMAIN_SHADER_ROOT_ACCESS        = 0x8
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_GEOMETRY_SHADER_ROOT_ACCESS      = 0x10
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_PIXEL_SHADER_ROOT_ACCESS         = 0x20
    | D3D12_ROOT_SIGNATURE_FLAG_ALLOW_STREAM_OUTPUT                   = 0x40
    | D3D12_ROOT_SIGNATURE_FLAG_LOCAL_ROOT_SIGNATURE                  = 0x80
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_AMPLIFICATION_SHADER_ROOT_ACCESS = 0x100
    | D3D12_ROOT_SIGNATURE_FLAG_DENY_MESH_SHADER_ROOT_ACCESS          = 0x200
    | D3D12_ROOT_SIGNATURE_FLAG_CBV_SRV_UAV_HEAP_DIRECTLY_INDEXED     = 0x400
    | D3D12_ROOT_SIGNATURE_FLAG_SAMPLER_HEAP_DIRECTLY_INDEXED         = 0x800

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_ROOT_SIGNATURE_DESC = 
    val mutable NumParameters: uint
    val mutable pParameters: nativeint
    val mutable NumStaticSamplers: uint
    val mutable pStaticSamplers: nativeint
    val mutable Flags: D3D12_ROOT_SIGNATURE_FLAGS
    

[<AllowNullLiteral>]
[<Guid("6102dee4-af59-4b09-b999-b44d73f09b24")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12CommandAllocator =

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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    abstract member Reset:
        unit -> unit

[<AllowNullLiteral>]
[<Guid("0ec870a6-5d7e-4c22-8cfc-5baae07616ed")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12CommandQueue =
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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    abstract member UpdateTileMappings:
        pResource: ID3D12Resource *
        NumResourceRegions: uint *
        pResourceRegionStartCoordinates: byref<D3D12_TILED_RESOURCE_COORDINATE> *
        pResourceRegionSizes: byref<D3D12_TILE_REGION_SIZE> *
        pHeap: byref<ID3D12Heap> *
        NumRanges: uint *
        pRangeFlags: byref<D3D12_TILE_RANGE_FLAGS> *
        pHeapRangeStartOffsets: byref<uint> *
        pRangeTileCounts: byref<uint> *
        Flags: D3D12_TILE_MAPPING_FLAGS
            -> unit
    
    abstract member CopyTileMappings:
        pDstResource: ID3D12Resource *
        pDstRegionStartCoordinate: byref<D3D12_TILED_RESOURCE_COORDINATE> *
        pSrcResource: ID3D12Resource *
        pSrcRegionStartCoordinate: byref<D3D12_TILED_RESOURCE_COORDINATE> *
        pRegionSize: byref<D3D12_TILE_REGION_SIZE> *
        Flags: D3D12_TILE_MAPPING_FLAGS
            -> unit
    
    abstract member ExecuteCommandLists:
        NumCommandLists: uint *
        [<MarshalAs(UnmanagedType.LPArray)>] ppCommandLists: ID3D12CommandList[]
            -> unit
    
    abstract member SetMarker:
        Metadata: uint *
        pData: voidptr *
        Size: uint
            -> unit
    
    abstract member BeginEvent:
        Metadata: uint *
        pData: voidptr *
        Size: uint
            -> unit
    
    abstract member EndEvent:
        unit -> unit
    
    abstract member Signal:
        pFence: ID3D12Fence *
        Value: uint64
            -> unit
    
    abstract member Wait:
        pFence: ID3D12Fence *
        Value: uint64
            -> unit

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

    [<PreserveSig>]
    abstract member GetDesc: byref<D3D12_DESCRIPTOR_HEAP_DESC> -> unit

    [<PreserveSig>]
    abstract member GetCPUDescriptorHandleForHeapStart: byref<D3D12_CPU_DESCRIPTOR_HANDLE> -> unit

    [<PreserveSig>]
    abstract member GetGPUDescriptorHandleForHeapStart: byref<D3D12_GPU_DESCRIPTOR_HANDLE> -> unit

[<AllowNullLiteral>]
[<Guid("c54a6b66-72df-4ee8-8be5-a946a1429214")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12RootSignature = interface end


type D3D12_PRIMITIVE_TOPOLOGY = 
    | D3D_PRIMITIVE_TOPOLOGY_UNDEFINED = 0
    | D3D_PRIMITIVE_TOPOLOGY_POINTLIST = 1
    | D3D_PRIMITIVE_TOPOLOGY_LINELIST = 2
    | D3D_PRIMITIVE_TOPOLOGY_LINESTRIP = 3
    | D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST = 4

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RECT =
    val mutable left: int
    val mutable top: int
    val mutable right: int
    val mutable bottom: int

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_VIEWPORT =
    val mutable TopLeftX: single
    val mutable TopLeftY: single
    val mutable Width: single
    val mutable Height: single
    val mutable MinDepth: single
    val mutable MaxDepth: single

type D3D12_RESOURCE_BARRIER_TYPE = 
    | D3D12_RESOURCE_BARRIER_TYPE_TRANSITION = 0
    | D3D12_RESOURCE_BARRIER_TYPE_ALIASING = 1
    | D3D12_RESOURCE_BARRIER_TYPE_UAV = 2

type D3D12_RESOURCE_BARRIER_FLAGS =
    | D3D12_RESOURCE_BARRIER_FLAG_NONE = 0
    | D3D12_RESOURCE_BARRIER_FLAG_BEGIN_ONLY = 0x1
    | D3D12_RESOURCE_BARRIER_FLAG_END_ONLY = 0x2

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RESOURCE_TRANSITION_BARRIER = 
    val mutable pResource: nativeint
    val mutable Subresource: uint
    val mutable StateBefore: D3D12_RESOURCE_STATES
    val mutable StateAfter: D3D12_RESOURCE_STATES

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RESOURCE_ALIASING_BARRIER = 
    val mutable pResourceBefore: nativeint
    val mutable pResourceAfter: nativeint

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RESOURCE_UAV_BARRIER = 
    val mutable pResource: nativeint

[<Struct>]
[<StructLayout(LayoutKind.Explicit, Pack = 0)>]
type D3D12_RESOURCE_BARRIER_Union = 
    [<FieldOffset(0)>]
    val mutable Transition: D3D12_RESOURCE_TRANSITION_BARRIER

    [<FieldOffset(0)>]
    val mutable Aliasing: D3D12_RESOURCE_ALIASING_BARRIER

    [<FieldOffset(0)>]
    val mutable UAV: D3D12_RESOURCE_UAV_BARRIER

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RESOURCE_BARRIER =
    val mutable Type: D3D12_RESOURCE_BARRIER_TYPE

    val mutable Flags: D3D12_RESOURCE_BARRIER_FLAGS

    val mutable Union: D3D12_RESOURCE_BARRIER_Union

type D3D12_GPU_VIRTUAL_ADDRESS = struct end
type D3D12_INDEX_BUFFER_VIEW = struct end
type D3D12_STREAM_OUTPUT_BUFFER_VIEW = struct end
type D3D12_CLEAR_FLAGS = struct end

[<AllowNullLiteral>]
[<Guid("5b160d0f-ac1b-4185-8ba8-b3ae42a5a455")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12GraphicsCommandList =
    inherit ID3D12CommandList

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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    [<PreserveSig>]
    abstract member GetType:
        unit -> D3D12_COMMAND_LIST_TYPE

    [<PreserveSig>]
    abstract member Close: 
        unit -> unit

    abstract member Reset:
        pAllocator: ID3D12CommandAllocator *
        pInitialState: ID3D12PipelineState
            -> unit

    abstract member ClearState:
        pPipelineState: ID3D12PipelineState
            -> unit
    
    [<PreserveSig>]
    abstract member DrawInstanced:
        VertexCountPerInstance: uint * 
        InstanceCount: uint * 
        StartVertexLocation: uint * 
        StartInstanceLocation: uint
            -> unit
    
    abstract member DrawIndexedInstanced:
        IndexCountPerInstance: uint * 
        InstanceCount: uint * 
        StartIndexLocation: uint * 
        BaseVertexLocation: int * 
        StartInstanceLocation: uint
            -> unit
    
    abstract member Dispatch:
        ThreadGroupCountX: uint * 
        ThreadGroupCountY: uint * 
        ThreadGroupCountZ: uint
            -> unit
    
    abstract member CopyBufferRegion:
        pDstBuffer: ID3D12Resource * 
        DstOffset: uint64 * 
        pSrcBuffer: ID3D12Resource * 
        SrcOffset: uint64 * 
        NumBytes: uint64
            -> unit
    
    abstract member CopyTextureRegion:
        pDst: byref<D3D12_TEXTURE_COPY_LOCATION> * 
        DstX: uint * 
        DstY: uint * 
        DstZ: uint * 
        pSrc: byref<D3D12_TEXTURE_COPY_LOCATION> * 
        pSrcBox: byref<D3D12_BOX>
            -> unit
    
    abstract member CopyResource:
        pDstResource: ID3D12Resource * 
        pSrcResource: ID3D12Resource
            -> unit
    
    abstract member CopyTiles:
        pTiledResource: ID3D12Resource * 
        pTileRegionStartCoordinate: byref<D3D12_TILED_RESOURCE_COORDINATE> * 
        pTileRegionSize: byref<D3D12_TILE_REGION_SIZE> * 
        pBuffer: ID3D12Resource * 
        BufferStartOffsetInBytes: uint64 * 
        Flags: D3D12_TILE_COPY_FLAGS
            -> unit
    
    abstract member ResolveSubresource:
        pDstResource: ID3D12Resource * 
        DstSubresource: uint * 
        pSrcResource: ID3D12Resource * 
        SrcSubresource: uint * 
        Format: DXGI_FORMAT
            -> unit
    
    [<PreserveSig>]
    abstract member IASetPrimitiveTopology:
        PrimitiveTopology: D3D12_PRIMITIVE_TOPOLOGY 
            -> unit
    
    [<PreserveSig>]
    abstract member RSSetViewports:
        NumViewports: uint * 
        pViewports: byref<D3D12_VIEWPORT>
            -> unit
    
    [<PreserveSig>]
    abstract member RSSetScissorRects:
        NumRects: uint * 
        pRects: byref<D3D12_RECT>
            -> unit
    
    abstract member OMSetBlendFactor:
        [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)>]
        BlendFactor: float[] // Size = 4
            -> unit
    
    abstract member OMSetStencilRef:
        StencilRef: uint
            -> unit
    
    abstract member SetPipelineState:
        pPipelineState: ID3D12PipelineState
            -> unit
    
    [<PreserveSig>]
    abstract member ResourceBarrier:
        NumBarriers: uint * 
        pBarriers: byref<D3D12_RESOURCE_BARRIER>
            -> unit
    
    abstract member ExecuteBundle:
        pCommandList: ID3D12GraphicsCommandList
            -> unit
    
    abstract member SetDescriptorHeaps:
        NumDescriptorHeaps: uint * 
        ppDescriptorHeaps: byref<ID3D12DescriptorHeap>
            -> unit
    
    abstract member SetComputeRootSignature:
        pRootSignature: ID3D12RootSignature
            -> unit
    
    [<PreserveSig>]
    abstract member SetGraphicsRootSignature:
        pRootSignature: ID3D12RootSignature
            -> unit
    
    abstract member SetComputeRootDescriptorTable:
        RootParameterIndex: uint * 
        BaseDescriptor: D3D12_GPU_DESCRIPTOR_HANDLE
            -> unit
    
    abstract member SetGraphicsRootDescriptorTable:
        RootParameterIndex: uint * 
        BaseDescriptor: D3D12_GPU_DESCRIPTOR_HANDLE
            -> unit
    
    abstract member SetComputeRoot32BitConstant:
        RootParameterIndex: uint * 
        SrcData: uint * 
        DestOffsetIn32BitValues: uint
            -> unit
    
    abstract member SetGraphicsRoot32BitConstant:
        RootParameterIndex: uint * 
        SrcData: uint * 
        DestOffsetIn32BitValues: uint
            -> unit
    
    abstract member SetComputeRoot32BitConstants:
        RootParameterIndex: uint * 
        Num32BitValuesToSet: uint * 
        pSrcData: voidptr *
        DestOffsetIn32BitValues: uint
            -> unit
    
    abstract member SetGraphicsRoot32BitConstants:
        RootParameterIndex: uint * 
        Num32BitValuesToSet: uint * 
        pSrcData: voidptr *
        DestOffsetIn32BitValues: uint
            -> unit
    
    abstract member SetComputeRootConstantBufferView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member SetGraphicsRootConstantBufferView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member SetComputeRootShaderResourceView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member SetGraphicsRootShaderResourceView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member SetComputeRootUnorderedAccessView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member SetGraphicsRootUnorderedAccessView:
        RootParameterIndex: uint * 
        BufferLocation: D3D12_GPU_VIRTUAL_ADDRESS
            -> unit
    
    abstract member IASetIndexBuffer:
        pView: byref<D3D12_INDEX_BUFFER_VIEW>
            -> unit
    
    [<PreserveSig>]
    abstract member IASetVertexBuffers:
        StartSlot: uint * 
        NumViews: uint * 
        pViews: inref<D3D12_VERTEX_BUFFER_VIEW>
            -> unit
    
    abstract member SOSetTargets:
        StartSlot: uint * 
        NumViews: uint * 
        pViews: byref<D3D12_STREAM_OUTPUT_BUFFER_VIEW>
            -> unit
    
    abstract member OMSetRenderTargets:
        NumRenderTargetDescriptors: uint * 
        pRenderTargetDescriptors: byref<D3D12_CPU_DESCRIPTOR_HANDLE> * 
        RTsSingleHandleToDescriptorRange: bool * 
        pDepthStencilDescriptor: nativeint // Should be byref<D3D12_CPU_DESCRIPTOR_HANDLE>, but idk how to pass null
            -> unit
    
    abstract member ClearDepthStencilView:
        DepthStencilView: D3D12_CPU_DESCRIPTOR_HANDLE * 
        ClearFlags: D3D12_CLEAR_FLAGS * 
        Depth: single * 
        Stencil: uint8 * 
        NumRects: uint * 
        pRects: byref<D3D12_RECT>
            -> unit
    
    abstract member ClearRenderTargetView:
        RenderTargetView: D3D12_CPU_DESCRIPTOR_HANDLE * 
        [<MarshalAs(UnmanagedType.LPArray)>] ColorRGBA: single[] * 
        NumRects: uint * 
        pRects: nativeint // Should be byref<D3D12_RECT>, but idk how to pass null
            -> unit

type D3D12_INPUT_CLASSIFICATION = 
    | D3D12_INPUT_CLASSIFICATION_PER_VERTEX_DATA = 0
    | D3D12_INPUT_CLASSIFICATION_PER_INSTANCE_DATA = 1

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_INPUT_ELEMENT_DESC =
    [<MarshalAs(UnmanagedType.LPStr)>] val mutable SemanticName: string;
    val mutable SemanticIndex: uint;
    val mutable Format: DXGI_FORMAT;
    val mutable InputSlot: uint;
    val mutable AlignedByteOffset: uint;
    val mutable InputSlotClass: D3D12_INPUT_CLASSIFICATION;
    val mutable InstanceDataStepRate: uint;

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_SHADER_BYTECODE =
    val mutable pShaderBytecode: nativeint
    val mutable BytecodeLength: unativeint

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_SO_DECLARATION_ENTRY =
    val mutable Stream: uint;
    [<MarshalAs(UnmanagedType.LPStr)>] val mutable  SemanticName: string;
    val mutable SemanticIndex: uint;
    val mutable StartComponent: byte;
    val mutable ComponentCount: byte;
    val mutable OutputSlot: byte;

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_STREAM_OUTPUT_DESC = 
    val mutable pSODeclaration: nativeint // Pointer to D3D12_SO_DECLARATION_ENTRY (cannot have byref in structs, which is unfortunate)
    val mutable NumEntries: uint
    val mutable pBufferStrides: nativeint
    val mutable NumStrides: uint;
    val mutable RasterizedStream: uint;

type D3D12_BLEND =
    | D3D12_BLEND_ZERO = 1
    | D3D12_BLEND_ONE = 2
    | D3D12_BLEND_SRC_COLOR = 3
    | D3D12_BLEND_INV_SRC_COLOR = 4
    | D3D12_BLEND_SRC_ALPHA = 5
    | D3D12_BLEND_INV_SRC_ALPHA = 6
    | D3D12_BLEND_DEST_ALPHA = 7
    | D3D12_BLEND_INV_DEST_ALPHA = 8
    | D3D12_BLEND_DEST_COLOR = 9
    | D3D12_BLEND_INV_DEST_COLOR = 10
    | D3D12_BLEND_SRC_ALPHA_SAT = 11
    | D3D12_BLEND_BLEND_FACTOR = 14
    | D3D12_BLEND_INV_BLEND_FACTOR = 15
    | D3D12_BLEND_SRC1_COLOR = 16
    | D3D12_BLEND_INV_SRC1_COLOR = 17
    | D3D12_BLEND_SRC1_ALPHA = 18
    | D3D12_BLEND_INV_SRC1_ALPHA = 19

type D3D12_BLEND_OP = 
    | D3D12_BLEND_OP_ADD = 1
    | D3D12_BLEND_OP_SUBTRACT = 2
    | D3D12_BLEND_OP_REV_SUBTRACT = 3
    | D3D12_BLEND_OP_MIN = 4
    | D3D12_BLEND_OP_MAX = 5

type D3D12_LOGIC_OP =
    | D3D12_LOGIC_OP_CLEAR = 0
    | D3D12_LOGIC_OP_SET = 1
    | D3D12_LOGIC_OP_COPY = 2
    | D3D12_LOGIC_OP_COPY_INVERTED = 3
    | D3D12_LOGIC_OP_NOOP = 4
    | D3D12_LOGIC_OP_INVERT = 5
    | D3D12_LOGIC_OP_AND = 6
    | D3D12_LOGIC_OP_NAND = 7
    | D3D12_LOGIC_OP_OR = 8
    | D3D12_LOGIC_OP_NOR = 9
    | D3D12_LOGIC_OP_XOR = 10
    | D3D12_LOGIC_OP_EQUIV = 11
    | D3D12_LOGIC_OP_AND_REVERSE = 12
    | D3D12_LOGIC_OP_AND_INVERTED = 13
    | D3D12_LOGIC_OP_OR_REVERSE = 14
    | D3D12_LOGIC_OP_OR_INVERTED = 15

[<Flags>]
type D3D12_COLOR_WRITE_ENABLE =
    | D3D12_COLOR_WRITE_ENABLE_RED = 1uy
    | D3D12_COLOR_WRITE_ENABLE_GREEN = 2uy
    | D3D12_COLOR_WRITE_ENABLE_BLUE = 4uy
    | D3D12_COLOR_WRITE_ENABLE_ALPHA = 8uy

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_RENDER_TARGET_BLEND_DESC =
    val mutable BlendEnable: bool;
    val mutable LogicOpEnable: bool;
    val mutable SrcBlend: D3D12_BLEND;
    val mutable DestBlend: D3D12_BLEND;
    val mutable BlendOp: D3D12_BLEND_OP;
    val mutable SrcBlendAlpha: D3D12_BLEND;
    val mutable DestBlendAlpha: D3D12_BLEND;
    val mutable BlendOpAlpha: D3D12_BLEND_OP;
    val mutable LogicOp: D3D12_LOGIC_OP;
    val mutable RenderTargetWriteMask: D3D12_COLOR_WRITE_ENABLE;

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D12_BLEND_DESC =
    val mutable AlphaToCoverageEnable: bool
    val mutable IndependentBlendEnable: bool

    [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)>] 
    val mutable RenderTarget: D3D12_RENDER_TARGET_BLEND_DESC[] // Size=8

type D3D12_FILL_MODE =
    | D3D12_FILL_MODE_WIREFRAME = 2
    | D3D12_FILL_MODE_SOLID = 3

type D3D12_CULL_MODE =
    | D3D12_CULL_MODE_NONE = 1
    | D3D12_CULL_MODE_FRONT = 2
    | D3D12_CULL_MODE_BACK = 3

type D3D12_CONSERVATIVE_RASTERIZATION_MODE = 
    | D3D12_CONSERVATIVE_RASTERIZATION_MODE_OFF = 0
    | D3D12_CONSERVATIVE_RASTERIZATION_MODE_ON = 1

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_RASTERIZER_DESC =
    val mutable FillMode: D3D12_FILL_MODE;
    val mutable CullMode: D3D12_CULL_MODE;
    val mutable FrontCounterClockwise: bool;
    val mutable DepthBias: int;
    val mutable DepthBiasClamp: single;
    val mutable SlopeScaledDepthBias: single;
    val mutable DepthClipEnable: bool;
    val mutable MultisampleEnable: bool;
    val mutable AntialiasedLineEnable: bool;
    val mutable ForcedSampleCount: uint;
    val mutable ConservativeRaster: D3D12_CONSERVATIVE_RASTERIZATION_MODE;

type D3D12_DEPTH_WRITE_MASK =
    | D3D12_DEPTH_WRITE_MASK_ZERO = 0
    | D3D12_DEPTH_WRITE_MASK_ALL = 1

type D3D12_COMPARISON_FUNC =
    | D3D12_COMPARISON_FUNC_NEVER = 1
    | D3D12_COMPARISON_FUNC_LESS = 2
    | D3D12_COMPARISON_FUNC_EQUAL = 3
    | D3D12_COMPARISON_FUNC_LESS_EQUAL = 4
    | D3D12_COMPARISON_FUNC_GREATER = 5
    | D3D12_COMPARISON_FUNC_NOT_EQUAL = 6
    | D3D12_COMPARISON_FUNC_GREATER_EQUAL = 7
    | D3D12_COMPARISON_FUNC_ALWAYS = 8

type D3D12_STENCIL_OP =
    | D3D12_STENCIL_OP_KEEP = 1
    | D3D12_STENCIL_OP_ZERO = 2
    | D3D12_STENCIL_OP_REPLACE = 3
    | D3D12_STENCIL_OP_INCR_SAT = 4
    | D3D12_STENCIL_OP_DECR_SAT = 5
    | D3D12_STENCIL_OP_INVERT = 6
    | D3D12_STENCIL_OP_INCR = 7
    | D3D12_STENCIL_OP_DECR = 8

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_DEPTH_STENCILOP_DESC =
    val mutable StencilFailOp: D3D12_STENCIL_OP;
    val mutable StencilDepthFailOp: D3D12_STENCIL_OP;
    val mutable StencilPassOp: D3D12_STENCIL_OP;
    val mutable StencilFunc: D3D12_COMPARISON_FUNC;

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_DEPTH_STENCIL_DESC =
    val mutable DepthEnable: bool;
    val mutable DepthWriteMask: D3D12_DEPTH_WRITE_MASK;
    val mutable DepthFunc: D3D12_COMPARISON_FUNC;
    val mutable StencilEnable: bool;
    val mutable StencilReadMask: uint8;
    val mutable StencilWriteMask: uint8;
    val mutable FrontFace: D3D12_DEPTH_STENCILOP_DESC;
    val mutable BackFace: D3D12_DEPTH_STENCILOP_DESC;

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_INPUT_LAYOUT_DESC =
    val mutable pInputElementDescs: nativeint; // Pointer to D3D12_INPUT_ELEMENT_DESC (cannot have byref in structs, which is unfortunate)
    val mutable NumElements: uint;

type D3D12_INDEX_BUFFER_STRIP_CUT_VALUE =
    | D3D12_INDEX_BUFFER_STRIP_CUT_VALUE_DISABLED = 0
    | D3D12_INDEX_BUFFER_STRIP_CUT_VALUE_0xFFFF = 1
    | D3D12_INDEX_BUFFER_STRIP_CUT_VALUE_0xFFFFFFFF = 2

type D3D12_PRIMITIVE_TOPOLOGY_TYPE =
    | D3D12_PRIMITIVE_TOPOLOGY_TYPE_UNDEFINED = 0
    | D3D12_PRIMITIVE_TOPOLOGY_TYPE_POINT = 1
    | D3D12_PRIMITIVE_TOPOLOGY_TYPE_LINE = 2
    | D3D12_PRIMITIVE_TOPOLOGY_TYPE_TRIANGLE = 3
    | D3D12_PRIMITIVE_TOPOLOGY_TYPE_PATCH = 4

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_CACHED_PIPELINE_STATE =
    val mutable pCachedBlob: nativeint;
    val mutable CachedBlobSizeInBytes: unativeint;

[<Flags>]
type D3D12_PIPELINE_STATE_FLAGS =
    | D3D12_PIPELINE_STATE_FLAG_NONE = 0x00000000
    | D3D12_PIPELINE_STATE_FLAG_TOOL_DEBUG = 0x00000001

[<StructLayout(LayoutKind.Sequential)>]
[<Struct>]
type D3D12_GRAPHICS_PIPELINE_STATE_DESC =
    val mutable pRootSignature: nativeint;
    val mutable VS: D3D12_SHADER_BYTECODE;
    val mutable PS: D3D12_SHADER_BYTECODE;
    val mutable DS: D3D12_SHADER_BYTECODE;
    val mutable HS: D3D12_SHADER_BYTECODE;
    val mutable GS: D3D12_SHADER_BYTECODE;
    val mutable StreamOutput: D3D12_STREAM_OUTPUT_DESC;
    val mutable BlendState: D3D12_BLEND_DESC;
    val mutable SampleMask: uint;
    val mutable RasterizerState: D3D12_RASTERIZER_DESC;
    val mutable DepthStencilState: D3D12_DEPTH_STENCIL_DESC;
    val mutable InputLayout: D3D12_INPUT_LAYOUT_DESC;
    val mutable IBStripCutValue: D3D12_INDEX_BUFFER_STRIP_CUT_VALUE;
    val mutable PrimitiveTopologyType: D3D12_PRIMITIVE_TOPOLOGY_TYPE;
    val mutable NumRenderTargets: uint;

    [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)>] 
    val mutable RTVFormats: DXGI_FORMAT[];

    val mutable DSVFormat: DXGI_FORMAT;
    val mutable SampleDesc: DXGI_SAMPLE_DESC;
    val mutable NodeMask: uint;
    val mutable CachedPSO: D3D12_CACHED_PIPELINE_STATE;
    val mutable Flags: D3D12_PIPELINE_STATE_FLAGS;

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
        ppCommandAllocator: byref<ID3D12CommandAllocator>
            -> unit

    abstract member CreateGraphicsPipelineState:
        pDesc: byref<D3D12_GRAPHICS_PIPELINE_STATE_DESC> *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        ppPipelineState: byref<ID3D12PipelineState>
            -> unit

    abstract member CreateComputePipelineState:
        pDesc: byref<D3D12_COMPUTE_PIPELINE_STATE_DESC> *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppPipelineState: byref<Object>
            -> unit

    abstract member CreateCommandList:
        nodeMask: uint *
        ``type``: D3D12_COMMAND_LIST_TYPE *
        pCommandAllocator: ID3D12CommandAllocator *
        pInitialState: ID3D12PipelineState *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        ppCommandList: byref<ID3D12GraphicsCommandList>
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

    [<PreserveSig>]
    abstract member GetDescriptorHandleIncrementSize:
        DescriptorHeapType: D3D12_DESCRIPTOR_HEAP_TYPE
            -> uint

    abstract member CreateRootSignature: 
        nodeMask: uint *
        pBlobWithRootSignature: nativeint * 
        blobLengthInBytes: unativeint * 
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        ppvRootSignature: byref<ID3D12RootSignature>
            -> unit
        
    abstract member CreateConstantBufferView: 
        pDesc: nativeint * 
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit
        
    abstract member CreateShaderResourceView: 
        pResource: ID3D12Resource * 
        pDesc: nativeint * 
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit
        
    abstract member CreateUnorderedAccessView: 
        pResource: ID3D12Resource *
        pCounterResource: ID3D12Resource * 
        pDesc: nativeint * 
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit
        
    abstract member CreateRenderTargetView: 
        pResource: ID3D12Resource * 
        pDesc: nativeint * 
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit

    abstract member CreateDepthStencilView:
        pResource: ID3D12Resource *
        pDesc: byref<D3D12_DEPTH_STENCIL_VIEW_DESC> *
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit
    
    abstract member CreateSampler:
        pDesc: byref<D3D12_SAMPLER_DESC> *
        DestDescriptor: D3D12_CPU_DESCRIPTOR_HANDLE
            -> unit
    
    abstract member CopyDescriptors:
        NumDestDescriptorRanges: uint *
        pDestDescriptorRangeStarts: byref<D3D12_CPU_DESCRIPTOR_HANDLE> *
        pDestDescriptorRangeSizes: byref<uint> *
        NumSrcDescriptorRanges: uint *
        pSrcDescriptorRangeStarts: byref<D3D12_CPU_DESCRIPTOR_HANDLE> *
        pSrcDescriptorRangeSizes: byref<uint> *
        DescriptorHeapsType: D3D12_DESCRIPTOR_HEAP_TYPE
            -> unit;
    
    abstract member CopyDescriptorsSimple:
        NumDescriptors: uint *
        DestDescriptorRangeStart: D3D12_CPU_DESCRIPTOR_HANDLE *
        SrcDescriptorRangeStart: D3D12_CPU_DESCRIPTOR_HANDLE *
        DescriptorHeapsType: D3D12_DESCRIPTOR_HEAP_TYPE
            -> unit;
    
    [<PreserveSig>]
    abstract member GetResourceAllocationInfo:
        visibleMask: uint *
        numResourceDescs: uint *
        pResourceDescs: byref<D3D12_RESOURCE_DESC>
            -> D3D12_RESOURCE_ALLOCATION_INFO
    
    [<PreserveSig>]
    abstract member GetCustomHeapProperties:
        Mask: uint *
        heapType: D3D12_HEAP_TYPE
            -> D3D12_HEAP_PROPERTIES
    
    abstract member CreateCommittedResource:
        pHeapProperties: byref<D3D12_HEAP_PROPERTIES> *
        HeapFlags: D3D12_HEAP_FLAGS *
        pDesc: byref<D3D12_RESOURCE_DESC> *
        InitialResourceState: D3D12_RESOURCE_STATES *
        pOptimizedClearValue: nativeint * // How do I pass nullref to byref<D3D12_CLEAR_VALUE>? idk, so nativeint it is
        [<MarshalAs(UnmanagedType.LPStruct)>] riidResource: Guid *
        ppvResource: byref<ID3D12Resource>
            -> unit

    abstract member CreateHeap:
        pDesc: byref<D3D12_HEAP_DESC> *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppvHeap: byref<Object>
            -> unit
    
    abstract member CreatePlacedResource:
        pHeap: ID3D12Heap *
        HeapOffset: uint64 *
        pDesc: byref<D3D12_RESOURCE_DESC> *
        InitialState: D3D12_RESOURCE_STATES *
        pOptimizedClearValue: byref<D3D12_CLEAR_VALUE> *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppvResource: byref<Object>
            -> unit
    
    abstract member CreateReservedResource:
        pDesc: byref<D3D12_RESOURCE_DESC> *
        InitialState: D3D12_RESOURCE_STATES *
        pOptimizedClearValue: byref<D3D12_CLEAR_VALUE> *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppvResource: byref<Object>
            -> unit
    
    abstract member CreateSharedHandle:
        pObject: byref<ID3D12DeviceChild> *
        pAttributes: byref<SECURITY_ATTRIBUTES> *
        Access: uint *
        [<MarshalAs(UnmanagedType.LPWStr)>] Name: string *
        pHandle: byref<WinInterop.Handle>
            -> unit
    
    abstract member OpenSharedHandle:
        NTHandle: WinInterop.Handle *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppvObj: byref<Object>
            -> unit
    
    abstract member OpenSharedHandleByName:
        [<MarshalAs(UnmanagedType.LPWStr)>] Name: string *
        Access: uint *
        pNTHandle: byref<WinInterop.Handle>
            -> unit
    
    abstract member MakeResident:
        NumObjects: uint *
        ppObjects: ID3D12Pageable
            -> unit
    
    abstract member Evict:
        NumObjects: uint *
        ppObjects: ID3D12Pageable
            -> unit
    
    abstract member CreateFence:
        InitialValue: uint64 *
        Flags: D3D12_FENCE_FLAGS *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        ppFence: byref<ID3D12Fence>
            -> unit

[<AllowNullLiteral>]
type IDXGIOutput = interface end
type DXGI_FRAME_STATISTICS = struct end
type DXGI_SWAP_CHAIN_FULLSCREEN_DESC = struct end
type DXGI_PRESENT_PARAMETERS = struct end
type DXGI_RGBA = struct end
type DXGI_MODE_ROTATION = struct end
type DXGI_MATRIX_3X2_F = struct end

[<AllowNullLiteral>]
[<Guid("94d99bdb-f1f8-4ab0-b236-7da0170edab1")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type IDXGISwapChain3 = 
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

    abstract member GetDevice:
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid * 
        [<MarshalAs(UnmanagedType.IUnknown)>] ppDevice: byref<Object> 
            -> unit

    abstract member Present:
        SyncInterval: uint *
        Flags: uint
            -> unit

    abstract member GetBuffer:
        Buffer: uint *
        [<MarshalAs(UnmanagedType.LPStruct)>] riid: Guid *
        ppSurface: byref<ID3D12Resource>
            -> unit

    abstract member SetFullscreenState: 
        Fullscreen: bool *
        pTarget: IDXGIOutput
            -> unit
    
    abstract member GetFullscreenState: 
        pFullscreen: byref<bool> *
        ppTarget: byref<IDXGIOutput>
            -> unit
    
    abstract member GetDesc: 
        pDesc: byref<DXGI_SWAP_CHAIN_DESC>
            -> unit
    
    abstract member ResizeBuffers: 
        BufferCount: uint *
        Width: uint *
        Height: uint *
        NewFormat: DXGI_FORMAT *
        SwapChainFlags: uint
            -> unit
    
    abstract member ResizeTarget: 
        pNewTargetParameters: byref<DXGI_MODE_DESC>
            -> unit
    
    abstract member GetContainingOutput: 
        ppOutput: byref<IDXGIOutput>
            -> unit
    
    abstract member GetFrameStatistics: 
        pStats: byref<DXGI_FRAME_STATISTICS>
            -> unit
    
    abstract member GetLastPresentCount: 
        pLastPresentCount: byref<uint>
            -> unit
    
    abstract member GetDesc1:
        pDesc: byref<DXGI_SWAP_CHAIN_DESC1>
            -> unit
    
    abstract member GetFullscreenDesc:
        pDesc: byref<DXGI_SWAP_CHAIN_FULLSCREEN_DESC>
            -> unit
    
    abstract member GetHwnd: 
        pHwnd: byref<WinInterop.Handle>
            -> unit
    
    abstract member GetCoreWindow: 
        [<MarshalAs(UnmanagedType.LPStruct)>] refiid: Guid *
        [<MarshalAs(UnmanagedType.IUnknown)>] ppUnk: byref<Object>
            -> unit
    
    abstract member Present1: 
        SyncInterval: uint *
        PresentFlags: uint *
        pPresentParameters: byref<DXGI_PRESENT_PARAMETERS>
            -> unit
    
    [<PreserveSig>]
    abstract member IsTemporaryMonoSupported:
        unit -> bool
    
    abstract member GetRestrictToOutput: 
        ppRestrictToOutput: byref<IDXGIOutput>
            -> unit
    
    abstract member SetBackgroundColor: 
        pColor: byref<DXGI_RGBA>
            -> unit
    
    abstract member GetBackgroundColor: 
        pColor: byref<DXGI_RGBA>
            -> unit
    
    abstract member SetRotation:
        Rotation: DXGI_MODE_ROTATION
            -> unit
    
    abstract member GetRotation: 
        pRotation: byref<DXGI_MODE_ROTATION>
            -> unit
    
    abstract member SetSourceSize:
        Width: uint *
        Height: uint
            -> unit
    
    abstract member GetSourceSize: 
        pWidth: byref<uint> *
        pHeight: byref<uint>
            -> unit
    
    abstract member SetMaximumFrameLatency: 
        MaxLatency: uint
            -> unit
    
    abstract member GetMaximumFrameLatency: 
        pMaxLatency: byref<uint>
            -> unit
    
    [<PreserveSig>]
    abstract member GetFrameLatencyWaitableObject:
        unit -> WinInterop.Handle
    
    abstract member SetMatrixTransform: 
        pMatrix: byref<DXGI_MATRIX_3X2_F>
            -> unit
    
    abstract member GetMatrixTransform: 
        pMatrix: byref<DXGI_MATRIX_3X2_F>
            -> unit
    
    [<PreserveSig>]
    abstract member GetCurrentBackBufferIndex:
        unit -> uint

[<Guid("8ba5fb08-5195-40e2-ac58-0d989c3a0102")>]
[<AllowNullLiteral>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3DBlob =
    [<PreserveSig>]
    abstract member GetBufferPointer: 
        unit -> nativeint

    [<PreserveSig>]
    abstract member GetBufferSize: 
        unit -> unativeint

type ID3DInclude = interface end

[<Struct>]
[<StructLayout(LayoutKind.Sequential)>]
type D3D_SHADER_MACRO = {
    [<MarshalAs(UnmanagedType.LPStr)>] Name: string
    [<MarshalAs(UnmanagedType.LPStr)>] Definition: string
}

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

    [<PreserveSig>]
    abstract member IsCurrent: 
        unit -> bool

    [<PreserveSig>]
    abstract member IsWindowedStereoEnabled:
        unit -> bool

    abstract member CreateSwapChainForHwnd: 
        pDevice: ID3D12CommandQueue * 
        hWnd: nativeint * 
        pDesc: byref<DXGI_SWAP_CHAIN_DESC1> * 
        pFullscreenDesc: int64 * 
        pRestrictToOutput: IDXGIOutput * 
        ppSwapChain: byref<IDXGISwapChain3>
            -> unit

    abstract member CreateSwapChainForCoreWindow:
        [<MarshalAs(UnmanagedType.IUnknown)>] pDevice: Object *
        [<MarshalAs(UnmanagedType.IUnknown)>] pWindow: Object * 
        pDesc: byref<DXGI_SWAP_CHAIN_DESC1> * 
        pRestrictToOutput: IDXGIOutput *
        ppSwapChain: byref<IDXGISwapChain3>
            -> unit
            (*
    abstract member HRESULT GetSharedResourceAdapterLuid(/* [annotation] _In_ */ IntPtr hResource, /* [annotation] _Out_ */ out LUID pLuid);
    abstract member HRESULT RegisterStereoStatusWindow(/* [annotation][in] _In_ */ IntPtr WindowHandle, /* [annotation][in] _In_ */ wMsg: uint, /* [annotation][out] _Out_ */ out pdwCookie: uint);
    abstract member HRESULT RegisterStereoStatusEvent(/* [annotation][in] _In_ */ IntPtr hEvent, /* [annotation][out] _Out_ */ out pdwCookie: uint);
    abstract member void UnregisterStereoStatus(/* [annotation][in] _In_ */ dwCookie: uint);
    abstract member HRESULT RegisterOcclusionStatusWindow(/* [annotation][in] _In_ */ IntPtr WindowHandle, /* [annotation][in] _In_ */ wMsg: uint, /* [annotation][out] _Out_ */ out pdwCookie: uint);
    abstract member HRESULT RegisterOcclusionStatusEvent(/* [annotation][in] _In_ */ IntPtr hEvent, /* [annotation][out] _Out_ */ out pdwCookie: uint);
    abstract member void UnregisterOcclusionStatus(/* [annotation][in] _In_ */ dwCookie: uint);
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

[<DllImport("d3d12", ExactSpelling = true, PreserveSig = false)>]
extern void D3D12SerializeRootSignature(
    D3D12_ROOT_SIGNATURE_DESC& pRootSignature,
    D3D_ROOT_SIGNATURE_VERSION Version,
    ID3DBlob& ppBlob,
    ID3DBlob& ppErrorBlob)

[<DllImport("d3dcompiler_47", ExactSpelling = true, PreserveSig = true)>]
extern unativeint D3DCompileFromFile(
    [<MarshalAs(UnmanagedType.LPWStr)>] string pFileName,
    nativeint pDefines,
    nativeint pInclude,
    [<MarshalAs(UnmanagedType.LPStr)>] string pEntrypoint,
    [<MarshalAs(UnmanagedType.LPStr)>] string pTarget,
    uint Flags1,
    uint Flags2,
    ID3DBlob& ppCode,
    ID3DBlob& ppErrorMsgs)
