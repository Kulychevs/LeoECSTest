using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class DoorDeactivationSystem : BaseEcsSystem<DoorComponent, DeactivateTagComponent>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DestinationRotationComponent>();
            yield return ecsWorld.GetPool<DeactivateTagComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var destinationRotationPool = pools.GetPool<DestinationRotationComponent>();
            var deativateTagPool = pools.GetPool<DeactivateTagComponent>();
            foreach (var door in entities)
            {
                if (destinationRotationPool.Has(door))
                    destinationRotationPool.Del(door);
                deativateTagPool.Del(door);
            }
        }
    }
}