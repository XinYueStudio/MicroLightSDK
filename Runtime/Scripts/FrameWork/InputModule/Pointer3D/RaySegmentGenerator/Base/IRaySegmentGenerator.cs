/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   IRaySegmentGenerator.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using UnityEngine;

namespace MicroLight.UnityPlugin.Pointer3D
{
    public interface IRaySegmentGenerator
    {
        bool enabled { get; set; }
        
        void ResetSegments();
        bool NextSegment(out Vector3 direction, out float distance);
    }
}