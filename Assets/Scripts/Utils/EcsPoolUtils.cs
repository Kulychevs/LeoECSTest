using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public static class EcsPoolUtils
    {
        public static ref T GetComponent<T>(this Dictionary<Type, IEcsPool> pools, int entity) where T : struct
        {
            return ref ((EcsPool<T>) pools[typeof(T)]).Get(entity);
        }

        public static EcsPool<T> GetPool<T>(this Dictionary<Type, IEcsPool> pools) where T : struct
        {
            return (EcsPool<T>) pools[typeof(T)];
        }
    }
}