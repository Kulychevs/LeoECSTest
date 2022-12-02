using System;
using UnityEngine;

namespace UnityTemplateProjects.Installers
{
    [RequireComponent(typeof(EcsComponentCreator))]
    public abstract class EcsComponentProvider<T> : MonoBehaviour where T : struct
    {
        [SerializeField] private T _component;
        
        private EcsComponentCreator _ecsComponentCreator;

        public T Component => _component;

        private void Awake()
        {
            _ecsComponentCreator = GetComponent<EcsComponentCreator>();

            if (_ecsComponentCreator != null)
            {
                _ecsComponentCreator.Create(_component);
            }
        }

        private void OnValidate()
        {
            if (_ecsComponentCreator == null)
                return;
            
            _ecsComponentCreator.Refresh(_component);
        }

        private void OnEnable()
        {
            _ecsComponentCreator.Create(_component);
        }

        private void OnDisable()
        {
            if(!enabled)
                _ecsComponentCreator.Destroy(_component);
        }

        private void OnDestroy()
        {
            _ecsComponentCreator.Destroy(_component);
        }
    }
}