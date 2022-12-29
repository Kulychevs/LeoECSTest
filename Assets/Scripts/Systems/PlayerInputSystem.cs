using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;
using Zenject;

namespace Systems
{
    public class PlayerInputSystem : BaseEcsSystem<PlayerTagComponent, PositionComponent>
    {
        [Inject]
        private IPositionInputService _Input;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<DestinationComponent>();
            yield return ecsWorld.GetPool<DestinationRotationComponent>();
            yield return ecsWorld.GetPool<PositionComponent>();
            yield return ecsWorld.GetPool<LerpParameter>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            if (!_Input.TryGetPosition(out var position))
                return;

            foreach (var entity in entities)
            {
                HandlePosition(entity, pools, position);
                HandleRotation(entity, pools, position);
            }
        }

        private void HandlePosition(int entity, Dictionary<Type, IEcsPool> pools, Vector3 position)
        {
            var destinationPool = pools.GetPool<DestinationComponent>();
            if (destinationPool.Has(entity))
                destinationPool.Get(entity).Value = position;
            else
                destinationPool.Add(entity).Value = position;
        }

        private void HandleRotation(int entity, Dictionary<Type, IEcsPool> pools, Vector3 position)
        {
            var pos = pools.GetComponent<PositionComponent>(entity);
            var lerpParameterPool = pools.GetPool<LerpParameter>();

            var destinationRotationPool = pools.GetPool<DestinationRotationComponent>();
            var newRot = Quaternion.LookRotation(position - pos.Value);
            if (destinationRotationPool.Has(entity))
                destinationRotationPool.Get(entity).Value = newRot;
            else
                destinationRotationPool.Add(entity).Value = newRot;

            if (lerpParameterPool.Has(entity))
                lerpParameterPool.Get(entity).Value = 0.0f;
        }
    }
}