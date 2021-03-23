/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   MicroLightApplicationManager.cs                  
ProductionDate :  2017-08-15 17:24:08
Author         :   T-CODE
************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace MicroLight
{
    public class MicroLightApplicationManager : MonoBehaviour
    {

        public KeyCode QuitKey = KeyCode.Escape;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


            if (Input.GetKeyUp(QuitKey))
            {

#if UNITY_EDITOR

                if (Application.isEditor)
                {
                    if (EditorApplication.isPlaying)
                    {
                        EditorApplication.isPlaying = false;
                    }
                }
#else
            Application.Quit();
#endif

            }
        }
    }
}