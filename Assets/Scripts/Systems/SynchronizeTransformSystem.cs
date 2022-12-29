using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class SynchronizeTransformSystem : BaseEcsSystem<TransformViewComponent, PositionComponent, RotationComponent>
    {
        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<PositionComponent>();
            yield return ecsWorld.GetPool<RotationComponent>();
            yield return ecsWorld.GetPool<TransformViewComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var position = pools.GetComponent<PositionComponent>(entity);
                var rotaton = pools.GetComponent<RotationComponent>(entity);
                var transform = pools.GetComponent<TransformViewComponent>(entity);

                transform.Value.position = position.Value;
                transform.Value.rotation = rotaton.Value;
            }
        }
    }
}