/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   PhysicsRaycastMethod.cs 
ProjectName    :   MicroLight
ProductionDate :  2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [AddComponentMenu("MicroLight/Pointer3D/Physics Raycast Method")]
    public class PhysicsRaycastMethod : BaseRaycastMethod
    {
        public enum MaskTypeEnum
        {
            Inclusive,
            Exclusive,
        }

        private static readonly RaycastHit[] hits = new RaycastHit[64];

        public MaskTypeEnum maskType;
        public LayerMask mask;

        public int RaycastMask { get { return maskType == MaskTypeEnum.Inclusive ? (int)mask : ~mask; } }
#if UNITY_EDITOR
        protected virtual void Reset()
        {
            maskType = MaskTypeEnum.Exclusive;
            mask = LayerMask.GetMask("Ignore Raycast");
        }
#endif
        protected override void OnEnable()
        {
            base.OnEnable();
            maskType = MaskTypeEnum.Exclusive;
            mask = LayerMask.GetMask("Ignore Raycast");
        }
        public override void Raycast(Ray ray, float distance, List<RaycastResult> raycastResults)
        {
            var hitCount = Physics.RaycastNonAlloc(ray, hits, distance, RaycastMask);

            for (int i = 0; i < hitCount; ++i)
            {
                raycastResults.Add(new RaycastResult
                {
                    gameObject = hits[i].collider.gameObject,
                    module = raycaster,
                    distance = hits[i].distance,
                    worldPosition = hits[i].point,
                    worldNormal = hits[i].normal,
                    screenPosition = InputModule.ScreenCenterPoint,
                    index = raycastResults.Count,
                    sortingLayer = 0,
                    sortingOrder = 0
                });

 
            }
        }
    }
}