using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MicroLight
{
    public class ControllerBase : MonoBehaviour
    {

        public Controller id = Controller.Hmd;

        private Vector3 pos = Vector3.zero;
        private Quaternion rotation = Quaternion.identity;

        private Vector3 localPosition = Vector3.zero;
        private Quaternion localRotation = Quaternion.identity;
        private Quaternion Rotation = Quaternion.identity;

        public bool UpdatePosition = true;
        public bool UpdateRotation = false;

        public string serialnumber;

        public bool Active = false;

        public virtual void OnEnable()
        {

            localPosition = transform.localPosition;
            localRotation = transform.localRotation;

            Rotation = transform.rotation;
        }



        // Use this for initialization
        public virtual void Start()
        {
            if (Application.isPlaying)
            {
                if (MicroLightManager.Instance.OpenTracker)
                {
                    if (MicroLightManager.Instance.mTracker != IntPtr.Zero)
                    {

                   
                        serialnumber = MicroLightPlugin.HoloGraphicUtilities.GetTrackingDeviceSerialnumber((int)id);





                         Active = MicroLightPlugin.Tracker.IsActive(MicroLightManager.Instance.mTracker, serialnumber);
                       
                    }
                }
            }
        }

        public virtual void UpdatePoseFunction()
        {
            if (MicroLightManager.Instance.deviceType == TrackingDeviceType.MicroLightIR)
            {



                if (UpdatePosition)
            {
                Vector3 position = MicroLightPlugin.Tracker.GetPosition(MicroLightManager.Instance.mTracker, serialnumber);

                transform.localPosition = position;
                }
                if (UpdateRotation)
            {
                Quaternion quaternion = MicroLightPlugin.Tracker.GetRotation(MicroLightManager.Instance.mTracker, serialnumber);

                transform.localRotation = quaternion * localRotation;
                }
            }
            else if (MicroLightManager.Instance.deviceType == TrackingDeviceType.LightHouse)
            {

                TrackedDevicePose pose= MicroLightPlugin.Tracker.GetControllerPose(MicroLightManager.Instance.mTracker,(int)TrackingUniverseOrigin.TrackingUniverseStanding, serialnumber);
 
              

                if (UpdatePosition)
                {
                    Vector3 position = Calibration.GetPosition(Calibration.GetMatrix(pose.mDeviceToAbsoluteTracking));

                    transform.localPosition = position;
                }
                if (UpdateRotation)
                {
                    Quaternion quaternion = Calibration.GetRotation(Calibration.GetMatrix(pose.mDeviceToAbsoluteTracking));

                    transform.localRotation = quaternion * localRotation;
                    transform.localEulerAngles += new Vector3(0,180,0);
                }
            }


        }

      
        // Update is called once per frame
        public virtual    void Update()
        {

            if (Application.isPlaying)
            {
                if (MicroLightManager.Instance.OpenTracker)
                {
                    if (MicroLightManager.Instance.mTracker != IntPtr.Zero)
                    {
                        UpdatePoseFunction();
                    }
                }
            }
        }
    }
}
