using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class DoorActivationSystem : BaseEcsSystem
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DoorComponent>();
            yield return ecsWorld.GetPool<ActivateTagComponent>();
            yield return ecsWorld.GetPool<DestinationRotationComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var ativateTagPool = pools.GetPool<ActivateTagComponent>();
            var rotationDestinationPool = pools.GetPool<DestinationRotationComponent>();
            foreach (var door in entities)
            {
                if (!rotationDestinationPool.Has(door))
                {
                    ref var rotationDestination = ref rotationDestinationPool.Add(door);
                    var openedRotation = pools.GetComponent<DoorComponent>(door).OpenedRotation;
                    rotationDestination.Value = openedRotation;
                }

                ativateTagPool.Del(door);
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<DoorComponent>().
                Inc<ActivateTagComponent>().
                End();
        }
    }
}