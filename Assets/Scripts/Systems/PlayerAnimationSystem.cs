using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class PlayerAnimationSystem : BaseEcsSystem
    {
        private const string RUN = "IsRun";

        private EcsFilter _PlayerFilter;

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<AnimatorViewComponent>();
            yield return ecsWorld.GetPool<DestinationComponent>();
        }

        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var animatorView = pools.GetComponent<AnimatorViewComponent>(entity);
                foreach (var player in _PlayerFilter)
                {
                    var destinationPool = pools.GetPool<DestinationComponent>();
                    var runValue = destinationPool.Has(player);
                    animatorView.Value.SetBool(RUN, runValue);
                }
            }
        }

        protected override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            _PlayerFilter = ecsWorld.Filter<PlayerTagComponent>().
                End();

            return ecsWorld.Filter<AnimatorViewComponent>().
                End();
        }
    }
}