using UnityEngine;

namespace ComponentsProviders
{
    public class AnimatorView : MonoBehaviour
    {
        [SerializeField] private Animator _Animator;

        public void SetBool(string name, bool value)
        {
            if (_Animator != null)
            {
                _Animator.SetBool(name, value);
            }
        }
    }
}