/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :   ListPool.cs
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
using System.Collections.Generic;

namespace MicroLight.UnityPlugin.Utility
{
    public static class ListPool<T>
    {
        private static readonly ObjectPool<List<T>> pool = new ObjectPool<List<T>>(() => new List<T>(), null, e => e.Clear());

        public static List<T> Get()
        {
            return pool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            pool.Release(toRelease);
        }
    }
}