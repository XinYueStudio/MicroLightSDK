/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :    
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MicroLight.UnityPlugin.Pointer3D
{
    public interface IRaycastMethod
    {
        bool enabled { get; }
        void Raycast(Ray ray, float distance, List<RaycastResult> raycastResults);
    }
}