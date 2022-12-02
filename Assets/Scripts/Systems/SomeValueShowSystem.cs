using System;
using System.Collections.Generic;
using Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityTemplateProjects.Installers;
using UnityTemplateProjects.Systems;

namespace Systems
{
    public class SomeValueShowSystem : BaseEcsSystem<SomeValueComponent>
    {
        protected override void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools)
        {
            foreach (var entity in entities)
            {
                var someValue = pools.GetComponent<SomeValueComponent>(entity);
            }
        }

        public override IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld)
        {
            yield return ecsWorld.GetPool<SomeValueComponent>();
        }
    }
}