using Components;
using UnityEngine;
using UnityTemplateProjects.Installers;

namespace ComponentsProviders
{
    public class ButtonProvider : EcsComponentProvider<ButtonComponent>
    {
        [SerializeField] private Vector3 _PressedOffset;

        protected override void Awake()
        {
            _ecsComponentCreator = GetComponent<EcsComponentCreator>();

            if (_ecsComponentCreator != null)
            {
                _component.OriginalPosition = transform.position;
                _component.PressedPosition = transform.position + _PressedOffset;
                _ecsComponentCreator.Create(_component);
            }
        }
    }
}