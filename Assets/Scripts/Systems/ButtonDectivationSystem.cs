using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class ButtonDectivationSystem : BaseEcsSystem
    {
        private EcsFilter _DoorFilter;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DeactivateTagComponent>();
            yield return ecsWorld.GetPool<DestinationComponent>();
            yield return ecsWorld.GetPool<ButtonComponent>();
            yield return ecsWorld.GetPool<DoorComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var deativateTagPool = pools.GetPool<DeactivateTagComponent>();
            var destinationPool = pools.GetPool<DestinationComponent>();

            foreach (var entity in entities)
            {
                var button = pools.GetComponent<ButtonComponent>(entity);
                if (!destinationPool.Has(entity))
                    destinationPool.Add(entity);

                ref var destination = ref destinationPool.Get(entity);
                destination.Value = button.OriginalPosition;

                foreach (var door in _DoorFilter)
                {
                    var doorId = pools.GetComponent<DoorComponent>(door).ID;
                    if (doorId != button.DoorID)
                        continue;

                    deativateTagPool.Add(door);
                }

                deativateTagPool.Del(entity);
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            _DoorFilter = ecsWorld.Filter<DoorComponent>().
                End();

            return ecsWorld.Filter<ButtonComponent>().
                Inc<DeactivateTagComponent>().
                End();
        }
    }
}