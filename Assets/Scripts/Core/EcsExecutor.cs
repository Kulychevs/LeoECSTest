using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;
using UnityTemplateProjects.Installers;
using Zenject;

namespace UnityTemplateProjects
{
    public class EcsExecutor : MonoBehaviour
    {
        private EcsSystems _ecsUpdateSystems;
        private EcsSystems _ecsFixedUpdateSystems;
        private EcsSystems _ecsLateUpdateSystems;
        private List<BaseSystemsContainer> _systemsContainers;
        private EcsWorld _ecsWorld;

        [Inject]
        private void Constructor(List<BaseSystemsContainer> systemsContainers, EcsWorld ecsWorld)
        {
            _systemsContainers = systemsContainers;
            _ecsWorld = ecsWorld;
        }
        
        public void Start()
        {
            _ecsUpdateSystems = GetEcsSystems(SystemContainerType.Update, _ecsWorld, _systemsContainers);
            _ecsFixedUpdateSystems = GetEcsSystems(SystemContainerType.FixedUpdate, _ecsWorld, _systemsContainers);
            _ecsLateUpdateSystems = GetEcsSystems(SystemContainerType.LateUpdate, _ecsWorld, _systemsContainers);
            
            _ecsUpdateSystems.Init();
            _ecsFixedUpdateSystems.Init();
            _ecsLateUpdateSystems.Init();
        }

        private void Update()
        {
            _ecsUpdateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _ecsFixedUpdateSystems?.Run();
        }

        private void LateUpdate()
        {
            _ecsLateUpdateSystems?.Run();
        }

        private EcsSystems GetEcsSystems(SystemContainerType type, EcsWorld ecsWorld, List<BaseSystemsContainer> systemsContainers)
        {
            var ecsSystems = new EcsSystems(ecsWorld);
            var systemContainer = systemsContainers.FirstOrDefault(d => d.Type == type);

            if (systemContainer == null)
            {
                Debug.LogError($"Can't find container with type {type.ToString()}");
                return null;
            }
            
            foreach (var system in systemContainer.Systems)
            {
                ecsSystems.Add(system);
            }

            return ecsSystems;
        }
    }
}