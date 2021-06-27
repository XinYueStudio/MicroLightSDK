using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace MicroLight
{

    [AddComponentMenu("MicroLight/ControllerInputBase")]
    [ExecuteInEditMode, RequireComponent(typeof(Transform))]
    public class ControllerInputBase : ControllerBase
    {

        [Header("[-----MicroLight Custom Touchpad------]")]
       // [HideInInspector]
        public bool BackIsDown = false;//SteamVR System Button
       // [HideInInspector]
        public bool MenuIsDown = false;//SteamVR Menu Button
       // [HideInInspector]
        public bool IndexTriggerUpIsDown = false;//SteamVR Grip  Button
                                                 // [HideInInspector]
        public bool IndexTriggerDownIsDown = false;//SteamVR Trigger Button
                                                   //  [HideInInspector]
        public bool ThumbstickUpIsDown = false;//SteamVR ThumbstickUp Button
     //   [HideInInspector]
        public bool ThumbstickDownIsDown = false;//SteamVR ThumbstickDown Button
     //   [HideInInspector]
        public bool ThumbstickLeftIsDown = false;//SteamVR  ThumbstickLeft Button
      //  [HideInInspector]
        public bool ThumbstickRightIsDown = false;//SteamVR  ThumbstickRight Button


        public bool EnButton = true;
        public bool EnAxis = true;

        public bool EditorShowDebug = true;

        public Vector2 ThumbstickAxis = Vector2.zero;
        [Header("MicroLight Back Button/SteamVR System Button")]
        public UnityEvent BackDown;
        public UnityEvent BackPressed;
        public UnityEvent BackUp;

        [Header("MicroLight Menu Button/SteamVR Menu Button")]
        public UnityEvent MenuDown;
        public UnityEvent MenuPressed;
        public UnityEvent MenuUp;

        [Header("MicroLight TriggerUp Button/SteamVR Grip Button")]
        public UnityEvent IndexTriggerUpDown;
        public UnityEvent IndexTriggerUpPressed;
        public UnityEvent IndexTriggerUpUp;

        [Header("MicroLight TriggerDown Button/SteamVR Trigger  Button")]
        public UnityEvent IndexTriggerDownDown;
        public UnityEvent IndexTriggerDownPressed;
        public UnityEvent IndexTriggerDownUp;

        [Header("MicroLight ThumbstickUp Button/SteamVR ThumbstickUp Button")]
        public UnityEvent ThumbstickUpDown;
        public UnityEvent ThumbstickUpPressed;
        public UnityEvent ThumbstickUpUp;

        [Header("MicroLight ThumbstickDown Button/SteamVR ThumbstickDown Button")]
        public UnityEvent ThumbstickDownDown;
        public UnityEvent ThumbstickDownPressed;
        public UnityEvent ThumbstickDownUp;
        [Header("MicroLight ThumbstickLeft Button/SteamVR ThumbstickLeft Button")]
        public UnityEvent ThumbstickLeftDown;
        public UnityEvent ThumbstickLeftPressed;
        public UnityEvent ThumbstickLeftUp;
        [Header("MicroLight ThumbstickRight Button/SteamVR ThumbstickRight Button")]
        public UnityEvent ThumbstickRightDown;
        public UnityEvent ThumbstickRightPressed;
        public UnityEvent ThumbstickRightUp;
        [Serializable]
        public class IndexTriggerAxis1D : UnityEvent<float>
        {

        }
        [Serializable]
        public class ThumbstickAxis2D : UnityEvent<Vector2>
        {

        }

        public IndexTriggerAxis1D IndexTriggerAxisChanged;
        public ThumbstickAxis2D ThumbstickAxisChanged;

        [HideInInspector]
        public float IndexTriggerAxis = 0;

 
        public override void OnEnable()
        {
            Vector3 eulerAngles = new Vector3();
            transform.localEulerAngles = eulerAngles;
            base.OnEnable();
        }
        public override void Update()
        {


            if (MicroLightManager.Instance)
            {
                if (MicroLightManager.Instance.OpenTracker)
                {
                   
                    //Do to transform Update
                    base.Update();

                    if (MicroLightManager.Instance.mTracker != IntPtr.Zero)
                    {
                        if (MicroLightManager.Instance.deviceType == TrackingDeviceType.MicroLightIR)
                        {

                            if (EnButton)
                            {
                                //Back Button 
                                bool Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.Back);



                                if (Result)
                                {
                                    if (!BackIsDown)
                                    {
                                        BackIsDown = Result;

                                        if (BackDown != null)
                                        {
                                            BackDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":BackDown");
                                        }
#endif
                                    }
                                    if (BackPressed != null)
                                    {
                                        BackPressed.Invoke();
                                    }

#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":BackPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (BackIsDown)
                                    {
                                        BackIsDown = false;
                                        if (BackUp != null)
                                        {
                                            BackUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":BackUp");
                                        }
#endif
                                    }
                                }

                                //Menu Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.Menu);
                                if (Result)
                                {
                                    if (!MenuIsDown)
                                    {
                                        MenuIsDown = Result;

                                        if (MenuDown != null)
                                        {
                                            MenuDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":MenuDown");
                                        }
#endif
                                    }


                                    if (MenuPressed != null)
                                    {
                                        MenuPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":MenuPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (MenuIsDown)
                                    {
                                        MenuIsDown = false;
                                        if (MenuUp != null)
                                        {
                                            MenuUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":MenuUp");
                                        }
#endif
                                    }
                                }

                                //IndexTriggerUp Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.IndexTriggerUp);
                                if (Result)
                                {
                                    if (!IndexTriggerUpIsDown)
                                    {
                                        IndexTriggerUpIsDown = Result;

                                        if (IndexTriggerUpDown != null)
                                        {
                                            IndexTriggerUpDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerUpDown");
                                        }
#endif
                                    }


                                    if (IndexTriggerUpPressed != null)
                                    {
                                        IndexTriggerUpPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":IndexTriggerUpPressed");
                                    }
#endif
                                }
                                else
                                {



                                    if (IndexTriggerUpIsDown)
                                    {
                                        IndexTriggerUpIsDown = false;
                                        if (IndexTriggerUpUp != null)
                                        {
                                            IndexTriggerUpUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerUpUp");
                                        }
#endif
                                    }
                                }

                                //IndexTriggerDown Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.IndexTriggerDown);
                                if (Result)
                                {
                                    if (!IndexTriggerDownIsDown)
                                    {
                                        IndexTriggerDownIsDown = Result;

                                        if (IndexTriggerDownDown != null)
                                        {
                                            IndexTriggerDownDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerDownDown");
                                        }
#endif
                                    }

                                    if (IndexTriggerDownPressed != null)
                                    {
                                        IndexTriggerDownPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":IndexTriggerDownPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (IndexTriggerDownIsDown)
                                    {
                                        IndexTriggerDownIsDown = false;
                                        if (IndexTriggerDownUp != null)
                                        {
                                            IndexTriggerDownUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerDownUp");
                                        }
#endif
                                    }
                                }

                                //ThumbstickUp Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.ThumbstickUp);
                                if (Result)
                                {
                                    if (!ThumbstickUpIsDown)
                                    {
                                        ThumbstickUpIsDown = Result;

                                        if (ThumbstickUpDown != null)
                                        {
                                            ThumbstickUpDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickUpDown");
                                        }
#endif
                                    }

                                    if (ThumbstickUpPressed != null)
                                    {
                                        ThumbstickUpPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickUpPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickUpIsDown)
                                    {
                                        ThumbstickUpIsDown = false;
                                        if (ThumbstickUpUp != null)
                                        {
                                            ThumbstickUpUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickUpUp");
                                        }
#endif
                                    }
                                }

                                //ThumbstickDown Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.ThumbstickDown);
                                if (Result)
                                {
                                    if (!ThumbstickDownIsDown)
                                    {
                                        ThumbstickDownIsDown = Result;

                                        if (ThumbstickDownDown != null)
                                        {
                                            ThumbstickDownDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickDownDown");
                                        }
#endif
                                    }

                                    if (ThumbstickDownPressed != null)
                                    {
                                        ThumbstickDownPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickDownPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickDownIsDown)
                                    {
                                        ThumbstickDownIsDown = false;
                                        if (ThumbstickDownUp != null)
                                        {
                                            ThumbstickDownUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickDownUp");
                                        }
#endif
                                    }
                                }

                                //ThumbstickLeft Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.ThumbstickLeft);
                                if (Result)
                                {
                                    if (!ThumbstickLeftIsDown)
                                    {
                                        ThumbstickLeftIsDown = Result;

                                        if (ThumbstickLeftDown != null)
                                        {
                                            ThumbstickLeftDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickLeftDown");
                                        }
#endif
                                    }

                                    if (ThumbstickLeftPressed != null)
                                    {
                                        ThumbstickLeftPressed.Invoke();
                                    }
#if DEBUGE
                            if (EditorShowDebug)
                            {
                                Debug.Log(controller + ":ThumbstickLeftPressed");
                            }
#endif
                                }
                                else
                                {

                                    if (ThumbstickLeftIsDown)
                                    {
                                        ThumbstickLeftIsDown = false;
                                        if (ThumbstickLeftUp != null)
                                        {
                                            ThumbstickLeftUp.Invoke();
                                        }
#if DEBUGE
                                if (EditorShowDebug)
                                {
                                    Debug.Log(controller + ":ThumbstickLeftUp");
                                }
#endif
                                    }
                                }

                                //ThumbstickRight Button 
                                Result = MicroLightPlugin.Tracker.GetButtonPressed(MicroLightManager.Instance.mTracker, serialnumber, (int)Button.ThumbstickRight);
                                if (Result)
                                {
                                    if (!ThumbstickRightIsDown)
                                    {
                                        ThumbstickRightIsDown = Result;

                                        if (ThumbstickRightDown != null)
                                        {
                                            ThumbstickRightDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickRightDown");
                                        }
#endif
                                    }

                                    if (ThumbstickRightPressed != null)
                                    {
                                        ThumbstickRightPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickRightPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickRightIsDown)
                                    {
                                        ThumbstickRightIsDown = false;
                                        if (ThumbstickRightUp != null)
                                        {
                                            ThumbstickRightUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickRightUp");
                                        }
#endif
                                    }
                                }
                            }
                            if (EnAxis)
                            {
                                Axis1D IndexTriggerAxisType = Axis1D.RIndexTrigger;

                                if (id == Controller.Touchpad1)

                                {


                                    IndexTriggerAxisType = Axis1D.RIndexTrigger;
                                }
                                if (id == Controller.Touchpad2)

                                {


                                    IndexTriggerAxisType = Axis1D.LIndexTrigger;
                                }
                                float Axis1DValue = MicroLightPlugin.Tracker.GetButtonAxis1D(MicroLightManager.Instance.mTracker, serialnumber, (int)IndexTriggerAxisType);
                                if (IndexTriggerAxis != Axis1DValue)
                                {
                                    IndexTriggerAxis = Axis1DValue;

                                    if (IndexTriggerAxisChanged != null)
                                    {
                                        IndexTriggerAxisChanged.Invoke(IndexTriggerAxis);
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":IndexTriggerAxisChanged");
                                    }
#endif
                                }
                                Axis2D ThumbstickAxisType = Axis2D.RThumbstick;
                                if (id == Controller.Touchpad1)

                                {


                                    ThumbstickAxisType = Axis2D.RThumbstick;

                                    Vector2 Axis2DValue = MicroLightPlugin.Tracker.GetButtonAxis2D(MicroLightManager.Instance.mTracker, serialnumber, (int)ThumbstickAxisType);
                                    if (ThumbstickAxis != Axis2DValue)
                                    {
                                        ThumbstickAxis = Axis2DValue;

                                        if (ThumbstickAxisChanged != null)
                                        {
                                            ThumbstickAxisChanged.Invoke(ThumbstickAxis);
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickAxis");
                                        }
#endif
                                    }
                                }
                                if (id == Controller.Touchpad2)

                                {


                                    ThumbstickAxisType = Axis2D.LThumbstick;

                                    Vector2 Axis2DValue = MicroLightPlugin.Tracker.GetButtonAxis2D(MicroLightManager.Instance.mTracker, serialnumber, (int)ThumbstickAxisType);
                                    if (ThumbstickAxis != Axis2DValue)
                                    {
                                        ThumbstickAxis = Axis2DValue;

                                        if (ThumbstickAxisChanged != null)
                                        {
                                            ThumbstickAxisChanged.Invoke(ThumbstickAxis);
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickAxis");
                                        }
#endif
                                    }
                                }


                            }
                        }
                        else if (MicroLightManager.Instance.deviceType == TrackingDeviceType.LightHouse)
                        {
                            VRControllerState vRControllerState = MicroLightPlugin.Tracker.GetControllerState(MicroLightManager.Instance.mTracker, serialnumber);

                            
                            if (EnButton)
                            {
                                //Back Button 
                                bool Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_System))) > 0;
                                if (Result)
                                {
                                    if (!BackIsDown)
                                    {
                                        BackIsDown = Result;

                                        if (BackDown != null)
                                        {
                                            BackDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":BackDown");
                                        }
#endif
                                    }
                                    if (BackPressed != null)
                                    {
                                        BackPressed.Invoke();
                                    }

#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":BackPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (BackIsDown)
                                    {
                                        BackIsDown = false;
                                        if (BackUp != null)
                                        {
                                            BackUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":BackUp");
                                        }
#endif
                                    }
                                }


                                //Menu Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_ApplicationMenu))) > 0;
                                if (Result)
                                {
                                    if (!MenuIsDown)
                                    {
                                        MenuIsDown = Result;

                                        if (MenuDown != null)
                                        {
                                            MenuDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":MenuDown");
                                        }
#endif
                                    }


                                    if (MenuPressed != null)
                                    {
                                        MenuPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":MenuPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (MenuIsDown)
                                    {
                                        MenuIsDown = false;
                                        if (MenuUp != null)
                                        {
                                            MenuUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":MenuUp");
                                        }
#endif
                                    }
                                }
                              
                                //IndexTriggerUp Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_Grip))) > 0;
                                if (Result)
                                {
                                    if (!IndexTriggerUpIsDown)
                                    {
                                        IndexTriggerUpIsDown = Result;

                                        if (IndexTriggerUpDown != null)
                                        {
                                            IndexTriggerUpDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerUpDown");
                                        }
#endif
                                    }


                                    if (IndexTriggerUpPressed != null)
                                    {
                                        IndexTriggerUpPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":IndexTriggerUpPressed");
                                    }
#endif
                                }
                                else
                                {



                                    if (IndexTriggerUpIsDown)
                                    {
                                        IndexTriggerUpIsDown = false;
                                        if (IndexTriggerUpUp != null)
                                        {
                                            IndexTriggerUpUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerUpUp");
                                        }
#endif
                                    }
                                }

                                //IndexTriggerDown Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_SteamVR_Trigger))) > 0;
                                if (Result)
                                {
                                    if (!IndexTriggerDownIsDown)
                                    {
                                        IndexTriggerDownIsDown = Result;

                                        if (IndexTriggerDownDown != null)
                                        {
                                            IndexTriggerDownDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerDownDown");
                                        }
#endif
                                    }

                                    if (IndexTriggerDownPressed != null)
                                    {
                                        IndexTriggerDownPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":IndexTriggerDownPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (IndexTriggerDownIsDown)
                                    {
                                        IndexTriggerDownIsDown = false;
                                        if (IndexTriggerDownUp != null)
                                        {
                                            IndexTriggerDownUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":IndexTriggerDownUp");
                                        }
#endif
                                    }
                                }


                               
                                //ThumbstickUp Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_SteamVR_Touchpad))) > 0 &&  Mathf.Abs( ThumbstickAxis.x )< 0.35f && ThumbstickAxis.y>0.8f;
                                if (Result)
                                {
                                    if (!ThumbstickUpIsDown)
                                    {
                                        ThumbstickUpIsDown = Result;

                                        if (ThumbstickUpDown != null)
                                        {
                                            ThumbstickUpDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickUpDown");
                                        }
#endif
                                    }

                                    if (ThumbstickUpPressed != null)
                                    {
                                        ThumbstickUpPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickUpPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickUpIsDown)
                                    {
                                        ThumbstickUpIsDown = false;
                                        if (ThumbstickUpUp != null)
                                        {
                                            ThumbstickUpUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickUpUp");
                                        }
#endif
                                    }
                                }

                                //ThumbstickDown Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_SteamVR_Touchpad))) > 0 && Mathf.Abs(ThumbstickAxis.x) < 0.35f && ThumbstickAxis.y < -0.8f; 
                                if (Result)
                                {
                                    if (!ThumbstickDownIsDown)
                                    {
                                        ThumbstickDownIsDown = Result;

                                        if (ThumbstickDownDown != null)
                                        {
                                            ThumbstickDownDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickDownDown");
                                        }
#endif
                                    }

                                    if (ThumbstickDownPressed != null)
                                    {
                                        ThumbstickDownPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickDownPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickDownIsDown)
                                    {
                                        ThumbstickDownIsDown = false;
                                        if (ThumbstickDownUp != null)
                                        {
                                            ThumbstickDownUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickDownUp");
                                        }
#endif
                                    }
                                }

                                //ThumbstickLeft Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_SteamVR_Touchpad))) > 0 && Mathf.Abs(ThumbstickAxis.y) < 0.35f && ThumbstickAxis.x < -0.8f;
                                if (Result)
                                {
                                    if (!ThumbstickLeftIsDown)
                                    {
                                        ThumbstickLeftIsDown = Result;

                                        if (ThumbstickLeftDown != null)
                                        {
                                            ThumbstickLeftDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickLeftDown");
                                        }
#endif
                                    }

                                    if (ThumbstickLeftPressed != null)
                                    {
                                        ThumbstickLeftPressed.Invoke();
                                    }
#if DEBUGE
                            if (EditorShowDebug)
                            {
                                Debug.Log(controller + ":ThumbstickLeftPressed");
                            }
#endif
                                }
                                else
                                {

                                    if (ThumbstickLeftIsDown)
                                    {
                                        ThumbstickLeftIsDown = false;
                                        if (ThumbstickLeftUp != null)
                                        {
                                            ThumbstickLeftUp.Invoke();
                                        }
#if DEBUGE
                                if (EditorShowDebug)
                                {
                                    Debug.Log(controller + ":ThumbstickLeftUp");
                                }
#endif
                                    }
                                }

                                //ThumbstickRight Button 
                                Result = (vRControllerState.ulButtonPressed & (1UL << ((int)VRButtonId.k_EButton_SteamVR_Touchpad))) > 0 && Mathf.Abs(ThumbstickAxis.y) < 0.35f && ThumbstickAxis.x > 0.8f;
                                if (Result)
                                {
                                    if (!ThumbstickRightIsDown)
                                    {
                                        ThumbstickRightIsDown = Result;

                                        if (ThumbstickRightDown != null)
                                        {
                                            ThumbstickRightDown.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickRightDown");
                                        }
#endif
                                    }

                                    if (ThumbstickRightPressed != null)
                                    {
                                        ThumbstickRightPressed.Invoke();
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickRightPressed");
                                    }
#endif
                                }
                                else
                                {

                                    if (ThumbstickRightIsDown)
                                    {
                                        ThumbstickRightIsDown = false;
                                        if (ThumbstickRightUp != null)
                                        {
                                            ThumbstickRightUp.Invoke();
                                        }
#if UNITY_EDITOR
                                        if (EditorShowDebug)
                                        {
                                            Debug.Log(id + ":ThumbstickRightUp");
                                        }
#endif
                                    }
                                }


                            }
                            if (EnAxis)
                            {


                                Vector2 Axis2DValue = new Vector2(vRControllerState.rAxis0.x, vRControllerState.rAxis0.y);
                                if (ThumbstickAxis != Axis2DValue)
                                {
                                    ThumbstickAxis = Axis2DValue;

                                    if (ThumbstickAxisChanged != null)
                                    {
                                        ThumbstickAxisChanged.Invoke(ThumbstickAxis);
                                    }
#if UNITY_EDITOR
                                    if (EditorShowDebug)
                                    {
                                        Debug.Log(id + ":ThumbstickAxis");
                                    }
#endif
                                }
                           
                           }
                        }

                    }
                }
            }

        }


      


    }
}
