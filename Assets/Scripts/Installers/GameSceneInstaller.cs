using Leopotam.EcsLite;
using Zenject;

namespace UnityTemplateProjects.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance<ITimeService>(new UnityTimeService()).AsSingle().NonLazy();
            Container.BindInstance<IPositionInputService>(new UnityPositionInputService()).AsSingle().NonLazy();
            Container.BindInstance(new EcsWorld()).AsSingle().NonLazy();
        }
    }
}