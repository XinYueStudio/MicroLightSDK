/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   Physics2DRaycastMethod.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [AddComponentMenu("MicroLight/Pointer3D/Physics2D Raycast Method")]
    public class Physics2DRaycastMethod : PhysicsRaycastMethod
    {
        private static readonly RaycastHit2D[] hits = new RaycastHit2D[64];

        public override void Raycast(Ray ray, float distance, List<RaycastResult> raycastResults)
        {
            var hitCount = Physics2D.GetRayIntersectionNonAlloc(ray, hits, distance, RaycastMask);

            for (int i = 0; i < hitCount; ++i)
            {
                var sr = hits[i].collider.gameObject.GetComponent<SpriteRenderer>();

                raycastResults.Add(new RaycastResult
                {
                    gameObject = hits[i].collider.gameObject,
                    module = raycaster,
                    distance = Vector3.Distance(ray.origin, hits[i].transform.position),
                    worldPosition = hits[i].point,
                    worldNormal = hits[i].normal,
                    screenPosition = InputModule.ScreenCenterPoint,
                    index = raycastResults.Count,
                    sortingLayer = sr != null ? sr.sortingLayerID : 0,
                    sortingOrder = sr != null ? sr.sortingOrder : 0
                });
            }
        }
    }
}