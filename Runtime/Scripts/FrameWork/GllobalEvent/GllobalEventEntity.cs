/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   GllobalEventEntity.cs                  
ProductionDate :  2017-12-18 11:24:08
Author         :   T-CODE
************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MicroLight.FrameWork
{
    
    /// <summary>
    /// delegate Define 
    /// </summary>
    public delegate void GllobalDelegate();
    public delegate void GllobalDelegate<T0>(T0 arg0);
    public delegate void GllobalDelegate<T0, T1>(T0 arg0, T1 arg1);
    public delegate void GllobalDelegate<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2);
    public delegate void GllobalDelegate<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    public delegate void GllobalDelegate<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    /// <summary>
    /// Gllobal Event Entity 
    /// </summary>
    public class GllobalEventEntity : IDisposable
    {
       
        private static Dictionary<System.Enum, Delegate> ListenerDictionary= new Dictionary<System.Enum, Delegate>();
        private static void Listen(System.Enum eventType,Delegate Callback)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                if ( callback.Method!=Callback.Method)
                {
                    callback = Delegate.Combine(callback, Callback);
                    ListenerDictionary.Remove(eventType);
                    ListenerDictionary.Add(eventType, callback);
                }
            }
            else
            {
                ListenerDictionary.Add(eventType, Callback);
            }
        }
        private static void Remove(System.Enum eventType, Delegate Callback)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                
                 callback = Delegate.Remove(callback, Callback);
                
                if (callback == null)
                {
                    if (ListenerDictionary.ContainsKey(eventType))
                        ListenerDictionary.Remove(eventType);
                }
                else
                {
                    ListenerDictionary.Remove(eventType);
                    ListenerDictionary.Add(eventType, callback);
                }
            }
        }
      
        public static void Addlistener(System.Enum eventType, GllobalDelegate Callback)
        {
            Listen(eventType, Callback);

        }
        public static void Addlistener<T0>(System.Enum eventType, GllobalDelegate<T0> Callback)
        {
            Listen(eventType, Callback);
        }
        public static void Addlistener<T0, T1>(System.Enum eventType, GllobalDelegate<T0, T1> Callback) 
        {
            Listen(eventType, Callback);
        }
        public static void Addlistener<T0, T1, T2>(System.Enum eventType, GllobalDelegate<T0, T1, T2> Callback)
        {
            Listen(eventType, Callback);
        }
        public static void Addlistener<T0, T1, T2, T3>(System.Enum eventType, GllobalDelegate<T0, T1, T2, T3> Callback)
        {
            Listen(eventType, Callback);
        }
        public static void Addlistener<T0,T1,T2,T3,T4>(System.Enum eventType, GllobalDelegate<T0,T1,T2,T3,T4> Callback)
        {
            Listen(eventType, Callback);
        }

        public static void Removelistener(System.Enum eventType, GllobalDelegate Callback)
        {
            Remove(eventType, Callback);

        }
        public static void Removelistener<T0>(System.Enum eventType, GllobalDelegate<T0> Callback)
        {
            Remove(eventType, Callback);


        }
        public static void Removelistener<T0, T1>(System.Enum eventType, GllobalDelegate<T0, T1> Callback)
        {
            Remove(eventType, Callback);

        }
        public static void Removelistener<T0, T1, T2>(System.Enum eventType, GllobalDelegate<T0, T1, T2> Callback)
        {
            Remove(eventType, Callback);

        }
        public static void Removelistener<T0, T1, T2, T3>(System.Enum eventType, GllobalDelegate<T0, T1, T2, T3> Callback)
        {
            Remove(eventType, Callback);

        }
        public static void Removelistener<T0, T1, T2, T3, T4>(System.Enum eventType, GllobalDelegate<T0, T1, T2, T3, T4> Callback)
        {
            Remove(eventType, Callback);

        }

        public static void Dispatcher(System.Enum eventType)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke();
            }
        }
        public static void Dispatcher<T0>(System.Enum eventType, T0 arg0)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke(arg0);
            }
        }
        public static void Dispatcher<T0, T1>(System.Enum eventType, T0 arg0, T1 arg1)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke(arg0, arg1);
            }
        }
        public static void Dispatcher<T0, T1, T2>(System.Enum eventType, T0 arg0, T1 arg1, T2 arg2)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke(arg0, arg1, arg2);
            }
        }
        public static void Dispatcher<T0, T1, T2, T3>(System.Enum eventType, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke(arg0, arg1, arg2, arg3);
            }
        }
        public static void Dispatcher<T0, T1, T2, T3, T4>(System.Enum eventType, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Delegate callback;
            if (ListenerDictionary.TryGetValue(eventType, out callback))
            {
                callback.DynamicInvoke(arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Dispose()
        {
            ListenerDictionary.Clear();
        }

    }

  
}