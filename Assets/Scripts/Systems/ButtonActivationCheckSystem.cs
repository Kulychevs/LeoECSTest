using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class ButtonActivationCheckSystem : BaseEcsSystem
    {
        private EcsFilter _PlayerFilter;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<PositionComponent>();
            yield return ecsWorld.GetPool<ActionDistanceComponent>();
            yield return ecsWorld.GetPool<ActivateTagComponent>();
            yield return ecsWorld.GetPool<DeactivateTagComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            var ativateTagPool = pools.GetPool<ActivateTagComponent>();
            var deativateTagPool = pools.GetPool<DeactivateTagComponent>();
            foreach (var player in _PlayerFilter)
            {
                var playerPosition = pools.GetComponent<PositionComponent>(player);
                foreach (var button in entities)
                {
                    var buttonPosition = pools.GetComponent<PositionComponent>(button);
                    var activationDistance = pools.GetComponent<ActionDistanceComponent>(button);
                    var sqrDistance = (playerPosition.Value - buttonPosition.Value).sqrMagnitude;

                    if (sqrDistance <= activationDistance.Value * activationDistance.Value)
                        ativateTagPool.Add(button);
                    else
                        deativateTagPool.Add(button);
                }
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            _PlayerFilter = ecsWorld.Filter<PlayerTagComponent>().
                Inc<PositionComponent>().
                End();

            return ecsWorld.Filter<ButtonComponent>().
                Inc<PositionComponent>().
                Inc<ActionDistanceComponent>().
                End();
        }
    }
}