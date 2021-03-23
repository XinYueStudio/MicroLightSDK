using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour {

    public UnityEvent OnEnableHandler;

    public void OnEnable()
    {
        if (OnEnableHandler != null)
            OnEnableHandler.Invoke();
    }
}
