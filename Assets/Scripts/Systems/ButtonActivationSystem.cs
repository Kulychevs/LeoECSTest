using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class ButtonActivationSystem : BaseEcsSystem
    {
        private EcsFilter _DoorFilter;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<ActivateTagComponent>();
            yield return ecsWorld.GetPool<ButtonComponent>();
            yield return ecsWorld.GetPool<DestinationComponent>();
            yield return ecsWorld.GetPool<DoorComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var ativateTagPool = pools.GetPool<ActivateTagComponent>();
            var destinationPool = pools.GetPool<DestinationComponent>();

            foreach (var entity in entities)
            {
                var button = pools.GetComponent<ButtonComponent>(entity);
                if (!destinationPool.Has(entity))
                    destinationPool.Add(entity);

                ref var destination = ref destinationPool.Get(entity);
                destination.Value = button.PressedPosition;

                foreach (var door in _DoorFilter)
                {
                    var doorId = pools.GetComponent<DoorComponent>(door).ID;
                    if (doorId != button.DoorID)
                        continue;

                    ativateTagPool.Add(door);
                }

                ativateTagPool.Del(entity);
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            _DoorFilter = ecsWorld.Filter<DoorComponent>().
                End();

            return ecsWorld.Filter<ButtonComponent>().
                Inc<ActivateTagComponent>().
                End();
        }
    }
}