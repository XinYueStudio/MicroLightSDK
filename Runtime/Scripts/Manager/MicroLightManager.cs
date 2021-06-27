using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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
using UnityEngine.Experimental.Rendering;
using System.Runtime.InteropServices;
using System.Threading;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MicroLight
{
    [AddComponentMenu("MicroLight/MicroLightManager")]
    [ExecuteInEditMode, RequireComponent(typeof(Transform))]
    public class MicroLightManager : MonoSingletonBase<MicroLightManager>
    {
        public SimulatorIntPtr mSimulator = IntPtr.Zero;
        public HWND mSimulatorHwnd = IntPtr.Zero;
        public HINSTANCE m_HINSTANCE = IntPtr.Zero;
        public RenderAPI m_RenderAPI = IntPtr.Zero;
        public CoverRenderAPI m_CoverRenderAPI = IntPtr.Zero;
        public RenderAPI m_UnityRenderAPI = IntPtr.Zero;
        public D3D11Device m_Device = IntPtr.Zero;
        public D3D11Device m_UnityDevice = IntPtr.Zero;

        public string appid="";
        public string appkey="";
        //[HideInInspector]
        public string code="";


        [Header("渲染内核选择")]
        [Tooltip("渲染内核选择")]
        public RendererType mRenderType = RendererType.MicroLightRender;
        [Header("投影模式选择")]
        [Tooltip("投影模式选择")]
        public ProjectionMode mProjectionMode = ProjectionMode.Surface;
        [HideInInspector]
        public ProjectionMode TempmProjectionMode = ProjectionMode.Surface;

        [Header("渲染模式选择")]
        [Tooltip("渲染模式选择")]
        public RenderMode mRenderMode = RenderMode.LR3D;

        [Header("是否自动全屏")]
        [Tooltip("是否自动全屏")]
        public bool AutoFullScreen = false;
        [Header("全屏是否显示在主屏幕")]
        [Tooltip("全屏是否显示在主屏幕")]
        public bool ShowOnPRIMARY = false;


       // [HideInInspector]
        [Header("单眼单帧渲染图像大小")]
        [Tooltip("单眼单帧渲染图像大小")]
        public Vector2 mRenderTextureSize = new Vector2(3840, 2160);
        [HideInInspector]
        [Header("投影区域参数")]
        [Tooltip("投影区域参数")]
        public PlayAeraSize mPlayAeraSize;


        public MicroLightPlayAera SurfacePlayAera=null;

        public MicroLightPlayAera LTypeRoomFrontPlayAera = null;
        public MicroLightPlayAera LTypeRoomFloorPlayAera = null;

        public MicroLightPlayAera TrapezoidRoomLeftPlayAera = null;
        public MicroLightPlayAera TrapezoidRoomFrontPlayAera = null;
        public MicroLightPlayAera TrapezoidRoomRightPlayAera = null;
        public MicroLightPlayAera TrapezoidRoomFloorPlayAera = null;
        [Header("投影区域绘画参数")]
        [Tooltip("投影区域绘画参数")]
        public Color PlayAeraDrawGridColor;
        public int PlayAeraEditorSection = 40;
        public Color PlayAeraDrawGridOutlineColor;
        public bool PlayAeraEditorDrawGrid = true;
        public bool PlayAeraRuntimeDrawGrid = false;
        [Range(0, 1)]
        public float PlayAeraRuntimeDrawGridStep = 0.01f;
       [Range(0,50)]
        public int PlayAeraRuntimeDrawGridVerticalCount = 1;
        [Range(0, 50)]
        public int PlayAeraRuntimeDrawGridHorizontalCount = 1;

        /// <summary>
        /// 这是其中唯一的玩家，多人的时候对应处理
        /// </summary>
        //[HideInInspector]
        public MicroLightCameraRig mCameraRig=null;

        [Header("两眼之间的间隔")]
        [Tooltip("两眼之间的间隔")]
        [Range(0,0.50F)]
        public float PDI = 0.064F;
        [Header("是否打开定位追踪")]
        [Tooltip("是否打开定位追踪")]
        public bool OpenTracker = true;

        [HideInInspector]
        [Header("定位系统类型")]
        [Tooltip("定位系统类型")]
        public TrackingDeviceType deviceType;
        public IntPtr mTracker=IntPtr.Zero;

        bool HadAuthorization = false;

  
        void InitTracker()
        {
            if (OpenTracker)
            {
                if (mTracker == IntPtr.Zero)
                {
                    string dllfilepath = Application.dataPath + "\\Plugins\\x86_64\\ImageDll.dll";
                    if (Application.isEditor)
                    {
                        dllfilepath = Application.dataPath + "\\MicroLightSDK\\Runtime\\Plugins\\x86_64\\ImageDll.dll";
                       // UnityEngine.Debug.Log(dllfilepath);
                    }
                    deviceType = (TrackingDeviceType)MicroLightPlugin.HoloGraphicUtilities.GetTrackingDeviceType();
                    mTracker = MicroLightPlugin.Tracker.CreateTracker(deviceType, dllfilepath);
                }
                if (mTracker != IntPtr.Zero)
                {
                    bool result = MicroLightPlugin.Tracker.Init(mTracker);

                    if (result)
                    {
                        UnityEngine.Debug.Log("Tracker Init Success");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("Tracker Init Failed");
                    }
                }
            }
        }

        void DestroyTracker()
        {
            if (OpenTracker)
            {
                if (mTracker != IntPtr.Zero)
                {
                    MicroLightPlugin.Tracker.Shutdown(mTracker);
                    MicroLightPlugin.Tracker.DestroyTracker(mTracker);
                    mTracker = IntPtr.Zero;
                }
            }
        }

        public bool CheckAuthorization = true;
        /// <summary>
        /// 获取运行授权
        /// </summary>
        void Authorization()
        {
 
            if ( !Application.isEditor)
            {
 
                string CommandLine = Environment.CommandLine;
                string[] CommandLineArgs = Environment.GetCommandLineArgs();

               // MicroLightPlugin.MessageBox(IntPtr.Zero,CommandLineArgs[1], "MicroLight:appid", 0);
               // MicroLightPlugin.MessageBox(IntPtr.Zero, CommandLineArgs[2], "MicroLight:appkey", 0);
                //MicroLightPlugin.MessageBox(IntPtr.Zero, CommandLineArgs[3], "MicroLight:code", 0);

                if (CommandLineArgs.Length >3)
                {
                    if (CommandLineArgs[1]!=null&& CommandLineArgs[1] != "")
                   {
                        appid = CommandLineArgs[1];
                   }

                    if (CommandLineArgs[2] != null && CommandLineArgs[2] != "")
                    {
                        appkey = CommandLineArgs[2];
                    }
                    if (CommandLineArgs[3] != null && CommandLineArgs[3] != "")
                    {
                        code = CommandLineArgs[3];
                    }
                    
                }
            }

            //MicroLightPlugin.MessageBox(IntPtr.Zero, "appid:" + appid + " appkey:" + appkey + " code:" + code, "MicroLight", 0);
            HadAuthorization =MicroLightPlugin.Configuration.SetAuthorization(appid, appkey, code,Application.isEditor);

        }



        void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
           
          
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);

                //获取运行授权
                Authorization();

                InitTracker();


                switch (mRenderType)
                {

                    case RendererType.MicroLightRender:
                    case RendererType.UnityRender:
                        Process curProcess = Process.GetCurrentProcess();

                        m_HINSTANCE = MicroLightPlugin.GetModuleHandle(curProcess.MainModule.ModuleName);

                        if (m_HINSTANCE != IntPtr.Zero)
                        {
                            mSimulator = MicroLightPlugin.Simulator.Create(m_HINSTANCE, curProcess.MainWindowHandle,ShowOnPRIMARY, AutoFullScreen);
                            mSimulatorHwnd = MicroLightPlugin.Simulator.GetHwnd(mSimulator);
                        }

                        break;

                    case RendererType.UnityCoverRender:

                        break;
                }
                Vector3 TextureSize = MicroLightPlugin.HoloGraphicUtilities.GetTextureSize();
                mRenderTextureSize.x = TextureSize.x;
                mRenderTextureSize.y = TextureSize.y;

                //Scale Kernel RenderTarget Size
                switch (MicroLightManager.Instance.mRenderMode)
                {
                    case RenderMode.LR3D:
                        switch (mProjectionMode)
                        {
                            case ProjectionMode.Surface:
                                TextureSize.x = TextureSize.x * 2;
                                break;
                            case ProjectionMode.LTypeRoom:
                                TextureSize.x = TextureSize.x * 4;
                                break;
                            case ProjectionMode.TrapezoidRoom:
                                TextureSize.x = TextureSize.x * 8;
                                break;
                        }
                        break;
                    case RenderMode.Stereo:
                        switch (mProjectionMode)
                        {
                            case ProjectionMode.Surface:
                                TextureSize.x = TextureSize.x * 2;
                                break;
                            case ProjectionMode.LTypeRoom:
                                TextureSize.x = TextureSize.x * 2;
                                break;
                            case ProjectionMode.TrapezoidRoom:
                                TextureSize.x = TextureSize.x * 4;
                                break;
                        }
                        break;
                }

                switch (mRenderType)
                {

                    case RendererType.MicroLightRender:
                        if (mSimulator != IntPtr.Zero)
                        {

                            m_RenderAPI = MicroLightPlugin.Render.CreateRender(GfxRenderer.D3D11_0);
                            if (m_RenderAPI == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_RenderAPI is null");
                            }
                      
                            MicroLightPlugin.Render.InitRender(m_RenderAPI, mSimulatorHwnd, DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, (int)TextureSize.x, (int)TextureSize.y, mRenderMode == RenderMode.Stereo);
                            MicroLightPlugin.Simulator.SetRenderAPI(mSimulator, m_RenderAPI);

                            m_Device = MicroLightPlugin.Render.GetRenderDevice(m_RenderAPI);
                            if (m_Device == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_Device is null");
                            }

                            m_UnityRenderAPI = UnityNativePlugin.Render.CreateRender(GfxRenderer.D3D11_0);
                            if (m_UnityRenderAPI == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_UnityRenderAPI is null");
                            }
                            m_UnityDevice = UnityNativePlugin.Render.GetRenderDevice(m_UnityRenderAPI);
                            if (m_UnityDevice == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_UnityDevice is null");
                            }



                            if (AutoFullScreen&& mRenderMode != RenderMode.Stereo)
                            {
                                    MicroLightPlugin.Render.FullScreenSwitch(m_RenderAPI, ShowOnPRIMARY);

                            }
                            
                        }
                        break;
                    case RendererType.UnityRender:
                        if (mSimulator != IntPtr.Zero)
                        {
 
                            m_UnityRenderAPI = UnityNativePlugin.Render.CreateRender(GfxRenderer.D3D11_0);
                            if (m_UnityRenderAPI == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_UnityRenderAPI is null");
                            }
                            MicroLightPlugin.Render.InitRender(m_UnityRenderAPI, mSimulatorHwnd, DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, Screen.width, Screen.height, mRenderMode== RenderMode.Stereo);
                            MicroLightPlugin.Simulator.SetRenderAPI(mSimulator, m_UnityRenderAPI);

                            m_UnityDevice = MicroLightPlugin.Render.GetRenderDevice(m_UnityRenderAPI);
                            if (m_UnityDevice == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_UnityDevice is null");
                            }

                            if (AutoFullScreen && mRenderMode != RenderMode.Stereo)
                            {


                                MicroLightPlugin.Render.FullScreenSwitch(m_UnityRenderAPI, ShowOnPRIMARY);

                            }
                        }

                        break;

                    case RendererType.UnityCoverRender:
                        {
                            m_UnityRenderAPI = UnityNativePlugin.Render.CreateRender(GfxRenderer.D3D11_0);
                            if (m_UnityRenderAPI == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_UnityRenderAPI is null");
                            }

                            m_CoverRenderAPI = UnityNativePlugin.CoverRender.Create(m_UnityRenderAPI, Screen.width, Screen.height);
                            if (m_CoverRenderAPI == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_CoverRenderAPI is null");
                            }
                            m_UnityDevice = UnityNativePlugin.Render.GetRenderDevice(m_UnityRenderAPI);
                            if (m_Device == IntPtr.Zero)
                            {
                                UnityEngine.Debug.Log("m_Device is null");
                            }
                            if (AutoFullScreen && mRenderMode != RenderMode.Stereo)
                            {


                                UnityNativePlugin.Render.FullScreenSwitch(m_UnityRenderAPI, ShowOnPRIMARY);
                               
                            }
                        }
                        break;
                }

                
            }

            UpdateBindings();

        }
 
        public  void UpdateBindings()
        {
            //Get PlayAera Size by Native API
            mPlayAeraSize.mSurfaceSize = MicroLightPlugin.HoloGraphicUtilities.GetPlayAeraSize(); 
            mPlayAeraSize.mTrapezoidRoomSize = MicroLightPlugin.HoloGraphicUtilities.GetTrapezoidRoomSize();
            mPlayAeraSize.ProjectionFaceInclination = MicroLightPlugin.HoloGraphicUtilities.GetProjectionFaceInclination();

            switch (mProjectionMode)
            {
                case ProjectionMode.Surface:
                    {
                        //SurfacePlayAera

                        if (LTypeRoomFrontPlayAera)
                        {
                            DestroyImmediate(LTypeRoomFrontPlayAera.gameObject);
                            LTypeRoomFrontPlayAera = null;
                        }
                        if (LTypeRoomFloorPlayAera)
                        {
                            DestroyImmediate(LTypeRoomFloorPlayAera.gameObject);
                            LTypeRoomFloorPlayAera = null;
                        }
                        if (TrapezoidRoomLeftPlayAera)
                        {
                            DestroyImmediate(TrapezoidRoomLeftPlayAera.gameObject);
                            TrapezoidRoomLeftPlayAera = null;
                        }
                        if (TrapezoidRoomFrontPlayAera)
                        {
                            DestroyImmediate(TrapezoidRoomFrontPlayAera.gameObject);
                            TrapezoidRoomFrontPlayAera = null;
                        }
                        if (TrapezoidRoomRightPlayAera)
                        {
                            DestroyImmediate(TrapezoidRoomRightPlayAera.gameObject);
                            TrapezoidRoomRightPlayAera = null;
                        }
                        if (TrapezoidRoomFloorPlayAera)
                        {
                            DestroyImmediate(TrapezoidRoomFloorPlayAera.gameObject);
                            TrapezoidRoomFloorPlayAera = null;
                        }


                        if (SurfacePlayAera == null)
                        {
                            MicroLightPlayAera[] mTransformS = transform.GetComponentsInChildren<MicroLightPlayAera>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }

                            SurfacePlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                            SurfacePlayAera.transform.parent = transform;
                            SurfacePlayAera.id = TrackingSpaceId.Surface;

                         
                        }
                        if (SurfacePlayAera)
                        {
                            SurfacePlayAera.transform.localPosition = Vector3.zero;
                            SurfacePlayAera.transform.localEulerAngles = new Vector3(-mPlayAeraSize.ProjectionFaceInclination, 0, 0);
                            if (mPlayAeraSize.mSurfaceSize.x != 0 && mPlayAeraSize.mSurfaceSize.y != 0)
                            {
                                SurfacePlayAera.transform.localScale = new Vector3(mPlayAeraSize.mSurfaceSize.x, mPlayAeraSize.mSurfaceSize.y, mPlayAeraSize.mSurfaceSize.y);
                            }
                        }

                        if (mCameraRig&& TempmProjectionMode!= mProjectionMode)
                        {
                            DestroyImmediate(mCameraRig.gameObject);
                        }
                        //mCameraRig
                        if (mCameraRig == null)
                        {
                            MicroLightCameraRig[] mTransformS = transform.GetComponentsInChildren<MicroLightCameraRig>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }
                            mCameraRig = new GameObject("MicroLightCameraRig").AddComponent<MicroLightCameraRig>();

                            mCameraRig.transform.parent = transform;
                            mCameraRig.id = 0;
                            mCameraRig.transform.localPosition = Vector3.zero;
                            mCameraRig.transform.localEulerAngles = Vector3.zero;
                            mCameraRig.transform.localScale = Vector3.one;

                            MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpace").AddComponent<MicroLightTrackingSpace>();
                            mTrackingSpace.transform.parent = mCameraRig.transform;
                            mTrackingSpace.id = TrackingSpaceId.Surface;
                            mTrackingSpace.transform.localPosition = Vector3.zero;
                            mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                            mTrackingSpace.transform.localScale = Vector3.one;
                            mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                            MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                            mLeftEye.transform.parent = mTrackingSpace.transform;
                            mLeftEye.mEyeTye = EyeTye.Left;
                            mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                            mLeftEye.transform.localEulerAngles = Vector3.zero;
                            mLeftEye.transform.localScale = Vector3.one;
                            mTrackingSpace.LeftEye = mLeftEye;

                            MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                            mRightEye.transform.parent = mTrackingSpace.transform;
                            mRightEye.mEyeTye = EyeTye.Right;
                            mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                            mRightEye.transform.localEulerAngles = Vector3.zero;
                            mRightEye.transform.localScale = Vector3.one;
                            mTrackingSpace.RightEye = mRightEye;
                        }
                   

                    }
                    break;
                case ProjectionMode.LTypeRoom:
                    {
                        //PlayAera
                        {
                            if (SurfacePlayAera)
                            {
                                DestroyImmediate(SurfacePlayAera.gameObject);
                                SurfacePlayAera = null;
                            }

                            if (TrapezoidRoomLeftPlayAera)
                            {
                                DestroyImmediate(TrapezoidRoomLeftPlayAera.gameObject);
                                TrapezoidRoomLeftPlayAera = null;
                            }
                            if (TrapezoidRoomFrontPlayAera)
                            {
                                DestroyImmediate(TrapezoidRoomFrontPlayAera.gameObject);
                                TrapezoidRoomFrontPlayAera = null;
                            }
                            if (TrapezoidRoomRightPlayAera)
                            {
                                DestroyImmediate(TrapezoidRoomRightPlayAera.gameObject);
                                TrapezoidRoomRightPlayAera = null;
                            }
                            if (TrapezoidRoomFloorPlayAera)
                            {
                                DestroyImmediate(TrapezoidRoomFloorPlayAera.gameObject);
                                TrapezoidRoomFloorPlayAera = null;
                            }

                            MicroLightPlayAera[] mTransformS = transform.GetComponentsInChildren<MicroLightPlayAera>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }

                            if (LTypeRoomFrontPlayAera == null)
                            {

                                LTypeRoomFrontPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                LTypeRoomFrontPlayAera.transform.parent = transform;
                                LTypeRoomFrontPlayAera.id = TrackingSpaceId.LTypeRoomFront;
                                LTypeRoomFrontPlayAera.transform.localPosition = Vector3.zero;
                                LTypeRoomFrontPlayAera.transform.localEulerAngles = new Vector3(-90, 0, 0);
                                if (mPlayAeraSize.mSurfaceSize.x != 0 && mPlayAeraSize.mSurfaceSize.y != 0)
                                {
                                    LTypeRoomFrontPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mSurfaceSize.x, mPlayAeraSize.mSurfaceSize.y, mPlayAeraSize.mSurfaceSize.y);
                                }

                              

                            }
                            if (LTypeRoomFloorPlayAera == null)
                            {

                                LTypeRoomFloorPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                LTypeRoomFloorPlayAera.transform.parent = transform;
                                LTypeRoomFloorPlayAera.id = TrackingSpaceId.LTypeRoomFloor;
                                LTypeRoomFloorPlayAera.transform.localPosition = new Vector3(0, -mPlayAeraSize.mSurfaceSize.y * 0.5f, -mPlayAeraSize.mSurfaceSize.y * 0.5f);
                                LTypeRoomFloorPlayAera.transform.localEulerAngles = Vector3.zero;
                                if (mPlayAeraSize.mSurfaceSize.x != 0 && mPlayAeraSize.mSurfaceSize.y != 0)
                                {
                                    LTypeRoomFloorPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mSurfaceSize.x, mPlayAeraSize.mSurfaceSize.y, mPlayAeraSize.mSurfaceSize.y);
                                }

                            
                            }
                        }

                        if (mCameraRig && TempmProjectionMode != mProjectionMode)
                        {
                            DestroyImmediate(mCameraRig.gameObject);
                        }
                        //mCameraRig
                        if (mCameraRig == null)
                        {
                            MicroLightCameraRig[] mTransformS = transform.GetComponentsInChildren<MicroLightCameraRig>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }
                            mCameraRig = new GameObject("MicroLightCameraRig").AddComponent<MicroLightCameraRig>();

                            mCameraRig.transform.parent = transform;
                            mCameraRig.id = 0;
                            mCameraRig.transform.localPosition = Vector3.zero;
                            mCameraRig.transform.localEulerAngles = Vector3.zero;
                            mCameraRig.transform.localScale = Vector3.one;
                            //TrackingSpaceFront
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceFront").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
                                mTrackingSpace.id = TrackingSpaceId.LTypeRoomFront;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }
                            //TrackingSpaceFLoor
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceFLoor").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
                                mTrackingSpace.id = TrackingSpaceId.LTypeRoomFloor;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }
                        }
                  
                    }
                    break;
                case ProjectionMode.TrapezoidRoom:
                    {

                        { 
                            if (SurfacePlayAera)
                            {
                                DestroyImmediate(SurfacePlayAera.gameObject);
                                SurfacePlayAera = null;
                            }
                            if (LTypeRoomFrontPlayAera)
                            {
                                DestroyImmediate(LTypeRoomFrontPlayAera.gameObject);
                                LTypeRoomFrontPlayAera = null;
                            }
                            if (LTypeRoomFloorPlayAera)
                            {
                                DestroyImmediate(LTypeRoomFloorPlayAera.gameObject);
                                LTypeRoomFloorPlayAera = null;
                            }


                            MicroLightPlayAera[] mTransformS = transform.GetComponentsInChildren<MicroLightPlayAera>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }

                            if (TrapezoidRoomLeftPlayAera == null)
                            {

                                TrapezoidRoomLeftPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                TrapezoidRoomLeftPlayAera.transform.parent = transform;
                                TrapezoidRoomLeftPlayAera.id = TrackingSpaceId.TrapezoidRoomLeft;
                                float Angles = mPlayAeraSize.mTrapezoidRoomSize.IntersectionAngle - 90;
                                float x = Mathf.Sin(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;
                                float z = Mathf.Cos(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;
                                TrapezoidRoomLeftPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mTrapezoidRoomSize.LeftWidth, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight);
                                TrapezoidRoomLeftPlayAera.transform.localPosition = new Vector3(-((x) + mPlayAeraSize.mTrapezoidRoomSize.FrontWidth * 0.5f), 0, -z);
                                TrapezoidRoomLeftPlayAera.transform.localEulerAngles = new Vector3(-90, -(90 - Angles), 0);

                            }
                            if (TrapezoidRoomFrontPlayAera == null)
                            {
                                TrapezoidRoomFrontPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                TrapezoidRoomFrontPlayAera.transform.parent = transform;
                                TrapezoidRoomFrontPlayAera.id = TrackingSpaceId.TrapezoidRoomFront;
                                TrapezoidRoomFrontPlayAera.transform.localPosition = Vector3.zero;
                                TrapezoidRoomFrontPlayAera.transform.localEulerAngles = new Vector3(-90, 0, 0);
                                TrapezoidRoomFrontPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mTrapezoidRoomSize.FrontWidth, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight);
                                
                            }
                            if (TrapezoidRoomRightPlayAera == null)
                            {
                                TrapezoidRoomRightPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                TrapezoidRoomRightPlayAera.transform.parent = transform;
                                TrapezoidRoomRightPlayAera.id = TrackingSpaceId.TrapezoidRoomRight;

                                float Angles = mPlayAeraSize.mTrapezoidRoomSize.IntersectionAngle - 90;
                                float x = Mathf.Sin(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;
                                float z = Mathf.Cos(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;
                                TrapezoidRoomRightPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mTrapezoidRoomSize.LeftWidth, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight);
                                TrapezoidRoomRightPlayAera.transform.localPosition = new Vector3(((x) + mPlayAeraSize.mTrapezoidRoomSize.FrontWidth * 0.5f), 0, -z);
                                TrapezoidRoomRightPlayAera.transform.localEulerAngles = new Vector3(-90, (90 - Angles), 0);
                              
                            }
                            if (TrapezoidRoomFloorPlayAera == null)
                            {
                                TrapezoidRoomFloorPlayAera = new GameObject("MicroLightPlayAera").AddComponent<MicroLightPlayAera>();

                                TrapezoidRoomFloorPlayAera.transform.parent = transform;
                                TrapezoidRoomFloorPlayAera.id = TrackingSpaceId.TrapezoidRoomFloor;

                                float Angles = mPlayAeraSize.mTrapezoidRoomSize.IntersectionAngle - 90;
                                float x = Mathf.Sin(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;
                                float z = Mathf.Cos(Angles * Mathf.Deg2Rad) * mPlayAeraSize.mTrapezoidRoomSize.LeftWidth * 0.5f;

                                TrapezoidRoomFloorPlayAera.transform.localScale = new Vector3(mPlayAeraSize.mTrapezoidRoomSize.LeftWidth + x*4, mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight, z*2);
                                TrapezoidRoomFloorPlayAera.transform.localPosition = new Vector3(0, -mPlayAeraSize.mTrapezoidRoomSize.GlobalHeight * 0.5f, -z);
                                TrapezoidRoomFloorPlayAera.transform.localEulerAngles = new Vector3(0, 0, 0);
                              
                            }
                        }




                        if (mCameraRig && TempmProjectionMode != mProjectionMode)
                        {
                            DestroyImmediate(mCameraRig.gameObject);
                        }
                        //mCameraRig
                        if (mCameraRig == null)
                        {
                            MicroLightCameraRig[] mTransformS = transform.GetComponentsInChildren<MicroLightCameraRig>();
                            for (int i = 0; i < mTransformS.Length; i++)
                            {
                                DestroyImmediate(mTransformS[i].gameObject);
                            }
                            mCameraRig = new GameObject("MicroLightCameraRig").AddComponent<MicroLightCameraRig>();

                            mCameraRig.transform.parent = transform;
                            mCameraRig.id = 0;
                            mCameraRig.transform.localPosition = Vector3.zero;
                            mCameraRig.transform.localEulerAngles = Vector3.zero;
                            mCameraRig.transform.localScale = Vector3.one;
                            //TrackingSpaceLeft
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceLeft").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
                                mTrackingSpace.id = TrackingSpaceId.TrapezoidRoomLeft;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }

                            //TrackingSpaceFront
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceFront").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
                                mTrackingSpace.id = TrackingSpaceId.TrapezoidRoomFront;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }
                            //TrackingSpaceRight
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceRight").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
                                mTrackingSpace.id = TrackingSpaceId.TrapezoidRoomRight;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }
                            //TrackingSpaceFLoor
                            {
                                MicroLightTrackingSpace mTrackingSpace = new GameObject("TrackingSpaceFLoor").AddComponent<MicroLightTrackingSpace>();
                                mTrackingSpace.transform.parent = mCameraRig.transform;
 
                                mTrackingSpace.id = TrackingSpaceId.TrapezoidRoomFloor;
                                mTrackingSpace.transform.localPosition = Vector3.zero;
                                mTrackingSpace.transform.localEulerAngles = Vector3.zero;
                                mTrackingSpace.transform.localScale = Vector3.one;
                                mCameraRig.mTrackingSpaces.Add(mTrackingSpace);


                                MicroLightCamera mLeftEye = new GameObject("LeftEye").AddComponent<MicroLightCamera>();
                                mLeftEye.transform.parent = mTrackingSpace.transform;
                                mLeftEye.mEyeTye = EyeTye.Left;
                                mLeftEye.transform.localPosition = new Vector3(-PDI * 0.5F, 0, 0);
                                mLeftEye.transform.localEulerAngles = Vector3.zero;
                                mLeftEye.transform.localScale = Vector3.one;
                                mTrackingSpace.LeftEye = mLeftEye;

                                MicroLightCamera mRightEye = new GameObject("RightEye").AddComponent<MicroLightCamera>();
                                mRightEye.transform.parent = mTrackingSpace.transform;
                                mRightEye.mEyeTye = EyeTye.Right;
                                mRightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
                                mRightEye.transform.localEulerAngles = Vector3.zero;
                                mRightEye.transform.localScale = Vector3.one;
                                mTrackingSpace.RightEye = mRightEye;
                            }
                        }


                    }

                    break;
            }

            TempmProjectionMode = mProjectionMode;
        }


        public void UpdateCameraPDI()
        {
            MicroLightTrackingSpace[] mTrackingSpaces =GetComponentsInChildren<MicroLightTrackingSpace>();

            for(int i=0;i< mTrackingSpaces.Length;i++)
            {
                mTrackingSpaces[i].LeftEye.transform.localPosition = new Vector3(-PDI*0.5F,0,0);
                mTrackingSpaces[i].RightEye.transform.localPosition = new Vector3(PDI * 0.5F, 0, 0);
            }
        }


      
        // Start is called before the first frame update
        void Start()
        {
            if (Application.isPlaying)
            {
                 StartCoroutine("Present");
            }

        }
 

        IEnumerator Present()
        {
           

            switch (mRenderType)
            {

                case RendererType.MicroLightRender:
                
                    while (mSimulator != IntPtr.Zero)
                    {
                        yield return new WaitForEndOfFrame();


                        MicroLightPlugin.Render.UpdateRender(m_RenderAPI);
                        MicroLightPlugin.Render.RenderEye(m_RenderAPI, 0);
                        if (mRenderMode == RenderMode.Stereo)
                        {
                            MicroLightPlugin.Render.RenderEye(m_RenderAPI, 1);
                        }
                        MicroLightPlugin.Render.Present(m_RenderAPI);
                    }


                    break;
                case RendererType.UnityRender:
                    while (mSimulator != IntPtr.Zero)
                    {
                        yield return new WaitForEndOfFrame();

                        GL.IssuePluginEvent(UnityNativePlugin.GetRenderEventFunc(), 0);

                    }
                    break;

                case RendererType.UnityCoverRender:
                    while (m_CoverRenderAPI != IntPtr.Zero)
                    {
                        yield return new WaitForEndOfFrame();

                        GL.IssuePluginEvent(UnityNativePlugin.GetRenderEventFunc(), 1);

                    }

                    break;
            }


        }

       
        // Update is called once per frame
        void Update()
        {
            
        }
       


        void OnApplicationQuit()
        {
           DestroyRenderPass();
        }

 
        public void DestroyRenderPass()
        {
          
 
            if (Application.isPlaying)
            {
                StopCoroutine("Present");
                DestroyTracker();

                switch (mRenderType)
                {

                    case RendererType.MicroLightRender:
                        try
                        {
                            MicroLightPlugin.Render.RenderDestroy(m_RenderAPI);
                        }
                        catch { }
                        try
                        {
                            MicroLightPlugin.Render.RenderDestroy(m_UnityRenderAPI);
                        }
                        catch { }
                        try
                        {
                            MicroLightPlugin.Simulator.Destroy(mSimulator);

                        }
                        catch { }

                break;
                    case RendererType.UnityRender:
                        UnityNativePlugin.Render.RenderDestroy(m_UnityRenderAPI);
                        MicroLightPlugin.Simulator.Destroy(mSimulator);
                        break;
                    case RendererType.UnityCoverRender:
                        UnityNativePlugin.Render.RenderDestroy(m_UnityRenderAPI);
                        break;
                }
                m_RenderAPI = IntPtr.Zero;
                m_UnityRenderAPI = IntPtr.Zero;
                mSimulator = IntPtr.Zero;

            }

  
        }



    }
}