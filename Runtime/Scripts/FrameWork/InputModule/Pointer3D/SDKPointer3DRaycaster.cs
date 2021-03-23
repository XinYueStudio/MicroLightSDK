/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   SDKPointer3DRaycaster.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using UnityEngine;
using System.Collections;
using MicroLight.UnityPlugin.Pointer3D;
using UnityEngine.EventSystems;
using System;

namespace MicroLight
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PhysicsRaycastMethod)),
        RequireComponent(typeof(CanvasRaycastMethod))]
    public class SDKPointer3DRaycaster : Pointer3DRaycaster
    {
       
        private MicroLightHand handHandler;
        public MicroLightHand HandHandler
        {
            get
            {
                if (handHandler == null)
                {
                    handHandler = GetComponent<MicroLightHand>();
                }
                if (handHandler == null)
                {
                    handHandler = GetComponentInParent<MicroLightHand>();
                }
                return handHandler;
            }

        }


        protected override void Start()
        {
            base.Start();
            
            buttonEventDataList.Add(new SDKPointer3DEventData(this, EventSystem.current, HandHandler));

        }

      

        [Range(0.001f, 10f)]
        public float scrollDeltaScale = 1.0f;
        public Vector2 scrollDelta = Vector2.zero;

        private float m_LastHorizontalValue;
        private float m_LastVerticalValue;

        public override Vector2 GetScrollDelta()
        {

            if (HandHandler)
            {
                if(HandHandler.ThumbstickAxis!=Vector2.zero)
                return HandHandler.ThumbstickAxis * scrollDeltaScale;
                else
                {
                    return( HandHandler.ThumbstickAxis + base.GetScrollDelta())* scrollDeltaScale;
                }
            }

                return base.GetScrollDelta()* scrollDeltaScale;
           
            
        }

    }

}