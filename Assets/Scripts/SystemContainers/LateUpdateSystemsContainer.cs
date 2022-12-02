using Leopotam.EcsLite;

namespace UnityTemplateProjects.Installers
{
    public class LateUpdateSystemsContainer : BaseSystemsContainer
    {
        public LateUpdateSystemsContainer(IEcsSystem[] systems, bool isGlobal) : base(systems, isGlobal)
        {
        }

        public override SystemContainerType Type => SystemContainerType.LateUpdate;
    }
}