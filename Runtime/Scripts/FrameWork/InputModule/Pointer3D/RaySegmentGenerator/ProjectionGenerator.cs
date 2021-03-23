/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   ProjectionGenerator.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using UnityEngine;

namespace MicroLight.UnityPlugin.Pointer3D
{
    public class ProjectionGenerator : BaseRaySegmentGenerator
    {
        public float velocity = 2f;
        public Vector3 gravity = Vector3.down;

        private bool isFirstSegment = true;

        public override void ResetSegments()
        {
            isFirstSegment = true;
        }

        public override bool NextSegment(out Vector3 direction, out float distance)
        {
            if (isFirstSegment && velocity > Pointer3DRaycaster.MIN_SEGMENT_DISTANCE)
            {
                isFirstSegment = false;
                direction = raycaster.transform.forward;
                distance = velocity;
            }
            else
            {
                direction = gravity;
                distance = float.PositiveInfinity;
            }

            return true;
        }
    }
}