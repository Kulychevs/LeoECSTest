using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public static class EcsSystemUtils
    {
        public static void BindSystem<TSystem, TSystemsContainer>(this DiContainer container) where TSystem : IEcsSystem where TSystemsContainer : BaseSystemsContainer
        {
            container.Bind<IEcsSystem>().To<TSystem>().AsSingle().WhenInjectedInto<TSystemsContainer>().NonLazy();
        }
    }
}