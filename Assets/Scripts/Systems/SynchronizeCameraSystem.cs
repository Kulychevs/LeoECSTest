using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class SynchronizeCameraSystem : BaseEcsSystem
    {
        private EcsFilter _PlayerFilter;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<TransformViewComponent>();
            yield return ecsWorld.GetPool<CameraComponent>();
            yield return ecsWorld.GetPool<PositionComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                ref var transform = ref pools.GetComponent<TransformViewComponent>(entity);
                var camera = pools.GetComponent<CameraComponent>(entity);
                foreach (var player in _PlayerFilter)
                {
                    var position = pools.GetComponent<PositionComponent>(player);
                    transform.Value.position = position.Value + camera.Offset;
                }
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            _PlayerFilter = ecsWorld.Filter<PlayerTagComponent>().
                Inc<PositionComponent>().
                End();

            return ecsWorld.Filter<CameraComponent>().
                Inc<TransformViewComponent>().
                End();
        }
    }
}