/************************************************************************************
Copyright      :   Copyright 2017 MicroLight, LLC. All Rights reserved.
Description    :    
ProjectName    :   MicroLight
ProductionDate :   2017-12-04 11:51:20
Author         :   T-CODE
************************************************************************************/
namespace MicroLight.UnityPlugin.Utility
{
    public static class IndexedSetPool<T>
    {
        private static readonly ObjectPool<IndexedSet<T>> pool = new ObjectPool<IndexedSet<T>>(() => new IndexedSet<T>(), null, e => e.Clear());

        public static IndexedSet<T> Get()
        {
            return pool.Get();
        }

        public static void Release(IndexedSet<T> toRelease)
        {
            pool.Release(toRelease);
        }
    }
}