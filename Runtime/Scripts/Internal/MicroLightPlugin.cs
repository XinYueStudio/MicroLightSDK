using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace MicroLight
{
    using HINSTANCE = System.IntPtr;
    using HWND = System.IntPtr;
    using SimulatorIntPtr = System.IntPtr;
    using RenderAPI = System.IntPtr;
    using CoverRenderAPI = System.IntPtr;
    using D3D11Device = System.IntPtr;
    using D3D11Texture2D = System.IntPtr;
    using D3D11Resource = System.IntPtr;
    using HANDLE = System.IntPtr;
    using D3D11ShaderResourceView = System.IntPtr;
    using RednerTextureIntPtr = System.IntPtr;
  
    /// <summary>
    /// 内核渲染器 接口类型
    /// </summary>
    public enum RendererType
    {
        /// <summary>
        ///  进程画面传输
        /// </summary>
        MicroLightRender,//ready
        /// <summary>
        ///   线程内部画面传输，创建窗口
        /// </summary>
        UnityRender,//ready
        /// <summary>
        /// 线程内部画面传输，不创建窗口 直接覆盖渲染
        /// </summary>
        UnityCoverRender,//ready
    }

    /// <summary>
    ///内核  渲染器
    /// </summary>
    public enum GfxRenderer
    {
        D3D11_0,//ready
        D3D11_1,//ready
        D3D12,//coming soon
        VULKAN//coming soon
    }

    public enum DXGI_FORMAT : long
    {
        DXGI_FORMAT_UNKNOWN = 0,
        DXGI_FORMAT_R32G32B32A32_TYPELESS = 1,
        DXGI_FORMAT_R32G32B32A32_FLOAT = 2,
        DXGI_FORMAT_R32G32B32A32_UINT = 3,
        DXGI_FORMAT_R32G32B32A32_SINT = 4,
        DXGI_FORMAT_R32G32B32_TYPELESS = 5,
        DXGI_FORMAT_R32G32B32_FLOAT = 6,
        DXGI_FORMAT_R32G32B32_UINT = 7,
        DXGI_FORMAT_R32G32B32_SINT = 8,
        DXGI_FORMAT_R16G16B16A16_TYPELESS = 9,
        DXGI_FORMAT_R16G16B16A16_FLOAT = 10,
        DXGI_FORMAT_R16G16B16A16_UNORM = 11,
        DXGI_FORMAT_R16G16B16A16_UINT = 12,
        DXGI_FORMAT_R16G16B16A16_SNORM = 13,
        DXGI_FORMAT_R16G16B16A16_SINT = 14,
        DXGI_FORMAT_R32G32_TYPELESS = 15,
        DXGI_FORMAT_R32G32_FLOAT = 16,
        DXGI_FORMAT_R32G32_UINT = 17,
        DXGI_FORMAT_R32G32_SINT = 18,
        DXGI_FORMAT_R32G8X24_TYPELESS = 19,
        DXGI_FORMAT_D32_FLOAT_S8X24_UINT = 20,
        DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
        DXGI_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
        DXGI_FORMAT_R10G10B10A2_TYPELESS = 23,
        DXGI_FORMAT_R10G10B10A2_UNORM = 24,
        DXGI_FORMAT_R10G10B10A2_UINT = 25,
        DXGI_FORMAT_R11G11B10_FLOAT = 26,
        DXGI_FORMAT_R8G8B8A8_TYPELESS = 27,
        DXGI_FORMAT_R8G8B8A8_UNORM = 28,
        DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
        DXGI_FORMAT_R8G8B8A8_UINT = 30,
        DXGI_FORMAT_R8G8B8A8_SNORM = 31,
        DXGI_FORMAT_R8G8B8A8_SINT = 32,
        DXGI_FORMAT_R16G16_TYPELESS = 33,
        DXGI_FORMAT_R16G16_FLOAT = 34,
        DXGI_FORMAT_R16G16_UNORM = 35,
        DXGI_FORMAT_R16G16_UINT = 36,
        DXGI_FORMAT_R16G16_SNORM = 37,
        DXGI_FORMAT_R16G16_SINT = 38,
        DXGI_FORMAT_R32_TYPELESS = 39,
        DXGI_FORMAT_D32_FLOAT = 40,
        DXGI_FORMAT_R32_FLOAT = 41,
        DXGI_FORMAT_R32_UINT = 42,
        DXGI_FORMAT_R32_SINT = 43,
        DXGI_FORMAT_R24G8_TYPELESS = 44,
        DXGI_FORMAT_D24_UNORM_S8_UINT = 45,
        DXGI_FORMAT_R24_UNORM_X8_TYPELESS = 46,
        DXGI_FORMAT_X24_TYPELESS_G8_UINT = 47,
        DXGI_FORMAT_R8G8_TYPELESS = 48,
        DXGI_FORMAT_R8G8_UNORM = 49,
        DXGI_FORMAT_R8G8_UINT = 50,
        DXGI_FORMAT_R8G8_SNORM = 51,
        DXGI_FORMAT_R8G8_SINT = 52,
        DXGI_FORMAT_R16_TYPELESS = 53,
        DXGI_FORMAT_R16_FLOAT = 54,
        DXGI_FORMAT_D16_UNORM = 55,
        DXGI_FORMAT_R16_UNORM = 56,
        DXGI_FORMAT_R16_UINT = 57,
        DXGI_FORMAT_R16_SNORM = 58,
        DXGI_FORMAT_R16_SINT = 59,
        DXGI_FORMAT_R8_TYPELESS = 60,
        DXGI_FORMAT_R8_UNORM = 61,
        DXGI_FORMAT_R8_UINT = 62,
        DXGI_FORMAT_R8_SNORM = 63,
        DXGI_FORMAT_R8_SINT = 64,
        DXGI_FORMAT_A8_UNORM = 65,
        DXGI_FORMAT_R1_UNORM = 66,
        DXGI_FORMAT_R9G9B9E5_SHAREDEXP = 67,
        DXGI_FORMAT_R8G8_B8G8_UNORM = 68,
        DXGI_FORMAT_G8R8_G8B8_UNORM = 69,
        DXGI_FORMAT_BC1_TYPELESS = 70,
        DXGI_FORMAT_BC1_UNORM = 71,
        DXGI_FORMAT_BC1_UNORM_SRGB = 72,
        DXGI_FORMAT_BC2_TYPELESS = 73,
        DXGI_FORMAT_BC2_UNORM = 74,
        DXGI_FORMAT_BC2_UNORM_SRGB = 75,
        DXGI_FORMAT_BC3_TYPELESS = 76,
        DXGI_FORMAT_BC3_UNORM = 77,
        DXGI_FORMAT_BC3_UNORM_SRGB = 78,
        DXGI_FORMAT_BC4_TYPELESS = 79,
        DXGI_FORMAT_BC4_UNORM = 80,
        DXGI_FORMAT_BC4_SNORM = 81,
        DXGI_FORMAT_BC5_TYPELESS = 82,
        DXGI_FORMAT_BC5_UNORM = 83,
        DXGI_FORMAT_BC5_SNORM = 84,
        DXGI_FORMAT_B5G6R5_UNORM = 85,
        DXGI_FORMAT_B5G5R5A1_UNORM = 86,
        DXGI_FORMAT_B8G8R8A8_UNORM = 87,
        DXGI_FORMAT_B8G8R8X8_UNORM = 88,
        DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
        DXGI_FORMAT_B8G8R8A8_TYPELESS = 90,
        DXGI_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
        DXGI_FORMAT_B8G8R8X8_TYPELESS = 92,
        DXGI_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
        DXGI_FORMAT_BC6H_TYPELESS = 94,
        DXGI_FORMAT_BC6H_UF16 = 95,
        DXGI_FORMAT_BC6H_SF16 = 96,
        DXGI_FORMAT_BC7_TYPELESS = 97,
        DXGI_FORMAT_BC7_UNORM = 98,
        DXGI_FORMAT_BC7_UNORM_SRGB = 99,
        DXGI_FORMAT_AYUV = 100,
        DXGI_FORMAT_Y410 = 101,
        DXGI_FORMAT_Y416 = 102,
        DXGI_FORMAT_NV12 = 103,
        DXGI_FORMAT_P010 = 104,
        DXGI_FORMAT_P016 = 105,
        DXGI_FORMAT_420_OPAQUE = 106,
        DXGI_FORMAT_YUY2 = 107,
        DXGI_FORMAT_Y210 = 108,
        DXGI_FORMAT_Y216 = 109,
        DXGI_FORMAT_NV11 = 110,
        DXGI_FORMAT_AI44 = 111,
        DXGI_FORMAT_IA44 = 112,
        DXGI_FORMAT_P8 = 113,
        DXGI_FORMAT_A8P8 = 114,
        DXGI_FORMAT_B4G4R4A4_UNORM = 115,

        DXGI_FORMAT_P208 = 130,
        DXGI_FORMAT_V208 = 131,
        DXGI_FORMAT_V408 = 132,


        DXGI_FORMAT_FORCE_UINT = 0xffffffff
    }

    /// <summary>
    /// 相机类型
    /// </summary>
    public enum EyeTye
    {
        /// <summary>
        /// 左眼相机
        /// </summary>
        Left,
        /// <summary>
        /// 右眼相机
        /// </summary>
        Right,
        /// <summary>
        /// 中心相机
        /// </summary>
        Center
    }
    /// <summary>
    /// 投影模式
    /// </summary>
    public enum ProjectionMode
    {
        /// <summary>
        ///   面投影，可以带倾斜角度
        /// </summary>
        Surface,
        /// <summary>
        ///   L型体验室
        /// </summary>
        LTypeRoom,
        /// <summary>
        /// 梯形体验室
        /// </summary>
        TrapezoidRoom,
    }

    /// <summary>
    /// 渲染模式
    /// </summary>
    public enum RenderMode
    {
        /// <summary>
        ///  左右3D,适用于单桌面，单墙面
        /// </summary>
        LR3D,
        /// <summary>
        /// 显卡主动3D渲染  适用于L型体验室 梯形体验室
        /// </summary>
        Stereo,



    }


    public enum TrackingSpaceId
    {
        Surface,
        LTypeRoomFront,
        LTypeRoomFloor,
        TrapezoidRoomLeft,
        TrapezoidRoomFront,
        TrapezoidRoomRight,
        TrapezoidRoomFloor,

    }

    public enum TrackingDeviceType
    {
        MicroLightIR,
        LightHouse
    }
    public enum Controller
    {
        None,
        Hmd,
        Touchpad1,
        Touchpad2,
        Remote1,
        Remote2,

        All = ~None
    }

    /// <summary>
    /// Key Code Settings 
    /// </summary>
    public enum Button : int
    {
        None = 0,
        Back = 1, //K1
        Menu = 2,//k2
        IndexTriggerUp = 3,//k3
        IndexTriggerDown = 4,//k3
        ThumbstickUp = 5,//StickUp
        ThumbstickDown = 6,//StickDown
        ThumbstickLeft = 7,//StickLeft
        ThumbstickRight = 8,//StickRight

        Any = ~None
    }
    /// <summary>
    /// Axis1D override
    /// </summary>
    public enum Axis1D
    {
        None = 0,
        LIndexTrigger,
        RIndexTrigger,
        Any = ~None
    }

    /// <summary>
    /// Axis2D override
    /// </summary>
    public enum Axis2D
    {
        None = 0,
        RThumbstick,
        LThumbstick,
        Any = ~None
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HoloGraphicFrustum
    {
        [MarshalAs(UnmanagedType.Struct)]
        public Matrix4x4 ProjectionMatrix;
        [MarshalAs(UnmanagedType.Struct)]
        public Quaternion Rotation;
        [MarshalAs(UnmanagedType.Bool)]
        public bool GoodData;
    }
 
 
    [Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TrapezoidRoomSize
    {
        [MarshalAs(UnmanagedType.R4)]
        public float GlobalHeight;
        [MarshalAs(UnmanagedType.R4)]
        public float FrontWidth;
        [MarshalAs(UnmanagedType.R4)]
        public float LeftWidth;
        [MarshalAs(UnmanagedType.R4)]
        public float RightWidth;
        [MarshalAs(UnmanagedType.R4)]
        public float IntersectionAngle;

    }
    [Serializable]
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct PlayAeraSize
    {
        [Header("投影面长宽，适用于桌面 墙面 L型体验室参数")]
        [Tooltip("投影面长宽，适用于桌面 墙面 L型体验室参数")]
        [MarshalAs(UnmanagedType.Struct)]
        public Vector2 mSurfaceSize;
        [Header("梯形体验室投影参数")]
        [Tooltip("梯形体验室投影参数")]
        [MarshalAs(UnmanagedType.Struct)]
        public TrapezoidRoomSize mTrapezoidRoomSize;
        [Header("投影面倾斜度")]
        [Tooltip("投影面倾斜度")]
        [Range(0, 90)]
        [MarshalAs(UnmanagedType.R4)]
        public float ProjectionFaceInclination;

    }
    public enum TrackingResult
    {
        Uninitialized = 1,
        Calibrating_InProgress = 100,
        Calibrating_OutOfRange = 101,
        Running_OK = 200,
        Running_OutOfRange = 201,
        Fallback_RotationOnly = 300,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct HmdVector3
    {
        public float v0; //float[3]
        public float v1;
        public float v2;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct HmdMatrix34
    {
        public float m0; //float[3][4]
        public float m1;
        public float m2;
        public float m3;
        public float m4;
        public float m5;
        public float m6;
        public float m7;
        public float m8;
        public float m9;
        public float m10;
        public float m11;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct TrackedDevicePose
    {
        public HmdMatrix34 mDeviceToAbsoluteTracking;
        public HmdVector3 vVelocity;
        public HmdVector3 vAngularVelocity;
        public TrackingResult eTrackingResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool bPoseIsValid;
        [MarshalAs(UnmanagedType.I1)]
        public bool bDeviceIsConnected;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct VRControllerAxis
    {
        public float x;
        public float y;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct VRControllerState
    {
        public uint unPacketNum;
        public ulong ulButtonPressed;
        public ulong ulButtonTouched;
        public VRControllerAxis rAxis0; //VRControllerAxis_t[5]
        public VRControllerAxis rAxis1;
        public VRControllerAxis rAxis2;
        public VRControllerAxis rAxis3;
        public VRControllerAxis rAxis4;
    }
    public enum VRButtonId
    {
        k_EButton_System = 0,
        k_EButton_ApplicationMenu = 1,
        k_EButton_Grip = 2,
        k_EButton_DPad_Left = 3,
        k_EButton_DPad_Up = 4,
        k_EButton_DPad_Right = 5,
        k_EButton_DPad_Down = 6,
        k_EButton_A = 7,
        k_EButton_ProximitySensor = 31,
        k_EButton_Axis0 = 32,
        k_EButton_Axis1 = 33,
        k_EButton_Axis2 = 34,
        k_EButton_Axis3 = 35,
        k_EButton_Axis4 = 36,
        k_EButton_SteamVR_Touchpad = 32,
        k_EButton_SteamVR_Trigger = 33,
        k_EButton_Dashboard_Back = 2,
        k_EButton_IndexController_A = 2,
        k_EButton_IndexController_B = 1,
        k_EButton_IndexController_JoyStick = 35,
        k_EButton_Max = 64,
    }
   public  enum TrackingUniverseOrigin
    {
        TrackingUniverseSeated = 0,     // Poses are provided relative to the seated zero pose
        TrackingUniverseStanding = 1,   // Poses are provided relative to the safe bounds configured by the user
        TrackingUniverseRawAndUncalibrated = 2, // Poses are provided in the coordinate system defined by the driver.  It has Y up and is unified for devices of the same driver. You usually don't want this one.
    }


    public class MicroLightPlugin
    {
      

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern HINSTANCE GetModuleHandle(string lpLibFileName);

        public enum GWL
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, GWL nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, GWL nIndex)
        {
            if (IntPtr.Size == 8)
                return GetWindowLongPtr64(hWnd, nIndex);
            else
                return GetWindowLongPtr32(hWnd, nIndex);
        }

        // size of a device name string
        private const int CCHDEVICENAME = 32;

        /// <summary>
        /// The MONITORINFOEX structure contains information about a display monitor.
        /// The GetMonitorInfo function stores information into a MONITORINFOEX structure or a MONITORINFO structure.
        /// The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to contain a name
        /// for the display monitor.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            /// <summary>
            /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function.
            /// Doing so lets the function determine the type of structure you are passing to it.
            /// </summary>
            public int Size;

            /// <summary>
            /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RectStruct Monitor;

            /// <summary>
            /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
            /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
            /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RectStruct WorkArea;

            /// <summary>
            /// The attributes of the display monitor.
            ///
            /// This member can be the following value:
            ///   1 : MONITORINFOF_PRIMARY
            /// </summary>
            public uint Flags;

            /// <summary>
            /// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
            /// and so can save some bytes by using a MONITORINFO structure.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string DeviceName;

            public void Init()
            {
                this.Size = 40 + 2 * CCHDEVICENAME;
                this.DeviceName = string.Empty;
            }
        }

        /// <summary>
        /// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
        /// </summary>
        /// <see cref="http://msdn.microsoft.com/en-us/library/dd162897%28VS.85%29.aspx"/>
        /// <remarks>
        /// By convention, the right and bottom edges of the rectangle are normally considered exclusive.
        /// In other words, the pixel whose coordinates are ( right, bottom ) lies immediately outside of the the rectangle.
        /// For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including,
        /// the right column and bottom row of pixels. This structure is identical to the RECTL structure.
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct RectStruct
        {
            /// <summary>
            /// The x-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Left;

            /// <summary>
            /// The y-coordinate of the upper-left corner of the rectangle.
            /// </summary>
            public int Top;

            /// <summary>
            /// The x-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Right;

            /// <summary>
            /// The y-coordinate of the lower-right corner of the rectangle.
            /// </summary>
            public int Bottom;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfoEx lpmi);
        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        internal delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RectStruct lprcMonitor, IntPtr dwData);

        public static void EnumMonitors()
        {
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumCallBack, IntPtr.Zero);
        }

        private static bool MonitorEnumCallBack(IntPtr hMonitor, IntPtr hdcMonitor, ref RectStruct lprcMonitor, IntPtr dwData)
        {
            MonitorInfoEx mon_info = new MonitorInfoEx();
            mon_info.Init();
            mon_info.Size =Marshal.SizeOf(mon_info);
            GetMonitorInfo(hMonitor, ref mon_info);
            ///Monitor info is stored in 'mon_info'
            return true;
        }
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, String message, String title, int type);



        public const string PluginName = "MicroLightPlugin";
 


        public class Simulator
        {
            [DllImport(PluginName, EntryPoint = "Create", CallingConvention = CallingConvention.Cdecl)]
            public static extern SimulatorIntPtr Create(HINSTANCE instance, HWND parenthwnd, bool showonprimary, bool fullscreen);

            [DllImport(PluginName, EntryPoint = "GetHwnd", CallingConvention = CallingConvention.Cdecl)]
            public static extern HWND GetHwnd(SimulatorIntPtr simulator);

            [DllImport(PluginName, EntryPoint = "SetRenderAPI", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetRenderAPI(SimulatorIntPtr simulator , RenderAPI render);
            
            [DllImport(PluginName, EntryPoint = "Destroy", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Destroy(SimulatorIntPtr simulator);
        }

        public class Render
        {
          
            [DllImport(PluginName, EntryPoint = "CreateRender", CallingConvention = CallingConvention.Cdecl)]
            public static extern RenderAPI CreateRender(GfxRenderer apiType);

            [DllImport(PluginName, EntryPoint = "InitRender", CallingConvention = CallingConvention.Cdecl)]
            public static extern void InitRender(RenderAPI render, HWND hwnd, DXGI_FORMAT format, int w, int h, bool stereo);

            [DllImport(PluginName, EntryPoint = "UpdateRender", CallingConvention = CallingConvention.Cdecl)]
            public static extern void UpdateRender(RenderAPI render);

            [DllImport(PluginName, EntryPoint = "RenderEye", CallingConvention = CallingConvention.Cdecl)]
            public static extern void RenderEye(RenderAPI render, int eyeindex);

            [DllImport(PluginName, EntryPoint = "RenderDestroy", CallingConvention = CallingConvention.Cdecl)]
            public static extern void RenderDestroy(RenderAPI render);

            [DllImport(PluginName, EntryPoint = "Present", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Present(RenderAPI render);

            [DllImport(PluginName, EntryPoint = "AutoFullScreen", CallingConvention = CallingConvention.Cdecl)]
            public static extern void AutoFullScreen(RenderAPI render);

            [DllImport(PluginName, EntryPoint = "FullScreenSwitch", CallingConvention = CallingConvention.Cdecl)]
            public static extern void FullScreenSwitch(RenderAPI render,bool PRIMARY);

            [DllImport(PluginName, EntryPoint = "GetRenderDevice", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11Device GetRenderDevice(RenderAPI render);

          
            [DllImport(PluginName, EntryPoint = "ClearViews", CallingConvention = CallingConvention.Cdecl)]
            public static extern void ClearViews(RenderAPI render);

            [DllImport(PluginName, EntryPoint = "AddView", CallingConvention = CallingConvention.Cdecl)]
            public static extern void AddView(RenderAPI render,
                                                                   int id,
                                                                   D3D11ShaderResourceView stereoleftview, 
                                                                    D3D11ShaderResourceView stereorightview, 
                                                                    float frustumwidthmultiple, 
                                                                    float frustumheightmultiple,
                                                                    float pivotx,
                                                                    float pivoty);


        }

        public class D3D11Helper
        {
            [DllImport(PluginName, EntryPoint = "CreateSharedView", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11Texture2D CreateSharedView(D3D11Device device, DXGI_FORMAT format, int w, int h);

            [DllImport(PluginName, EntryPoint = "GetSharedHandle", CallingConvention = CallingConvention.Cdecl)]
            public static extern HANDLE GetSharedHandle(D3D11Device device, D3D11Texture2D texture);

            [DllImport(PluginName, EntryPoint = "SubmitSharedView", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SubmitSharedView(D3D11Device device, RednerTextureIntPtr source, D3D11ShaderResourceView resource);

            [DllImport(PluginName, EntryPoint = "OpenSharedResource", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11Resource OpenSharedResource(D3D11Device device, HANDLE handle);

            [DllImport(PluginName, EntryPoint = "CreateShaderResourceView", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11ShaderResourceView CreateShaderResourceView(D3D11Device device, D3D11Resource resource);

            [DllImport(PluginName, EntryPoint = "CreateShaderResourceViewFromFile", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11ShaderResourceView CreateShaderResourceViewFromFile(D3D11Device device, [MarshalAs(UnmanagedType.LPStr)] string file);

            [DllImport(PluginName, EntryPoint = "CreateShaderResourceViewFromRenderTexture", CallingConvention = CallingConvention.Cdecl)]
            public static extern D3D11ShaderResourceView CreateShaderResourceViewFromRenderTexture(D3D11Device device, RednerTextureIntPtr source, DXGI_FORMAT format);
 
        }
 
        public class HoloGraphicUtilities
        {
            [DllImport(PluginName, EntryPoint = "GetCalculateFrustumMatrix", CallingConvention = CallingConvention.Cdecl)]
            public static extern HoloGraphicFrustum GetCalculateFrustumMatrix(float nearClipPlane, float farClipPlane, Vector3 Corner0, Vector3 Corner1, Vector3 Corner2, Vector3 Corner3, Vector3 pe);
 

            [DllImport(PluginName, EntryPoint = "GetDisPlayScale", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetDisPlayScale();

            [DllImport(PluginName, EntryPoint = "GetDisPlayMode", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetDisPlayMode();

            [DllImport(PluginName, EntryPoint = "GetTextureSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetTextureSize();

            [DllImport(PluginName, EntryPoint = "GetPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetPlayAeraSize();

            [DllImport(PluginName, EntryPoint = "SetPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetPlayAeraSize(Vector3 size);

            [DllImport(PluginName, EntryPoint = "GetTrapezoidRoomSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern TrapezoidRoomSize GetTrapezoidRoomSize();

            [DllImport(PluginName, EntryPoint = "SetTrapezoidRoomSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetTrapezoidRoomSize(TrapezoidRoomSize size);

            [DllImport(PluginName, EntryPoint = "GetProjectionFaceInclination", CallingConvention = CallingConvention.Cdecl)]
            public static extern float GetProjectionFaceInclination();

            [DllImport(PluginName, EntryPoint = "SetProjectionFaceInclination", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetProjectionFaceInclination(float angle);

            [DllImport(PluginName, EntryPoint = "GetDesktopPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetDesktopPlayAeraSize();

            [DllImport(PluginName, EntryPoint = "SetDesktopPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetDesktopPlayAeraSize(Vector3 size);

            [DllImport(PluginName, EntryPoint = "GetLTypePlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector4 GetLTypePlayAeraSize();

            [DllImport(PluginName, EntryPoint = "SetLTypePlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetLTypePlayAeraSize(Vector4 size);

            [DllImport(PluginName, EntryPoint = "GetTrapezoidRoomPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector4 GetTrapezoidRoomPlayAeraSize();

            [DllImport(PluginName, EntryPoint = "SetTrapezoidRoomPlayAeraSize", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetTrapezoidRoomPlayAeraSize(Vector4 size);

            [DllImport(PluginName, EntryPoint = "GetTrackingDeviceType", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrackingDeviceType();

            [DllImport(PluginName, EntryPoint = "GetTrackingDeviceSerialnumber", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.LPWStr)]
            public static extern string GetTrackingDeviceSerialnumber(int id);

            [DllImport(PluginName, EntryPoint = "GetTrackingDeviceSerialnumberLen", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetTrackingDeviceSerialnumberLen(int id);

            [DllImport(PluginName, EntryPoint = "SetTrackingDeviceSerialnumber", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetTrackingDeviceSerialnumber(int id, [MarshalAs(UnmanagedType.LPWStr)] string serialnumber);

            [DllImport(PluginName, EntryPoint = "GetDisPlaySizeScale", CallingConvention = CallingConvention.Cdecl)]
            public static extern float GetDisPlaySizeScale();

            [DllImport(PluginName, EntryPoint = "SetDisPlaySizeScale", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetDisPlaySizeScale(float SizeScale);

            [DllImport(PluginName, EntryPoint = "GetIRTrackingPositionOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetIRTrackingPositionOffset(int id);

            [DllImport(PluginName, EntryPoint = "SetIRTrackingPositionOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetIRTrackingPositionOffset(int id, Vector3 pos);

            [DllImport(PluginName, EntryPoint = "GetIRTrackingEluerAnglesOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetIRTrackingEluerAnglesOffset(int id);

            [DllImport(PluginName, EntryPoint = "SetIRTrackingEluerAnglesOffset", CallingConvention = CallingConvention.Cdecl)]
            public static extern void SetIRTrackingEluerAnglesOffset(int id, Vector3 angles);

        }

        public class Tracker
        {
            [DllImport(PluginName, EntryPoint = "CreateTracker", CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr CreateTracker(TrackingDeviceType deviceType, [MarshalAs(UnmanagedType.LPWStr)] string file);

            [DllImport(PluginName, EntryPoint = "DestroyTracker", CallingConvention = CallingConvention.Cdecl)]
            public static extern void DestroyTracker(IntPtr tracker);

            [DllImport(PluginName, EntryPoint = "Init", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool Init(IntPtr tracker);

            [DllImport(PluginName, EntryPoint = "Reset", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool Reset(IntPtr tracker);

            [DllImport(PluginName, EntryPoint = "Shutdown", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool Shutdown(IntPtr tracker);
 
            [DllImport(PluginName, EntryPoint = "GetActiveDeviceCount", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActiveDeviceCount(IntPtr tracker);
 
            [DllImport(PluginName, EntryPoint = "GetActiveDeviceId", CallingConvention = CallingConvention.Cdecl)]
            public static extern int GetActiveDeviceId(IntPtr tracker, int index);

            [DllImport(PluginName, EntryPoint = "IsActive", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool IsActive(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);

            [DllImport(PluginName, EntryPoint = "ReCenter", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool ReCenter(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);


            [DllImport(PluginName, EntryPoint = "GetPosition", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetPosition(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);

            [DllImport(PluginName, EntryPoint = "GetRotation", CallingConvention = CallingConvention.Cdecl)]
            public static extern Quaternion GetRotation(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);


            [DllImport(PluginName, EntryPoint = "GetButtonDown", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool GetButtonDown(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber, int key);

            [DllImport(PluginName, EntryPoint = "GetButtonPressed", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool GetButtonPressed(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber, int key);

            [DllImport(PluginName, EntryPoint = "GetButtonUp", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool GetButtonUp(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber, int key);

            [DllImport(PluginName, EntryPoint = "GetButtonAxis1D", CallingConvention = CallingConvention.Cdecl)]
            public static extern float GetButtonAxis1D(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber, int key);

            [DllImport(PluginName, EntryPoint = "GetButtonAxis2D", CallingConvention = CallingConvention.Cdecl)]
            public static extern Vector3 GetButtonAxis2D(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber, int key);

            [DllImport(PluginName, EntryPoint = "GetConnectTrackingDeviceSerialnumber", CallingConvention = CallingConvention.Cdecl)]
            [return: MarshalAs(UnmanagedType.LPWStr)]
            public static extern string GetConnectTrackingDeviceSerialnumber(IntPtr tracker, int index);

            [DllImport(PluginName, EntryPoint = "GetControllerPose", CallingConvention = CallingConvention.Cdecl)]
            public static extern TrackedDevicePose GetControllerPose(IntPtr tracker,int origin, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);

            [DllImport(PluginName, EntryPoint = "GetControllerState", CallingConvention = CallingConvention.Cdecl)]
            public static extern VRControllerState GetControllerState(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber);

            [DllImport(PluginName, EntryPoint = "HmdCenterCalibration", CallingConvention = CallingConvention.Cdecl)]
            public static extern void HmdCenterCalibration(IntPtr tracker, [MarshalAs(UnmanagedType.LPStr)] string serialnumber,Vector3 sizexz, TrackedDevicePose pose);

        }



       

    
        public class Configuration
        {
            [DllImport(PluginName, EntryPoint = "SetAuthorization", CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SetAuthorization([MarshalAs(UnmanagedType.LPStr)] string appid, [MarshalAs(UnmanagedType.LPStr)] string appkey, [MarshalAs(UnmanagedType.LPStr)] string code ,bool isEditor);


        }
    }
    public static class Calibration
    {
        private static float _copysign(float sizeval, float signval)
        {
            return Mathf.Sign(signval) == 1 ? Mathf.Abs(sizeval) : -Mathf.Abs(sizeval);
        }

        public static Matrix4x4 GetMatrix(HmdMatrix34 pose)
        {
            var m = Matrix4x4.identity;

            m[0, 0] = pose.m0;
            m[0, 1] = pose.m1;
            m[0, 2] = -pose.m2;
            m[0, 3] = pose.m3;

            m[1, 0] = pose.m4;
            m[1, 1] = pose.m5;
            m[1, 2] = -pose.m6;
            m[1, 3] = pose.m7;

            m[2, 0] = -pose.m8;
            m[2, 1] = -pose.m9;
            m[2, 2] = pose.m10;
            m[2, 3] = -pose.m11;

            return m;
        }
        public static Vector3 GetPosition(Matrix4x4 matrix)
        {
            var x =- matrix.m03;
            var y = matrix.m13;
            var z =- matrix.m23;

            return new Vector3(x, y, z);
        }
        public static Quaternion GetRotation(Matrix4x4 matrix)
        {
            Quaternion q = new Quaternion();
            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 + matrix.m11 + matrix.m22)) / 2;
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m00 - matrix.m11 - matrix.m22)) / 2;
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 + matrix.m11 - matrix.m22)) / 2;
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m00 - matrix.m11 + matrix.m22)) / 2;
            q.x = _copysign(q.x, matrix.m21 - matrix.m12);
            q.y =_copysign(q.y, matrix.m02 - matrix.m20);
            q.z = _copysign(q.z, matrix.m10 - matrix.m01);
            return q;
        }
    }
}