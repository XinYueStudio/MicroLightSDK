using MicroLight.FrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GllobalEventEntityTest : MonoBehaviour {
    /// <summary>
    /// Event Type Define 
    /// </summary>
    public enum GllobalEventType
    {
        Event0,
        Event1,
        Event2,
    }
    // Use this for initialization
    void Start () {
        GllobalEventEntity.Addlistener( GllobalEventType.Event0,Callback0);
        GllobalEventEntity.Addlistener(GllobalEventType.Event0, Callback2);
        GllobalEventEntity.Addlistener<int>(GllobalEventType.Event1, Callback1);
        GllobalEventEntity.Addlistener<int>(GllobalEventType.Event1, Callback4);

    }

    private void Callback4(int arg0)
    {
        Debug.Log("Callback4 " + arg0.ToString());
    }

    private void Callback2()
    {
        Debug.Log("Callback2");
    }

    private void Callback1(int arg0)
    {
        Debug.Log("Callback1 "+ arg0.ToString());
    }

    private void Callback0()
    {
        Debug.Log("Callback0");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Start();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GllobalEventEntity.Dispatcher(GllobalEventType.Event0);

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GllobalEventEntity.Dispatcher<int>(GllobalEventType.Event1,11);

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            GllobalEventEntity.Removelistener(GllobalEventType.Event0, Callback2);

            GllobalEventEntity.Removelistener<int>(GllobalEventType.Event1, Callback4);

        }
    }
}
