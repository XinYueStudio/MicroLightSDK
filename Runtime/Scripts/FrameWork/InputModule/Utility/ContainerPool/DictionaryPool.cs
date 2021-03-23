/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   DictionaryPool.cs 
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;

namespace MicroLight.UnityPlugin.Utility
{
    public static class DictionaryPool<TKey, TValue>
    {
        private static readonly ObjectPool<Dictionary<TKey, TValue>> pool = new ObjectPool<Dictionary<TKey, TValue>>(() => new Dictionary<TKey, TValue>(), null, e => e.Clear());

        public static Dictionary<TKey, TValue> Get()
        {
            return pool.Get();
        }

        public static void Release(Dictionary<TKey, TValue> toRelease)
        {
            pool.Release(toRelease);
        }
    }
}