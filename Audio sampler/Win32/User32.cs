using System;
using System.Runtime.InteropServices;

namespace Audio_sampler.Win32 {

  public static class User32 {

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern ushort RegisterClassExW(ref WindowClassEx windowClassEx);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreateWindowExW(ExtendedWindowStyle dwExStyle,  string lpClassName, string lpWindowName,
                                                WindowStyle         dwStyle,    int    x,           int    y,
                                                int                 nWidth,     int    nHeight,
                                                IntPtr              hWndParent, IntPtr hMenu, IntPtr hInstance,
                                                IntPtr              lpParam);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool SetLayeredWindowAttributes(IntPtr                  hwnd, uint crKey, byte bAlpha,
                                                         LayeredWindowAttributes dwFlags);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool UpdateWindow(IntPtr hWnd);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool PeekMessageW(ref Message msg, IntPtr hwnd, uint filterMin, uint filterMax,
                                           uint        removeMsg);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool WaitMessage();

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool TranslateMessage(ref Message msg);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool DispatchMessageW(ref Message msg);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool UnregisterClassW(string lpClassName, IntPtr hInstance);

    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool PostMessageW(IntPtr hwnd, WindowMessage message, IntPtr wparam, IntPtr lparam);
    
    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern IntPtr DefWindowProcW(IntPtr hwnd, WindowMessage msg, IntPtr wparam, IntPtr lparam);
    
    [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode)]
    public static extern bool SendMessageW(IntPtr hwnd, WindowMessage msg, IntPtr wparam, IntPtr lparam);

  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct WindowClassEx {

    public uint   Size;
    public uint   Style;
    public IntPtr WindowProc;
    public int    ClsExtra;
    public int    WindowExtra;
    public IntPtr Instance;
    public IntPtr Icon;
    public IntPtr Curser;
    public IntPtr Background;
    public string MenuName;
    public string ClassName;
    public IntPtr IconSm;

    public static uint NativeSize() => (uint) Marshal.SizeOf(typeof(WindowClassEx));

  }

  [Flags]
  public enum ExtendedWindowStyle : uint {

    Left                = 0,
    LtrReading          = 0,
    RightScrollbar      = 0,
    DlgModalFrame       = 1,
    NoParentNotify      = 4,
    Topmost             = 8,
    AcceptFiles         = 16,                                // 0x00000010
    Transparent         = 32,                                // 0x00000020
    MdiChild            = 64,                                // 0x00000040
    ToolWindow          = 128,                               // 0x00000080
    WindowEdge          = 256,                               // 0x00000100
    PaletteWindow       = WindowEdge | ToolWindow | Topmost, // 0x00000188
    ClientEdge          = 512,                               // 0x00000200
    OverlappedWindow    = ClientEdge | WindowEdge,           // 0x00000300
    ContextHelp         = 1024,                              // 0x00000400
    Right               = 4096,                              // 0x00001000
    RtlReading          = 8192,                              // 0x00002000
    LeftScrollbar       = 16384,                             // 0x00004000
    ControlParent       = 65536,                             // 0x00010000
    StaticEdge          = 131072,                            // 0x00020000
    AppWindow           = 262144,                            // 0x00040000
    Layered             = 524288,                            // 0x00080000
    NoInheritLayout     = 1048576,                           // 0x00100000
    NoRedirectionBitmap = 2097152,                           // 0x00200000
    LayoutRtl           = 4194304,                           // 0x00400000
    Composited          = 33554432,                          // 0x02000000
    NoActivate          = 134217728,                         // 0x08000000

  }

  [Flags]
  public enum WindowStyle : uint {

    Overlapped       = 0,
    Tiled            = 0,
    MaximizeBox      = 10000,                                                      // 0x00002710
    Tabstop          = 65536,                                                      // 0x00010000
    Group            = 131072,                                                     // 0x00020000
    MinimizeBox      = Group,                                                      // 0x00020000
    Sizebox          = 262144,                                                     // 0x00040000
    ThickFrame       = Sizebox,                                                    // 0x00040000
    SysMenu          = 524288,                                                     // 0x00080000
    HScroll          = 1048576,                                                    // 0x00100000
    VScroll          = 2097152,                                                    // 0x00200000
    DlgFrame         = 4194304,                                                    // 0x00400000
    Border           = 8388608,                                                    // 0x00800000
    Caption          = Border                                       | DlgFrame,    // 0x00C00000
    OverlappedWindow = Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox, // 0x00CE2710
    TiledWindow      = OverlappedWindow,                                           // 0x00CE2710
    Maximize         = 16777216,                                                   // 0x01000000
    ClipChildren     = 33554432,                                                   // 0x02000000
    ClipSiblings     = 67108864,                                                   // 0x04000000
    Disabled         = 134217728,                                                  // 0x08000000
    Visible          = 268435456,                                                  // 0x10000000
    Iconic           = 536870912,                                                  // 0x20000000
    Minimize         = Iconic,                                                     // 0x20000000
    Child            = 1073741824,                                                 // 0x40000000
    ChildWindow      = Child,                                                      // 0x40000000
    Popup            = 2147483648,                                                 // 0x80000000
    PopupWindow      = Popup | Border | SysMenu,                                   // 0x80880000

  }

  public enum LayeredWindowAttributes : uint {

    None     = 0,
    ColorKey = 1,
    Alpha    = 2,
    Opaque   = 4,

  }

  public enum WindowMessage : uint {

    Null                        = 0,
    Create                      = 1,
    Destroy                     = 2,
    Move                        = 3,
    Size                        = 5,
    Activate                    = 6,
    Setfocus                    = 7,
    Killfocus                   = 8,
    Enable                      = 10,    // 0x0000000A
    Setredraw                   = 11,    // 0x0000000B
    Settext                     = 12,    // 0x0000000C
    Gettext                     = 13,    // 0x0000000D
    Gettextlength               = 14,    // 0x0000000E
    Paint                       = 15,    // 0x0000000F
    Close                       = 16,    // 0x00000010
    Queryendsession             = 17,    // 0x00000011
    Quit                        = 18,    // 0x00000012
    Queryopen                   = 19,    // 0x00000013
    EraseBackground             = 20,    // 0x00000014
    Syscolorchange              = 21,    // 0x00000015
    Endsession                  = 22,    // 0x00000016
    Showwindow                  = 24,    // 0x00000018
    Settingchange               = 26,    // 0x0000001A
    Wininichange                = 26,    // 0x0000001A
    Devmodechange               = 27,    // 0x0000001B
    Activateapp                 = 28,    // 0x0000001C
    Fontchange                  = 29,    // 0x0000001D
    Timechange                  = 30,    // 0x0000001E
    Cancelmode                  = 31,    // 0x0000001F
    Setcursor                   = 32,    // 0x00000020
    Mouseactivate               = 33,    // 0x00000021
    Childactivate               = 34,    // 0x00000022
    Queuesync                   = 35,    // 0x00000023
    Getminmaxinfo               = 36,    // 0x00000024
    Painticon                   = 38,    // 0x00000026
    Iconerasebkgnd              = 39,    // 0x00000027
    Nextdlgctl                  = 40,    // 0x00000028
    Spoolerstatus               = 42,    // 0x0000002A
    Drawitem                    = 43,    // 0x0000002B
    Measureitem                 = 44,    // 0x0000002C
    Deleteitem                  = 45,    // 0x0000002D
    Vkeytoitem                  = 46,    // 0x0000002E
    Chartoitem                  = 47,    // 0x0000002F
    Setfont                     = 48,    // 0x00000030
    Getfont                     = 49,    // 0x00000031
    Sethotkey                   = 50,    // 0x00000032
    Gethotkey                   = 51,    // 0x00000033
    Querydragicon               = 55,    // 0x00000037
    Compareitem                 = 57,    // 0x00000039
    Getobject                   = 61,    // 0x0000003D
    Compacting                  = 65,    // 0x00000041
    Commnotify                  = 68,    // 0x00000044
    Windowposchanging           = 70,    // 0x00000046
    Windowposchanged            = 71,    // 0x00000047
    Power                       = 72,    // 0x00000048
    Copydata                    = 74,    // 0x0000004A
    Canceljournal               = 75,    // 0x0000004B
    Notify                      = 78,    // 0x0000004E
    Inputlangchangerequest      = 80,    // 0x00000050
    Inputlangchange             = 81,    // 0x00000051
    Tcard                       = 82,    // 0x00000052
    Help                        = 83,    // 0x00000053
    Userchanged                 = 84,    // 0x00000054
    Notifyformat                = 85,    // 0x00000055
    Contextmenu                 = 123,   // 0x0000007B
    Stylechanging               = 124,   // 0x0000007C
    Stylechanged                = 125,   // 0x0000007D
    Displaychange               = 126,   // 0x0000007E
    Geticon                     = 127,   // 0x0000007F
    Seticon                     = 128,   // 0x00000080
    Nccreate                    = 129,   // 0x00000081
    Ncdestroy                   = 130,   // 0x00000082
    Nccalcsize                  = 131,   // 0x00000083
    Nchittest                   = 132,   // 0x00000084
    NcPaint                     = 133,   // 0x00000085
    Ncactivate                  = 134,   // 0x00000086
    Getdlgcode                  = 135,   // 0x00000087
    Syncpaint                   = 136,   // 0x00000088
    Ncmousemove                 = 160,   // 0x000000A0
    Nclbuttondown               = 161,   // 0x000000A1
    Nclbuttonup                 = 162,   // 0x000000A2
    Nclbuttondblclk             = 163,   // 0x000000A3
    Ncrbuttondown               = 164,   // 0x000000A4
    Ncrbuttonup                 = 165,   // 0x000000A5
    Ncrbuttondblclk             = 166,   // 0x000000A6
    Ncmbuttondown               = 167,   // 0x000000A7
    Ncmbuttonup                 = 168,   // 0x000000A8
    Ncmbuttondblclk             = 169,   // 0x000000A9
    Ncxbuttondown               = 171,   // 0x000000AB
    Ncxbuttonup                 = 172,   // 0x000000AC
    Ncxbuttondblclk             = 173,   // 0x000000AD
    InputDeviceChange           = 254,   // 0x000000FE
    Input                       = 255,   // 0x000000FF
    Keydown                     = 256,   // 0x00000100
    Keyfirst                    = 256,   // 0x00000100
    Keyup                       = 257,   // 0x00000101
    Char                        = 258,   // 0x00000102
    Deadchar                    = 259,   // 0x00000103
    Syskeydown                  = 260,   // 0x00000104
    Syskeyup                    = 261,   // 0x00000105
    Syschar                     = 262,   // 0x00000106
    Sysdeadchar                 = 263,   // 0x00000107
    Keylast                     = 265,   // 0x00000109
    Unichar                     = 265,   // 0x00000109
    ImeStartcomposition         = 269,   // 0x0000010D
    ImeEndcomposition           = 270,   // 0x0000010E
    ImeComposition              = 271,   // 0x0000010F
    ImeKeylast                  = 271,   // 0x0000010F
    Initdialog                  = 272,   // 0x00000110
    Command                     = 273,   // 0x00000111
    Syscommand                  = 274,   // 0x00000112
    Timer                       = 275,   // 0x00000113
    Hscroll                     = 276,   // 0x00000114
    Vscroll                     = 277,   // 0x00000115
    Initmenu                    = 278,   // 0x00000116
    Initmenupopup               = 279,   // 0x00000117
    Menuselect                  = 287,   // 0x0000011F
    Menuchar                    = 288,   // 0x00000120
    Enteridle                   = 289,   // 0x00000121
    Menurbuttonup               = 290,   // 0x00000122
    Menudrag                    = 291,   // 0x00000123
    Menugetobject               = 292,   // 0x00000124
    Uninitmenupopup             = 293,   // 0x00000125
    Menucommand                 = 294,   // 0x00000126
    Changeuistate               = 295,   // 0x00000127
    Updateuistate               = 296,   // 0x00000128
    Queryuistate                = 297,   // 0x00000129
    Ctlcolormsgbox              = 306,   // 0x00000132
    Ctlcoloredit                = 307,   // 0x00000133
    Ctlcolorlistbox             = 308,   // 0x00000134
    Ctlcolorbtn                 = 309,   // 0x00000135
    Ctlcolordlg                 = 310,   // 0x00000136
    Ctlcolorscrollbar           = 311,   // 0x00000137
    Ctlcolorstatic              = 312,   // 0x00000138
    Gethmenu                    = 481,   // 0x000001E1
    Mousefirst                  = 512,   // 0x00000200
    Mousemove                   = 512,   // 0x00000200
    Lbuttondown                 = 513,   // 0x00000201
    Lbuttonup                   = 514,   // 0x00000202
    Lbuttondblclk               = 515,   // 0x00000203
    Rbuttondown                 = 516,   // 0x00000204
    Rbuttonup                   = 517,   // 0x00000205
    Rbuttondblclk               = 518,   // 0x00000206
    Mbuttondown                 = 519,   // 0x00000207
    Mbuttonup                   = 520,   // 0x00000208
    Mbuttondblclk               = 521,   // 0x00000209
    Mousewheel                  = 522,   // 0x0000020A
    Xbuttondown                 = 523,   // 0x0000020B
    Xbuttonup                   = 524,   // 0x0000020C
    Xbuttondblclk               = 525,   // 0x0000020D
    Mousehwheel                 = 526,   // 0x0000020E
    Parentnotify                = 528,   // 0x00000210
    Entermenuloop               = 529,   // 0x00000211
    Exitmenuloop                = 530,   // 0x00000212
    Nextmenu                    = 531,   // 0x00000213
    Sizing                      = 532,   // 0x00000214
    Capturechanged              = 533,   // 0x00000215
    Moving                      = 534,   // 0x00000216
    Powerbroadcast              = 536,   // 0x00000218
    Devicechange                = 537,   // 0x00000219
    Mdicreate                   = 544,   // 0x00000220
    Mdidestroy                  = 545,   // 0x00000221
    Mdiactivate                 = 546,   // 0x00000222
    Mdirestore                  = 547,   // 0x00000223
    Mdinext                     = 548,   // 0x00000224
    Mdimaximize                 = 549,   // 0x00000225
    Mditile                     = 550,   // 0x00000226
    Mdicascade                  = 551,   // 0x00000227
    Mdiiconarrange              = 552,   // 0x00000228
    Mdigetactive                = 553,   // 0x00000229
    Mdisetmenu                  = 560,   // 0x00000230
    Entersizemove               = 561,   // 0x00000231
    Exitsizemove                = 562,   // 0x00000232
    Dropfiles                   = 563,   // 0x00000233
    Mdirefreshmenu              = 564,   // 0x00000234
    ImeSetcontext               = 641,   // 0x00000281
    ImeNotify                   = 642,   // 0x00000282
    ImeControl                  = 643,   // 0x00000283
    ImeCompositionfull          = 644,   // 0x00000284
    ImeSelect                   = 645,   // 0x00000285
    ImeChar                     = 646,   // 0x00000286
    ImeRequest                  = 648,   // 0x00000288
    ImeKeydown                  = 656,   // 0x00000290
    ImeKeyup                    = 657,   // 0x00000291
    Ncmousehover                = 672,   // 0x000002A0
    Mousehover                  = 673,   // 0x000002A1
    Ncmouseleave                = 674,   // 0x000002A2
    Mouseleave                  = 675,   // 0x000002A3
    WtssessionChange            = 689,   // 0x000002B1
    TabletFirst                 = 704,   // 0x000002C0
    TabletLast                  = 735,   // 0x000002DF
    DpiChanged                  = 736,   // 0x000002E0
    Cut                         = 768,   // 0x00000300
    Copy                        = 769,   // 0x00000301
    Paste                       = 770,   // 0x00000302
    Clear                       = 771,   // 0x00000303
    Undo                        = 772,   // 0x00000304
    Renderformat                = 773,   // 0x00000305
    Renderallformats            = 774,   // 0x00000306
    Destroyclipboard            = 775,   // 0x00000307
    Drawclipboard               = 776,   // 0x00000308
    Paintclipboard              = 777,   // 0x00000309
    Vscrollclipboard            = 778,   // 0x0000030A
    Sizeclipboard               = 779,   // 0x0000030B
    Askcbformatname             = 780,   // 0x0000030C
    Changecbchain               = 781,   // 0x0000030D
    Hscrollclipboard            = 782,   // 0x0000030E
    Querynewpalette             = 783,   // 0x0000030F
    Paletteischanging           = 784,   // 0x00000310
    Palettechanged              = 785,   // 0x00000311
    Hotkey                      = 786,   // 0x00000312
    Print                       = 791,   // 0x00000317
    Printclient                 = 792,   // 0x00000318
    Appcommand                  = 793,   // 0x00000319
    Themechanged                = 794,   // 0x0000031A
    Clipboardupdate             = 797,   // 0x0000031D
    DwmCompositionChanged       = 798,   // 0x0000031E
    Dwmncrenderingchanged       = 799,   // 0x0000031F
    Dwmcolorizationcolorchanged = 800,   // 0x00000320
    Dwmwindowmaximizedchange    = 801,   // 0x00000321
    Gettitlebarinfoex           = 831,   // 0x0000033F
    Handheldfirst               = 856,   // 0x00000358
    Handheldlast                = 863,   // 0x0000035F
    Afxfirst                    = 864,   // 0x00000360
    Afxlast                     = 895,   // 0x0000037F
    Penwinfirst                 = 896,   // 0x00000380
    Penwinlast                  = 911,   // 0x0000038F
    User                        = 1024,  // 0x00000400
    Reflect                     = 8192,  // 0x00002000
    App                         = 32768, // 0x00008000

  }

  public struct Message {

    public IntPtr        Hwnd;
    public WindowMessage Msg;
    public IntPtr        lParam;
    public IntPtr        wParam;
    public uint          Time;
    public int           X;
    public int           Y;

  }
  
  public delegate IntPtr WindowProc(IntPtr hWnd, WindowMessage msg, IntPtr wParam, IntPtr lParam);

}