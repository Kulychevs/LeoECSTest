using System;
using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public class EcsComponentCreator : MonoBehaviour
    {
        private int _entity;
        private EcsWorld _ecsWorld;

        private bool _entityCreated;
        
        [Inject]
        private void Constructor(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public void Create<T>(T component) where T : struct
        {
            if (!_entityCreated)
            {
                _entity = _ecsWorld.NewEntity();
                _entityCreated = true;
            }

            var pool = _ecsWorld.GetPool<T>();

            if (pool.Has(_entity))
                return;
            
            ref var c = ref pool.Add(_entity);
            c = component;
        }

        public void Refresh<T>(T component) where T: struct
        {
            var pool = _ecsWorld.GetPool<T>();

            if (!pool.Has(_entity))
                return;
            
            ref var c = ref pool.Get(_entity);
            c = component;
        }

        public void Destroy<T>(T component) where T : struct
        {
            int[] entities = Array.Empty<int>();
            
            _ecsWorld.GetAllEntities(ref entities);

            if (!entities.Contains(_entity))
                return;
            
            var pool = _ecsWorld.GetPool<T>();
            
            if (!pool.Has(_entity))
                return;
            
            pool.Del(_entity);
        }
    }
}