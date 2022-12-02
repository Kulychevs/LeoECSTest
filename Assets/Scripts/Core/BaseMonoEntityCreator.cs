using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public abstract class BaseMonoEntityCreator : MonoBehaviour
    {
        [Inject]
        private void Constructor(EcsWorld ecsWorld)
        {
            Create(ecsWorld);
        }

        private void Create(EcsWorld ecsWorld)
        {
            var entity = ecsWorld.NewEntity();
            
            OnCreate(ecsWorld, entity);
        }

        protected abstract void OnCreate(EcsWorld ecsWorld, int entity);

        protected ref T AddComponent<T>(EcsWorld ecsWorld, int entity) where T : struct
        {
            var pool = ecsWorld.GetPool<T>();
            return ref pool.Add(entity);
            IEcsPool p;
        }
    }
}