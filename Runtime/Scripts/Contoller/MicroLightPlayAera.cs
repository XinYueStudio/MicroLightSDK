using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace MicroLight
{
    [AddComponentMenu("MicroLight/MicroLightPlayAera")]
    [ExecuteInEditMode, RequireComponent(typeof(Transform))]
    public class MicroLightPlayAera : MonoBehaviour
    {
        public TrackingSpaceId id;

 
        public Vector3 Corner0
        {
            get
            {
                Vector3 Corner=Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner= transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                }
                return Corner;
            }
        }
        public Vector3 Corner1
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        float scale = ((MicroLightManager.Instance.mPlayAeraSize.mTrapezoidRoomSize.FrontWidth * 0.5f) / (transform.localScale.x * 0.5f));
                        Corner = transform.TransformPoint(new Vector3(-0.5f*scale, 0, 0.5f));
                        break;
                }
                return Corner;
               
            }
        }
        public Vector3 Corner2
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        float scale = ((MicroLightManager.Instance.mPlayAeraSize.mTrapezoidRoomSize.FrontWidth * 0.5f) / (transform.localScale.x * 0.5f));
                        Corner = transform.TransformPoint(new Vector3(0.5f* scale, 0, 0.5f));
                        break;
                }
                return Corner;
                
            }
        }
        public Vector3 Corner3
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                }
                return Corner;
               
            }
        }

        public Vector3 RectCorner0
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, -0.5f));
                        break;
                }
                return Corner;
            }
        }
        public Vector3 RectCorner1
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(-0.5f, 0, 0.5f));
                        break;
                }
                return Corner;

            }
        }
        public Vector3 RectCorner2
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, 0.5f));
                        break;
                }
                return Corner;

            }
        }
        public Vector3 RectCorner3
        {
            get
            {
                Vector3 Corner = Vector3.zero;
                switch (id)
                {
                    case TrackingSpaceId.Surface:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.LTypeRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomLeft:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFront:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomRight:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                    case TrackingSpaceId.TrapezoidRoomFloor:
                        Corner = transform.TransformPoint(new Vector3(0.5f, 0, -0.5f));
                        break;
                }
                return Corner;

            }
        }

        // Use this for initialization
        void Start()
        {
         
        }

        // Update is called once per frame
        void Update()
        {
            
                  
        }

      Material _mat;
      Material  mat
        {
            get
            {
                if(_mat==null)
                _mat = new Material(Shader.Find("GUI/Text Shader"));

                return _mat;
            }

            set
            {
                _mat = value;
            }
        }
        //public float xoffset = 0.000925f;
        //void OnRenderObject()
        //{
        //    if (Application.isPlaying)
        //    {
        //        if (MicroLightManager.Instance.PlayAeraRuntimeDrawGrid && MicroLightManager.Instance.PlayAeraEditorSection > 0)
        //        {
        //            GL.PushMatrix();
        //            mat.SetPass(0);
 
        //            GL.Begin(GL.LINES);

        //            float distance01 = Vector3.Distance(Corner0, Corner1);
        //            float distance12 = Vector3.Distance(Corner1, Corner2);
        //            float distance23 = Vector3.Distance(Corner2, Corner3);
        //            float distance03 = Vector3.Distance(Corner0, Corner3);

        //            Vector3 direction01 = (Corner1 - Corner0).normalized;
        //            Vector3 direction12 = (Corner2 - Corner1).normalized;
        //            Vector3 direction32 = (Corner2 - Corner3).normalized;
        //            Vector3 direction03 = (Corner3 - Corner0).normalized;

        //            GL.Color(MicroLightManager.Instance.PlayAeraDrawGridColor);

        //            //Vertical
        //            for (int j = 0; j < MicroLightManager.Instance.PlayAeraRuntimeDrawGridVerticalCount; j++)
        //            {
        //                float offsetstep = 0;
        //                if (MicroLightManager.Instance.PlayAeraRuntimeDrawGridVerticalCount > 1)
        //                    offsetstep = (j - (MicroLightManager.Instance.PlayAeraRuntimeDrawGridVerticalCount / 2)) * MicroLightManager.Instance.PlayAeraRuntimeDrawGridStep;

        //                for (int i = 0; i <= MicroLightManager.Instance.PlayAeraEditorSection; i++)
        //                {
                            
        //                    Vector3 toppoint = Corner1 + (distance12 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction12 + offsetstep * direction12  ;
        //                    Vector3 buttompoint = Corner0 + (distance03 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction03 + offsetstep * direction03  ;
                        


        //                    GL.Vertex(toppoint);
        //                    GL.Vertex(buttompoint);
        //                }
        //            }
        //            //Horizontal
        //            for (int j = 0; j < MicroLightManager.Instance.PlayAeraRuntimeDrawGridHorizontalCount; j++)
        //            {
        //                float offsetstep = 0;
        //                if (MicroLightManager.Instance.PlayAeraRuntimeDrawGridHorizontalCount > 1)
        //                    offsetstep = (j - (MicroLightManager.Instance.PlayAeraRuntimeDrawGridHorizontalCount / 2)) * MicroLightManager.Instance.PlayAeraRuntimeDrawGridStep;

        //                for (int i = 0; i <= MicroLightManager.Instance.PlayAeraEditorSection; i++)
        //                {
                           
        //                    Vector3 leftpoint = Corner0 + (distance01 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction01 + offsetstep * direction01;
        //                    Vector3 rightpoint = Corner3 + (distance23 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction32 + offsetstep * direction32;
                         

        //                    GL.Vertex(leftpoint);
        //                    GL.Vertex(rightpoint);
        //                }
        //            }


        //            GL.End();

        //            GL.PopMatrix();
        //        }
        //    }
        //}

        
        
        #if UNITY_EDITOR
        public static Mesh GetUnityPrimitiveMesh(PrimitiveType primitiveType)
        {
            switch (primitiveType)
            {
                case PrimitiveType.Sphere:
                    return GetCachedPrimitiveMesh(ref _unitySphereMesh, primitiveType);
                    break;
                case PrimitiveType.Capsule:
                    return GetCachedPrimitiveMesh(ref _unityCapsuleMesh, primitiveType);
                    break;
                case PrimitiveType.Cylinder:
                    return GetCachedPrimitiveMesh(ref _unityCylinderMesh, primitiveType);
                    break;
                case PrimitiveType.Cube:
                    return GetCachedPrimitiveMesh(ref _unityCubeMesh, primitiveType);
                    break;
                case PrimitiveType.Plane:
                    return GetCachedPrimitiveMesh(ref _unityPlaneMesh, primitiveType);
                    break;
                case PrimitiveType.Quad:
                    return GetCachedPrimitiveMesh(ref _unityQuadMesh, primitiveType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(primitiveType.ToString(), primitiveType, null);
            }
        }

        private static Mesh GetCachedPrimitiveMesh(ref Mesh primMesh, PrimitiveType primitiveType)
        {
            if (primMesh == null)
            {
                //Debug.Log("Getting Unity Primitive Mesh: " + primitiveType);
                primMesh = Resources.GetBuiltinResource<Mesh>(GetPrimitiveMeshPath(primitiveType));

                if (primMesh == null)
                {
                    Debug.LogError("Couldn't load Unity Primitive Mesh: " + primitiveType);
                }
            }

            return primMesh;
        }

        private static string GetPrimitiveMeshPath(PrimitiveType primitiveType)
        {
            switch (primitiveType)
            {
                case PrimitiveType.Sphere:
                    return "New-Sphere.fbx";
                    break;
                case PrimitiveType.Capsule:
                    return "New-Capsule.fbx";
                    break;
                case PrimitiveType.Cylinder:
                    return "New-Cylinder.fbx";
                    break;
                case PrimitiveType.Cube:
                    return "Cube.fbx";
                    break;
                case PrimitiveType.Plane:
                    return "New-Plane.fbx";
                    break;
                case PrimitiveType.Quad:
                    return "Quad.fbx";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(primitiveType.ToString(), primitiveType, null);
            }
        }

        private static Mesh _unityCapsuleMesh = null;
        private static Mesh _unityCubeMesh = null;
        private static Mesh _unityCylinderMesh = null;
        private static Mesh _unityPlaneMesh = null;
        private static Mesh _unitySphereMesh = null;
        private static Mesh _unityQuadMesh = null;


        public class MicroLightPlayAeraGizmoDrawer
        {

            [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
            static void DrawGizmoForGrid(MicroLightPlayAera manager, GizmoType gizmoType)
            {
                if (MicroLightManager.Instance.PlayAeraEditorDrawGrid)
                {
                    if (MicroLightManager.Instance.PlayAeraEditorSection > 0)
                    {
                        float distance01 = Vector3.Distance(manager.Corner0, manager.Corner1);
                        float distance12 = Vector3.Distance(manager.Corner1, manager.Corner2);
                        float distance23 = Vector3.Distance(manager.Corner2, manager.Corner3);
                        float distance03 = Vector3.Distance(manager.Corner0, manager.Corner3);

                        Vector3 direction01 = (manager.Corner1 - manager.Corner0).normalized;
                        Vector3 direction12 = (manager.Corner2 - manager.Corner1).normalized;
                        Vector3 direction32 = (manager.Corner2 - manager.Corner3).normalized;
                        Vector3 direction03 = (manager.Corner3 - manager.Corner0).normalized;

                        Gizmos.color = MicroLightManager.Instance.PlayAeraDrawGridColor;

                        //Vertical
                        for (int i = 0; i < MicroLightManager.Instance.PlayAeraEditorSection; i++)
                        {
                            Vector3 toppoint = manager.Corner1 + (distance12 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction12 ;
                            Vector3 buttompoint = manager.Corner0 + (distance03 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction03 ;
                            Gizmos.DrawLine(toppoint, buttompoint);
                        }
                      
                        //Horizontal
                        for (int i = 0; i < MicroLightManager.Instance.PlayAeraEditorSection; i++)
                            {
                                Vector3 leftpoint = manager.Corner0 + (distance01 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction01  ;
                                Vector3 rightpoint = manager.Corner3 + (distance23 / MicroLightManager.Instance.PlayAeraEditorSection) * i * direction32  ;
                                Gizmos.DrawLine(leftpoint, rightpoint);
                            }
                      

                    }

                   
                    Gizmos.color = MicroLightManager.Instance.PlayAeraDrawGridOutlineColor;
                    Gizmos.DrawLine(manager.Corner0, manager.Corner1);
                    Gizmos.DrawLine(manager.Corner1, manager.Corner2);
                    Gizmos.DrawLine(manager.Corner2, manager.Corner3);
                    Gizmos.DrawLine(manager.Corner3, manager.Corner0);

                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(manager.Corner0, 0.01F);
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(manager.Corner1, 0.01F);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(manager.Corner2, 0.01F);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(manager.Corner3, 0.01F);

                }

            }
        }
#endif
    }

}