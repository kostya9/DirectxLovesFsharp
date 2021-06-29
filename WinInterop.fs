module WinInterop

    open System.Runtime.InteropServices
    open System

    type HWND = nativeint
    type Handle = nativeint

    type Window(handle: HWND, windowCallback: Delegate) = 
        member this.WinCallback = windowCallback
        member this.Handle = handle

    [<RequireQualifiedAccess>]
    module External =

        [<Struct>]
        [<StructLayout(LayoutKind.Sequential)>]
        type CREATESTRUCTW = 
            val mutable lpCreateParams: nativeint
            val mutable hInstance: Handle
            val mutable hMenu: Handle
            val mutable hwndParent: Handle
            val mutable cy: int
            val mutable cx: int
            val mutable y: int
            val mutable x: int
            val mutable style: int
            [<MarshalAs(UnmanagedType.LPWStr)>] val mutable lpszName: string
            [<MarshalAs(UnmanagedType.LPWStr)>] val mutable lpszClass: string
            val mutable dwExStyle: uint
            

        [<Flags>]
        type WindowStyles = 
            | CS_HREDRAW = 0x0002u
            | CS_VREDRAW = 0x0001u

        [<Struct>]
        [<StructLayout(LayoutKind.Sequential)>]
        type WNDCLASSEXA =
            val mutable cbSize: uint
            val mutable style: WindowStyles
            val mutable lpfnWndProc: nativeint
            val mutable cbClsExtra: int
            val mutable cbWndExtra: int
            val mutable hInstance: Handle
            val mutable hIcon: Handle
            val mutable hCursor: Handle
            val mutable hbrBackground: Handle
            [<MarshalAs(UnmanagedType.LPWStr)>] val mutable lpszMenuName: string
            [<MarshalAs(UnmanagedType.LPWStr)>] val mutable lpszClassName: string
            val mutable hIconSm: Handle

        [<System.FlagsAttribute>]
        type WindowStyle = 
            | WS_OVERLAPPED = 0x00000000u
            | WS_CAPTION = 0x00C00000u
            | WS_SYSMENU = 0x00080000u
            | WS_THICKFRAME = 0x00040000u
            | WS_MINIMIZEBOX = 0x00020000u
            | WS_MAXIMIZEBOX = 0x00010000u
            | WS_VISIBLE = 0x10000000u

        module CombinedWindowStyle = 
            let WS_OVERLAPPEDWINDOW = WindowStyle.WS_OVERLAPPED ||| WindowStyle.WS_CAPTION ||| WindowStyle.WS_SYSMENU ||| WindowStyle.WS_THICKFRAME ||| WindowStyle.WS_MINIMIZEBOX ||| WindowStyle.WS_MAXIMIZEBOX

        [<Struct>]
        [<StructLayout(LayoutKind.Sequential)>]
        type Point = 
            val mutable X: double
            val mutable Y: double

        [<Struct>]
        [<StructLayout(LayoutKind.Sequential)>]
        type MSG = 
            val mutable hwnd: HWND
            val mutable message: uint
            val mutable wParam: nativeint
            val mutable lParam: nativeint
            val mutable time: uint32
            val mutable pt: Point
            val mutable lPrivate: uint32
        

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern nativeint CreateWindowExA(unativeint dwExStyle, 
            [<MarshalAs(UnmanagedType.LPWStr)>] string lpClassName, 
            [<MarshalAs(UnmanagedType.LPWStr)>] string lpWindowName, 
            WindowStyle dwStyle,
            nativeint X, 
            nativeint Y,
            nativeint nWidth,
            nativeint nHeight,
            nativeint hWndParent,
            nativeint hMenu,
            nativeint hInstance,
            nativeint lpParam)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern unit ShowWindow(nativeint hWnd, int nCmdShow)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern unit UpdateWindow(nativeint hWnd)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern uint16 RegisterClassExA(WNDCLASSEXA& unnamedParam1)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern nativeint DefWindowProcA(HWND hWnd, unativeint uMsg, nativeint wParam, nativeint lParam)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern bool GetMessageA(MSG& messages, HWND hWnd, unativeint wMsgFilterMin, unativeint wMsgFilterMax)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern bool TranslateMessage(MSG& messages)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern bool DispatchMessageA(MSG& messages)

        [<DllImport("kernel32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern Handle CreateEventW(
            Object lpEventAttributes,
            bool bManualReset,
            bool bInitialState,
            [<MarshalAs(UnmanagedType.LPWStr)>] string lpName)

        [<DllImport("kernel32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern unit WaitForSingleObject(Handle hHandle, uint dwMilliseconds)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern unit SetWindowLongPtrW(Handle hWNd, int nIndex, nativeint dwNewLong)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern voidptr GetWindowLongPtrW(Handle hWNd, int nIndex)

        [<DllImport("user32", ExactSpelling = true, SetLastError = true, ThrowOnUnmappableChar = true)>]
        extern void PostQuitMessage(int nExitCode)


    type WindowProc = delegate of nativeint * unativeint * nativeint * nativeint -> nativeint

    module WindowMsgType =
        let WM_CREATE = 0x0001un
        let WM_PAINT = 0x000Fun
        let WM_DESTROY = 0x0002un

    let makeWindow(callback: WindowProc) =

        let windowCallbackPointer = Marshal.GetFunctionPointerForDelegate callback

        let thisInstance = Marshal.GetHINSTANCE(System.Reflection.Assembly.GetEntryAssembly().Modules |> Seq.head)

        let mutable className = "classmyclassyv" + "1"

        let mutable windowClass = External.WNDCLASSEXA()
        windowClass.cbSize <- uint sizeof<External.WNDCLASSEXA>
        windowClass.style <- External.WindowStyles.CS_HREDRAW ||| External.WindowStyles.CS_VREDRAW;
        windowClass.lpfnWndProc <- windowCallbackPointer
        windowClass.hInstance <- thisInstance
        windowClass.lpszClassName <- className
        let classId = External.RegisterClassExA(&windowClass)
        if classId = 0us then do
            let error = Marshal.GetLastWin32Error()
            failwith $"RegisterClassEx Win32 error = {error}"

        let style = External.CombinedWindowStyle.WS_OVERLAPPEDWINDOW
        let windowHwnd = External.CreateWindowExA(0un, className, "Hello", style, 0n, 0n, 800n, 600n, 0n, 0n, thisInstance, 0n)

        if windowHwnd = 0n then do
            let error = Marshal.GetLastWin32Error()
            failwith $"CreateWindowExA error = {error}"

        Window(windowHwnd, callback)

    let showWindow(window: Window) =
        External.ShowWindow(window.Handle, 1)
        External.UpdateWindow(window.Handle)
