using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Zenject;

namespace Systems
{
    public class RotationSystem : BaseEcsSystem<RotationComponent, DestinationRotationComponent, RotationNormalizedSpeedComponent>
    {
        [Inject]
        private ITimeService _TimeService;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<RotationComponent>();
            yield return ecsWorld.GetPool<DestinationRotationComponent>();
            yield return ecsWorld.GetPool<RotationNormalizedSpeedComponent>();
            yield return ecsWorld.GetPool<LerpParameter>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var lerpParameterPool = pools.GetPool<LerpParameter>();
            foreach (var entity in entities)
            {
                if (!lerpParameterPool.Has(entity))
                    lerpParameterPool.Add(entity);

                ref var lerpParameter = ref lerpParameterPool.Get(entity);
                if (lerpParameter.Value >= 1.0f)
                    continue;

                ref var rotation = ref pools.GetComponent<RotationComponent>(entity);
                var desinationRotation = pools.GetComponent<DestinationRotationComponent>(entity);
                var rotationSpeed = pools.GetComponent<RotationNormalizedSpeedComponent>(entity);

                lerpParameter.Value += _TimeService.DeltaTime * rotationSpeed.Value;
                var newRotation = Quaternion.Lerp(rotation.Value, desinationRotation.Value, lerpParameter.Value);
                rotation.Value = newRotation;
            }
        }
    }
}