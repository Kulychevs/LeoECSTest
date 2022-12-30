using Leopotam.EcsLite.UnityEditor;
using UnityEngine;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public abstract class BaseEcsSystemsInstaller : MonoInstaller
    {
        [SerializeField] private bool _isGlobal;
        [SerializeField] private EcsExecutor _ecsExecutor;

        public override void InstallBindings()
        {
            OnInstallBindings();
            
            Container.Bind(typeof(BaseSystemsContainer), typeof(UpdateSystemsContainer)).To<UpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            Container.Bind(typeof(BaseSystemsContainer), typeof(FixedUpdateSystemsContainer)).To<FixedUpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            Container.Bind(typeof(BaseSystemsContainer), typeof(LateUpdateSystemsContainer)).To<LateUpdateSystemsContainer>().AsTransient().WithArguments(_isGlobal);
            
            Container.BindSystem<EcsWorldDebugSystem, UpdateSystemsContainer>();

            Container.Bind<EcsExecutor>().FromComponentInNewPrefab(_ecsExecutor).AsSingle().NonLazy();
        }

        protected abstract void OnInstallBindings();
    }
}

    