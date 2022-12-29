using Components;
using UnityTemplateProjects.Installers;

namespace ComponentsProviders
{
    public class CameraProvider : EcsComponentProvider<CameraComponent>
    {
        protected override void Awake()
        {
            _ecsComponentCreator = GetComponent<EcsComponentCreator>();

            if (_ecsComponentCreator != null)
            {
                _component.Offset = transform.position;
                _ecsComponentCreator.Create(_component);
            }
        }
    }
}