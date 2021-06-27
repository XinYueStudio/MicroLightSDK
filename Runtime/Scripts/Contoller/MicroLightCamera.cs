using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MicroLight
{
    [AddComponentMenu("MicroLight/MicroLightCamera")]
    [ExecuteInEditMode, RequireComponent(typeof(Camera))]
    public class MicroLightCamera : MonoBehaviour
    {
        public EyeTye mEyeTye;

        private Camera _TargetCamera;

        public Camera TargetCamera
        {
            get
            {
                if (_TargetCamera == null)
                {
                    _TargetCamera = GetComponent<Camera>();

                }
                return _TargetCamera;
            }
            set
            {
                _TargetCamera = value;
            }
        }
 
        private  MicroLightTrackingSpace _trackingSpace;

        public   MicroLightTrackingSpace trackingSpace
        {
            get
            {
                if(_trackingSpace==null)
                {
                    _trackingSpace = GetComponentInParent<MicroLightTrackingSpace>();
                }

                return _trackingSpace;
            }

            set
            {
                _trackingSpace = value;
            }
        }
 
        
        // Start is called before the first frame update
        void Start()
        {
      
        }
#if MicroLightGridView
        public Shader GridViewRenderShader;
        private Material material;
        public Material mMaterial
        {
            get
            {
                if (material == null)
                {
                    if (GridViewRenderShader == null)
                    {
                        GridViewRenderShader = Shader.Find("MicroLight/GridView");
                    }

                    if (GridViewRenderShader != null)
                        material = new Material(GridViewRenderShader);
                }

                return material;
            }
            set
            {
                material = value;
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (MicroLightManager.Instance.PlayAeraRuntimeDrawGrid)
            {
                Graphics.Blit(source, TargetCamera.targetTexture, mMaterial);
            }
            else
            {
                Graphics.Blit(source, destination);
            }

        }
#endif


      
        // Update is called once per frame
        void Update()
        {
          

           // if (Application.isPlaying)
            {
                if (trackingSpace.BingdingAera.id == TrackingSpaceId.TrapezoidRoomFloor)
                {
                    HoloGraphicFrustum frustum = MicroLightPlugin.HoloGraphicUtilities.GetCalculateFrustumMatrix(
                      TargetCamera.nearClipPlane,
                      TargetCamera.farClipPlane,
                      trackingSpace.BingdingAera.RectCorner0,
                   trackingSpace.BingdingAera.RectCorner1,
                   trackingSpace.BingdingAera.RectCorner2,
                   trackingSpace.BingdingAera.RectCorner3,
                   transform.position);
                    if (frustum.GoodData)
                    {
                        TargetCamera.projectionMatrix = frustum.ProjectionMatrix;
                        TargetCamera.transform.rotation = frustum.Rotation;
                    }
                }
                else
                {
                    HoloGraphicFrustum frustum = MicroLightPlugin.HoloGraphicUtilities.GetCalculateFrustumMatrix(
                   TargetCamera.nearClipPlane,
                   TargetCamera.farClipPlane,
                   trackingSpace.BingdingAera.Corner0,
                trackingSpace.BingdingAera.Corner1,
                trackingSpace.BingdingAera.Corner2,
                trackingSpace.BingdingAera.Corner3,
                transform.position);
                    if (frustum.GoodData)
                    {
                        TargetCamera.projectionMatrix = frustum.ProjectionMatrix;
                        TargetCamera.transform.rotation = frustum.Rotation;
                    }
                }
            }
 
        }
 
#if UNITY_EDITOR

        public class MicroLightCameraGizmoDrawer
        {

            [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
            static void DrawGizmoForGrid(MicroLightCamera manager, GizmoType gizmoType)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(manager.trackingSpace.BingdingAera.Corner0, manager.transform.position);
                Gizmos.DrawLine(manager.trackingSpace.BingdingAera.Corner1, manager.transform.position);
                Gizmos.DrawLine(manager.trackingSpace.BingdingAera.Corner2, manager.transform.position);
                Gizmos.DrawLine(manager.trackingSpace.BingdingAera.Corner3, manager.transform.position);

            }
        }
#endif


    }

}