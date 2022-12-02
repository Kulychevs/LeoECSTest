using Leopotam.EcsLite;
using System.Collections.Generic;

namespace UnityTemplateProjects.Installers
{
    public class FixedUpdateSystemsContainer : BaseSystemsContainer
    {
        public FixedUpdateSystemsContainer(IEcsSystem[] systems, bool isGlobal) : base(systems, isGlobal)
        {
        }

        public override SystemContainerType Type => SystemContainerType.FixedUpdate;
    }
}