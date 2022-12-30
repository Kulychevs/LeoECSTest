using Systems;

namespace UnityTemplateProjects.Installers
{
    public class GameEcsSystemsInstaller : BaseEcsSystemsInstaller
    {
        protected override void OnInstallBindings()
        {
            Container.BindSystem<PlayerInputSystem, UpdateSystemsContainer>();

            Container.BindSystem<ButtonActivationCheckSystem, UpdateSystemsContainer>();
            Container.BindSystem<ButtonActivationSystem, UpdateSystemsContainer>();
            Container.BindSystem<ButtonDectivationSystem, UpdateSystemsContainer>();

            Container.BindSystem<DoorActivationSystem, UpdateSystemsContainer>();
            Container.BindSystem<DoorDeactivationSystem, UpdateSystemsContainer>();

            Container.BindSystem<MovementSystem, UpdateSystemsContainer>();
            Container.BindSystem<LerpRotationSystem, UpdateSystemsContainer>();

            Container.BindSystem<SynchronizeTransformSystem, UpdateSystemsContainer>();
            Container.BindSystem<PlayerAnimationSystem, UpdateSystemsContainer>();
            Container.BindSystem<SynchronizeCameraSystem, LateUpdateSystemsContainer>();
        }
    }
}