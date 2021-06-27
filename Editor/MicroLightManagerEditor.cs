//#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace MicroLight
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MicroLightManager))]
    public class MicroLightManagerEditor : Editor
    {

        public RendererType EmRenderType;
        public ProjectionMode EmProjectionMode;
        public RenderMode EmRenderMode;
        public PlayAeraSize EmPlayAeraSize;
        public Color EPlayAeraDrawGridColor;
        public Color EPlayAeraDrawGridOutlineColor;
        public bool EPlayAeraEditorDrawGrid;
        public float EPDI;

        public void OnEnable()
        {
            MicroLightManager manager = (MicroLightManager)target;

            EmRenderType = manager.mRenderType;
            EmProjectionMode= manager.mProjectionMode;
            EmRenderMode = manager.mRenderMode;
            EmPlayAeraSize= manager.mPlayAeraSize;
            EPlayAeraDrawGridColor = manager.PlayAeraDrawGridColor;
            EPlayAeraDrawGridOutlineColor = manager.PlayAeraDrawGridOutlineColor;
            EPlayAeraEditorDrawGrid = manager.PlayAeraEditorDrawGrid;
            manager.TempmProjectionMode = manager.mProjectionMode;
            EPDI = manager.PDI;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MicroLightManager manager = (MicroLightManager)target;
            if ( EmProjectionMode != manager.mProjectionMode )
            {
                EmProjectionMode = manager.mProjectionMode;
               
                manager.UpdateBindings();
                manager.TempmProjectionMode = manager.mProjectionMode;
            }


            if (EmRenderMode != manager.mRenderMode)
            {
                EmRenderMode = manager.mRenderMode;
                if (manager.mRenderMode == RenderMode.LR3D)
                {
                    manager.mProjectionMode = ProjectionMode.Surface;
                    manager.UpdateBindings();
                    manager.TempmProjectionMode = manager.mProjectionMode;
                }
            }
            if (EPDI != manager.PDI)
            {
                EPDI = manager.PDI;
                manager.UpdateCameraPDI();
            }
        }
    }
}
//#endif