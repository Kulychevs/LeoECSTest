using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public class UpdateSystemsContainer : BaseSystemsContainer
    {
        public UpdateSystemsContainer(IEcsSystem[] systems, bool isGlobal) : base(systems, isGlobal)
        {
        }

        public override SystemContainerType Type => SystemContainerType.Update;
    }
}