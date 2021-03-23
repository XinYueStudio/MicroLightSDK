/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   StandaloneRaycaster.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace MicroLight.UnityPlugin.Pointer3D
{
    [AddComponentMenu("MicroLight/Pointer3D/Standalone Raycaster")]
    public class StandaloneRaycaster : Pointer3DRaycaster
    {
        protected override void Start()
        {
            base.Start();
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.Fire1, PointerEventData.InputButton.Left));
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.Fire2, PointerEventData.InputButton.Middle));
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.Fire3, PointerEventData.InputButton.Right));
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.MouseLeft, PointerEventData.InputButton.Left));
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.MouseMiddle, PointerEventData.InputButton.Middle));
            buttonEventDataList.Add(new StandaloneEventData(this, EventSystem.current, StandaloneEventData.StandaloneButton.MouseRight, PointerEventData.InputButton.Right));
        }

        public override Vector2 GetScrollDelta()
        {
            return Input.mouseScrollDelta;
        }
    }
}