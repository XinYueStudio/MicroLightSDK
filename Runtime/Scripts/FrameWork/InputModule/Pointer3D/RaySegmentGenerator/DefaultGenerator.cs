/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   DefaultGenerator.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System;
using UnityEngine;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [Obsolete]
    public class DefaultGenerator : BaseRaySegmentGenerator
    {
        public override void ResetSegments() { }

        public override bool NextSegment(out Vector3 direction, out float distance)
        {
            direction = transform.forward;
            distance = float.PositiveInfinity;

            return true;
        }
    }
}