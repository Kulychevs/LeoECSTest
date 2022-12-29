using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Zenject;

namespace Systems
{
    public class MovementSystem : BaseEcsSystem<PositionComponent, DestinationComponent, SpeedComponent>
    {
        private const float STOP_THRESHOLD = 0.02f;

        [Inject]
        private ITimeService _TimeService;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<PositionComponent>();
            yield return ecsWorld.GetPool<DestinationComponent>();
            yield return ecsWorld.GetPool<SpeedComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                ref var position = ref pools.GetComponent<PositionComponent>(entity);
                var desination = pools.GetComponent<DestinationComponent>(entity);
                var speed = pools.GetComponent<SpeedComponent>(entity);

                var heading = desination.Value - position.Value;
                if (heading.sqrMagnitude <= STOP_THRESHOLD * STOP_THRESHOLD)
                {
                    pools.GetPool<DestinationComponent>().Del(entity);
                }
                else
                {
                    var newPosition = position.Value + heading.normalized * (speed.Value * _TimeService.DeltaTime);
                    position.Value = newPosition;
                }
            }
        }
    }
}