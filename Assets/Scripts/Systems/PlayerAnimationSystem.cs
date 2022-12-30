using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class PlayerAnimationSystem : BaseEcsSystem<AnimatorViewComponent, PlayerTagComponent>
    {
        private const string RUN = "IsRun";

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
                var destinationPool = pools.GetPool<DestinationComponent>();
                var runValue = destinationPool.Has(entity);
                animatorView.Value.SetBool(RUN, runValue);
            }
        }
    }
}