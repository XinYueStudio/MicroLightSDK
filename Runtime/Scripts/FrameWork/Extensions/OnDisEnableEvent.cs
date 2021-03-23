using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisEnableEvent : MonoBehaviour {

    public UnityEvent OnDisEnableHandler;

    public void OnDisable()
    {
        if (OnDisEnableHandler != null)
            OnDisEnableHandler.Invoke();
    }
}
