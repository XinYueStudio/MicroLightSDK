using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace MicroLight
{

    [AddComponentMenu("MicroLight/MicroLightCameraRig")]
    [ExecuteInEditMode, RequireComponent(typeof(Transform))]
    public class MicroLightCameraRig : ControllerBase
    {
   
        [HideInInspector]
        public List<MicroLightTrackingSpace> mTrackingSpaces=new List<MicroLightTrackingSpace>();



        // Use this for initialization
        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

        }

//#if UNITY_EDITOR
//        public class MicroLightCameraRigGizmoDrawer
//        {
//            [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
//            static void DrawGizmoForGrid(MicroLightCameraRig manager, GizmoType gizmoType)
//            {
//                Gizmos.color = Color.blue;
//                Gizmos.DrawSphere(manager.transform.position, 0.2f);
//                // Instead of a sphere, draw the actual grid using Gizmos.DrawLine()
//            }
//        }
//#endif
    }

}