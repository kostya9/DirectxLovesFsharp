﻿module D3dInterop

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
[<Guid("0ec870a6-5d7e-4c22-8cfc-5baae07616ed")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12CommandQueue = interface end

[<AllowNullLiteral>]
[<Guid("310d36a0-d2e7-4c0a-aa04-6a9d23b8886a")>]
[<ComImport>]
type IDXGISwapChain = interface end

[<AllowNullLiteral>]
[<Guid("696442be-a72e-4059-bc79-5b5c98040fad")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12Resource = interface end

[<AllowNullLiteral>]
[<Guid("790a45f7-0d42-4876-983a-0a55cfe6f4aa")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type IDXGISwapChain1 = 
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

// Well, at some point I will probably need to specify these
type D3D12_COMPUTE_PIPELINE_STATE_DESC = unit
type D3D12_FEATURE = unit

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
type ID3D12CommandAllocator = interface end

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

    abstract member GetDesc: unit -> D3D12_DESCRIPTOR_HEAP_DESC
       
    [<PreserveSig>]
    abstract member GetCPUDescriptorHandleForHeapStart: byref<D3D12_CPU_DESCRIPTOR_HANDLE> -> unit
        
    [<PreserveSig>]
    abstract member GetGPUDescriptorHandleForHeapStart: unit -> D3D12_GPU_DESCRIPTOR_HANDLE

[<AllowNullLiteral>]
[<Guid("c54a6b66-72df-4ee8-8be5-a946a1429214")>]
[<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>]
type ID3D12RootSignature = interface end

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
    val mutable pRootSignature: ID3D12RootSignature;
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
            -> unativeint

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

[<AllowNullLiteral>]
type IDXGIOutput = interface end

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
