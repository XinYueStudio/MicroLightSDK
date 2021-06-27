using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
 
    public class UnityNativePlugin
    {
      
 

        public const string PluginName = "UnityNativePlugin";

        [DllImport(PluginName, EntryPoint = "GetRenderEventFunc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetRenderEventFunc();
 

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

        
        public class CoverRender
        {
            [DllImport(PluginName, EntryPoint = "CoverCreate", CallingConvention = CallingConvention.Cdecl)]
            public static extern CoverRenderAPI Create(RenderAPI render, float width, float height);

            [DllImport(PluginName, EntryPoint = "CoverUpdate", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Update(CoverRenderAPI render);

            [DllImport(PluginName, EntryPoint = "CoverRender", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Render(CoverRenderAPI render);

            [DllImport(PluginName, EntryPoint = "CoverClearViews", CallingConvention = CallingConvention.Cdecl)]
            public static extern void ClearViews(CoverRenderAPI render);

            [DllImport(PluginName, EntryPoint = "CoverAddView", CallingConvention = CallingConvention.Cdecl)]
            public static extern void AddView(CoverRenderAPI render, 
                                                                   int id,
                                                                    D3D11ShaderResourceView stereoleftview,
                                                                    D3D11ShaderResourceView stereorightview,
                                                                    float frustumwidthmultiple,
                                                                    float frustumheightmultiple,
                                                                    float pivotx,
                                                                    float pivoty);





       
        }

 
    }

}