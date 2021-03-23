using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MicroLight
{
    public class MicroLightDontDestroyOnLoad : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


    }
}
