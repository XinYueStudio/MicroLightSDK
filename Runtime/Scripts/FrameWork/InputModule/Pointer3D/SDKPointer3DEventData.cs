 
using UnityEngine;
using System.Collections;
using MicroLight.UnityPlugin.Pointer3D;
using UnityEngine.EventSystems;

namespace MicroLight
{
    public class SDKPointer3DEventData : Pointer3DEventData
    {
       
        private MicroLightHand HandHandler;
        public Pointer3DRaycaster mownerRaycaster;
        public SDKPointer3DEventData(Pointer3DRaycaster ownerRaycaster, EventSystem eventSystem, MicroLightHand Incontroller ) : base(ownerRaycaster, eventSystem)
        {
            HandHandler = Incontroller ;
            mownerRaycaster = ownerRaycaster;
        }
       


        public override bool GetPress()
        {
            if (mownerRaycaster.UseMouse)
            {
                return Input.GetMouseButton(0);

            }
            else
            {
                if (HandHandler)
                {
                
                    return HandHandler.IndexTriggerDownIsDown || Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0);

                }
                else
                {

                    return Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0);
                }
            }
        }

        public override bool GetPressDown()
        {
            if (mownerRaycaster.UseMouse)
            {
                return Input.GetMouseButtonDown(0);

            }
            else
            {
                if (HandHandler)
                {

                    return HandHandler.IndexTriggerDownIsDown||Input.GetKey(KeyCode.Return);

                }
                else
                {

                    return Input.GetKey(KeyCode.Return);
                }
            }

        }

        public override bool GetPressUp()
        {
            if (mownerRaycaster.UseMouse)
            {
                return Input.GetMouseButtonUp(0);


              
            }
            else
            {
                if (HandHandler)
                {

                    return !HandHandler.IndexTriggerDownIsDown ||!Input.GetKey(KeyCode.Return);

                }
                else
                {
                    return !Input.GetKey(KeyCode.Return);
                }
            }
        }

    }

}