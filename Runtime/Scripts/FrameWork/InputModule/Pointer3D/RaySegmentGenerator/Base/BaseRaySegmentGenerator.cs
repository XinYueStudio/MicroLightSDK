/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   BaseRaySegmentGenerator.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using UnityEngine;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [RequireComponent(typeof(Pointer3DRaycaster))]
    public abstract class BaseRaySegmentGenerator : MonoBehaviour, IRaySegmentGenerator
    {
        private Pointer3DRaycaster m_raycaster;
        public Pointer3DRaycaster raycaster { get { return m_raycaster; } }

        protected virtual void Start()
        {
            m_raycaster = GetComponent<Pointer3DRaycaster>();
            if (m_raycaster != null) { m_raycaster.AddGenerator(this); }
        }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy()
        {
            if (m_raycaster != null) { raycaster.RemoveGenerator(this); }
            m_raycaster = null;
        }

        public abstract void ResetSegments();

        public abstract bool NextSegment(out Vector3 direction, out float distance);


    }
}