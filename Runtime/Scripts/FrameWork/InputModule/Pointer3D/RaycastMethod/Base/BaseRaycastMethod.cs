/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :    BaseRaycastMethod.cs
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [RequireComponent(typeof(Pointer3DRaycaster))]
    public abstract class BaseRaycastMethod : MonoBehaviour, IRaycastMethod
    {
        private Pointer3DRaycaster m_raycaster;
        public Pointer3DRaycaster raycaster { get { return m_raycaster; } }

        protected virtual void Start()
        {
          
        }

        protected virtual void OnEnable() {
            m_raycaster = GetComponent<Pointer3DRaycaster>();
            if (m_raycaster != null) { m_raycaster.AddRaycastMethod(this); }
        }

        protected virtual void OnDisable() {
            if (m_raycaster != null) { raycaster.RemoveRaycastMethod(this); }
            m_raycaster = null;
        }

        protected virtual void OnDestroy()
        {
            if (m_raycaster != null) { raycaster.RemoveRaycastMethod(this); }
            m_raycaster = null;
        }

        public abstract void Raycast(Ray ray, float distance, List<RaycastResult> raycastResults);
    }
}