using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace UnityTemplateProjects.Systems
{
    public abstract class BaseEcsSystem<T1, T2, T3, T4, T5> : BaseEcsSystem where T1: struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().Inc<T5>().End();
        }
    }
    
    public abstract class BaseEcsSystem<T1, T2, T3, T4> : BaseEcsSystem where T1: struct where T2 : struct where T3 : struct where T4 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().Inc<T4>().End();
        }
    }
    
    public abstract class BaseEcsSystem<T1, T2, T3> : BaseEcsSystem where T1: struct where T2 : struct where T3 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().Inc<T3>().End();
        }
    }
    
    public abstract class BaseEcsSystem<T1, T2> : BaseEcsSystem where T1: struct where T2 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().Inc<T2>().End();
        }
    }
    
    public abstract class BaseEcsSystem<T1> : BaseEcsSystem where T1 : struct
    {
        protected sealed override EcsFilter InitFilter(EcsWorld ecsWorld)
        {
            return ecsWorld.Filter<T1>().End();
        }
    }

    public abstract class BaseEcsSystem : IEcsInitSystem, IEcsRunSystem
    {
        protected EcsFilter Entities;
        protected Dictionary<Type, IEcsPool> Pools = new Dictionary<Type, IEcsPool>();

        public void Init(IEcsSystems systems)
        {
            Entities = InitFilter(systems.GetWorld());

            foreach (var pool in GetPools(systems.GetWorld()))
            {
                Pools.Add(pool.GetComponentType(), pool);
            }

            PostInit(systems);
        }

        protected virtual void PostInit(IEcsSystems systems)
        {
        }

        protected abstract EcsFilter InitFilter(EcsWorld ecsWorld);

        public void Run(IEcsSystems systems)
        {
            Execute(Entities, Pools);
        }

        protected abstract void Execute(EcsFilter entities, Dictionary<Type, IEcsPool> pools);

        public abstract IEnumerable<IEcsPool> GetPools(EcsWorld ecsWorld);
    }
}