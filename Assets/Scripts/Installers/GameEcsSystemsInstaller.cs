using Systems;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine.PlayerLoop;
using UnityTemplateProjects.Systems;

namespace UnityTemplateProjects.Installers
{
    public class GameEcsSystemsInstaller : BaseEcsSystemsInstaller
    {
        protected override void OnInstallBindings()
        {
            Container.BindSystem<SomeValueShowSystem, UpdateSystemsContainer>();
        }
    }
}