using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering;
 
using System;

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


    [AddComponentMenu("MicroLight/MicroLightTrackingSpace")]
    [ExecuteInEditMode, RequireComponent(typeof(Transform))]
    public class MicroLightTrackingSpace : MonoBehaviour
    {
        public TrackingSpaceId id;

        public MicroLightCamera LeftEye;
        public MicroLightCamera RightEye;
        public MicroLightPlayAera _BingdingAera;

        public MicroLightPlayAera BingdingAera
        {
            get
            {
                if(_BingdingAera==null)
                {
                    MicroLightPlayAera[]  microLightPlayAeras= MicroLightManager.Instance.GetComponentsInChildren<MicroLightPlayAera>();
                    for(int i=0;i< microLightPlayAeras.Length;i++)
                    {
                        if (microLightPlayAeras[i].id == id)
                        {
                            _BingdingAera = microLightPlayAeras[i];
                            break;
                        }

                    }
                  
                }


                return _BingdingAera;
            }
            set
            {
                _BingdingAera = value;
            }
        }
        Vector3 TextureSize;
        RenderTexture LeftRT;
        RenderTexture RightRT;
        D3D11Texture2D LeftRT2D;
        D3D11Texture2D RightRT2D;
        D3D11ShaderResourceView leftView;
        D3D11ShaderResourceView rightView;
         

        public void NewTextureForRender()
        {
            if (Application.isPlaying)
            {
                TextureSize =new Vector3(1920,1080,0);// MicroLightPlugin.HoloGraphicUtilities.GetTextureSize();

                LeftRT = new RenderTexture((int)TextureSize.x, (int)TextureSize.y, 24, GraphicsFormat.B8G8R8A8_UNorm);
                LeftEye.TargetCamera.targetTexture = LeftRT;

                RightRT = new RenderTexture((int)TextureSize.x, (int)TextureSize.y, 24, GraphicsFormat.B8G8R8A8_UNorm);
                RightEye.TargetCamera.targetTexture = RightRT;

                if (LeftRT.GetNativeTexturePtr() == IntPtr.Zero)
                {
                    Debug.LogError("Can not Create LeftRT RenderTexture");
                }
                if (RightRT.GetNativeTexturePtr() == IntPtr.Zero)
                {
                    Debug.LogError("Can not Create RightRT RenderTexture");
                }
            }
        }


        // Use this for initialization
        void Start()
        {
            NewTextureForRender();
            if (Application.isPlaying)
            {
                if (MicroLightManager.Instance)
                {
                   
                    switch (MicroLightManager.Instance.mRenderType)
                    {

                        case RendererType.MicroLightRender:
                            {

                            

                                //Create Shared  Texture2D

                                LeftRT2D = MicroLightPlugin.D3D11Helper.CreateSharedView(MicroLightManager.Instance.m_UnityDevice,
                                                                                                                                                                DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
                                                                                                                                                                (int)TextureSize.x,
                                                                                                                                                                (int)TextureSize.y);

                                if (LeftRT2D == IntPtr.Zero)
                                {
                                    Debug.Log(this + " LeftRT2D is null");
                                }
                                HANDLE LeftRTHandleSend = MicroLightPlugin.D3D11Helper.GetSharedHandle(MicroLightManager.Instance.m_UnityDevice, LeftRT2D);
                                if (LeftRTHandleSend == IntPtr.Zero)
                                {
                                    Debug.Log(this + " LeftRTHandleSend is null");
                                }
                                D3D11Resource LeftRTHandleReceive = MicroLightPlugin.D3D11Helper.OpenSharedResource(MicroLightManager.Instance.m_Device, LeftRTHandleSend);
                                if (LeftRTHandleReceive == IntPtr.Zero)
                                {
                                    Debug.Log(this + " LeftRTHandleReceive is null");
                                }

                                RightRT2D = MicroLightPlugin.D3D11Helper.CreateSharedView(MicroLightManager.Instance.m_UnityDevice,
                                                                                                                                                              DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM,
                                                                                                                                                              (int)TextureSize.x,
                                                                                                                                                              (int)TextureSize.y);
                                if (RightRT2D == IntPtr.Zero)
                                {
                                    Debug.Log(this + " RightRT2D is null");
                                }
                                HANDLE RightRTHandleSend = MicroLightPlugin.D3D11Helper.GetSharedHandle(MicroLightManager.Instance.m_UnityDevice, RightRT2D);
                                if (RightRTHandleSend == IntPtr.Zero)
                                {
                                    Debug.Log(this + " RightRTHandleSend is null");
                                }
                                D3D11Resource RightRTHandleReceive = MicroLightPlugin.D3D11Helper.OpenSharedResource(MicroLightManager.Instance.m_Device, RightRTHandleSend);
                                if (RightRTHandleSend == IntPtr.Zero)
                                {
                                    Debug.Log(this + " RightRTHandleReceive is null");
                                }

                                leftView = MicroLightPlugin.D3D11Helper.CreateShaderResourceView(MicroLightManager.Instance.m_Device, LeftRTHandleReceive);
                                rightView = MicroLightPlugin.D3D11Helper.CreateShaderResourceView(MicroLightManager.Instance.m_Device, RightRTHandleReceive);

 
                                if (leftView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("leftView is null");
                                }
                                if (rightView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("rightView is null");
                                }

                                switch (MicroLightManager.Instance.mRenderMode)
                                {
                                    case RenderMode.LR3D:
                                        {
                                            MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, ((int)id * 10 + (int)LeftEye.mEyeTye), leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                            MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, ((int)id * 10 + (int)RightEye.mEyeTye), rightView, leftView, 0.5f, 1.0f, 0.25f, 0);
                                        }
                                        break;
                                    case RenderMode.Stereo:
                                        {

                                            switch (id)
                                            {
                                                case TrackingSpaceId.Surface:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 1, 1, 0, 0);
                                                    break;

                                                case TrackingSpaceId.LTypeRoomFront:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                                    break;
                                                case TrackingSpaceId.LTypeRoomFloor:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, 0.25f, 0);
                                                    break;
                                                case TrackingSpaceId.TrapezoidRoomLeft:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.375f, 0);
                                                    break;
                                                case TrackingSpaceId.TrapezoidRoomFront:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.125f, 0);
                                                    break;
                                                case TrackingSpaceId.TrapezoidRoomRight:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.125f, 0);
                                                    break;
#if MicroLightGridView
                                                case TrackingSpaceId.TrapezoidRoomFloor:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id+1, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                    break;
#else
                                               case TrackingSpaceId.TrapezoidRoomFloor:
                                                    MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_RenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                    break;
#endif
                                            }


                                        }
                                        break;
                                }



                            }
                            break;
                        case RendererType.UnityRender:
                            {
                                 

                                  leftView =
                                    MicroLightPlugin.D3D11Helper.CreateShaderResourceViewFromRenderTexture(MicroLightManager.Instance.m_UnityDevice,
                                    LeftRT.GetNativeTexturePtr(),
                                    DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM);

                                  rightView =
                                    MicroLightPlugin.D3D11Helper.CreateShaderResourceViewFromRenderTexture(MicroLightManager.Instance.m_UnityDevice,
                                    RightRT.GetNativeTexturePtr(),
                                    DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM);

                                if (leftView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("leftView is null");
                                }
                                if (rightView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("rightView is null");
                                }

                                switch (MicroLightManager.Instance.mRenderMode)
                                {
                                    case RenderMode.LR3D:
                                        MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, ((int)id * 10 + (int)LeftEye.mEyeTye), leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                        MicroLightPlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, ((int)id * 10 + (int)RightEye.mEyeTye), rightView, leftView, 0.5f, 1.0f, 0.25f, 0);
                                        break;
                                    case RenderMode.Stereo:

                                    
                                        switch (id)
                                        {
                                            case TrackingSpaceId.Surface:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 1, 1, 0, 0);
                                                break;

                                            case TrackingSpaceId.LTypeRoomFront:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                                break;
                                            case TrackingSpaceId.LTypeRoomFloor:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, 0.25f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomLeft:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.375f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomFront:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.125f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomRight:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.125f, 0);
                                                break;
#if MicroLightGridView
                                            case TrackingSpaceId.TrapezoidRoomFloor:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id+1, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                break;
#else
                                            case TrackingSpaceId.TrapezoidRoomFloor:
                                                UnityNativePlugin.Render.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                break;

#endif
                                        }

                                        break;
                                }

                            }
                            break;

                        case RendererType.UnityCoverRender:
                            {
                                  
 
                                  leftView =
                                 MicroLightPlugin.D3D11Helper.CreateShaderResourceViewFromRenderTexture(MicroLightManager.Instance.m_UnityDevice,
                                 LeftRT.GetNativeTexturePtr(),
                                 DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM);

                                  rightView =
                                    MicroLightPlugin.D3D11Helper.CreateShaderResourceViewFromRenderTexture(MicroLightManager.Instance.m_UnityDevice,
                                    RightRT.GetNativeTexturePtr(),
                                    DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM);


                                if (leftView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("leftView is null");
                                }
                                if (rightView == IntPtr.Zero)
                                {
                                    UnityEngine.Debug.Log("rightView is null");
                                }
                                switch (MicroLightManager.Instance.mRenderMode)
                                {
                                    case RenderMode.LR3D:
                                        UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_CoverRenderAPI, ((int)id * 10 + (int)LeftEye.mEyeTye), leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                        UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_CoverRenderAPI, ((int)id * 10 + (int)RightEye.mEyeTye), rightView, leftView, 0.5f, 1.0f, 0.25f, 0);
                                        break;
                                    case RenderMode.Stereo:
 
                                        switch (id)
                                        {
                                            case TrackingSpaceId.Surface:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 1, 1, 0, 0);
                                                break;

                                            case TrackingSpaceId.LTypeRoomFront:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, -0.25f, 0);
                                                break;
                                            case TrackingSpaceId.LTypeRoomFloor:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.5f, 1.0f, 0.25f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomLeft:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.375f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomFront:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, -0.125f, 0);
                                                break;
                                            case TrackingSpaceId.TrapezoidRoomRight:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.125f, 0);
                                                break;
#if MicroLightGridView
                                            case TrackingSpaceId.TrapezoidRoomFloor:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id+1, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                break;
#else
                                              case TrackingSpaceId.TrapezoidRoomFloor:
                                                UnityNativePlugin.CoverRender.AddView(MicroLightManager.Instance.m_UnityRenderAPI, (int)id, leftView, rightView, 0.25f, 1.0f, 0.375f, 0);
                                                break;
#endif
                                        }

                                        break;
                                }
                            }
                            break;
                    }
                }
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Application.isPlaying)
            {
                if (MicroLightManager.Instance)
                {
                   
                    switch (MicroLightManager.Instance.mRenderType)
                    {

                        case RendererType.MicroLightRender:

                            if(MicroLightManager.Instance.m_UnityDevice!=IntPtr.Zero&& RightRT2D != IntPtr.Zero && LeftRT2D != IntPtr.Zero)
                            {
                                MicroLightPlugin.D3D11Helper.SubmitSharedView(MicroLightManager.Instance.m_UnityDevice, LeftRT.GetNativeTexturePtr(), LeftRT2D);
                                MicroLightPlugin.D3D11Helper.SubmitSharedView(MicroLightManager.Instance.m_UnityDevice, RightRT.GetNativeTexturePtr(), RightRT2D);
                            }

                            break;
                        case RendererType.UnityRender:
                       
                            break;

                        case RendererType.UnityCoverRender:
                           
                            break;
                    }
                }
            }
        }
    }

}
