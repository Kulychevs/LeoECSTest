using Components;
using UnityTemplateProjects.Installers;

namespace ComponentsProviders
{
    public class TransformProvider : EcsComponentProvider<TransformViewComponent>
    {
        protected override void Awake()
        {
            base.Awake();
            if (_ecsComponentCreator != null)
            {
                var positionComponent = new PositionComponent() { Value = Component.Value.position };
                _ecsComponentCreator.Create(positionComponent);

                var rotationComponent = new RotationComponent() { Value = Component.Value.rotation };
                _ecsComponentCreator.Create(rotationComponent);
            }
        }
    }
}