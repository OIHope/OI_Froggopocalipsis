using UnityEngine;

namespace ActionExecuteSystem
{
    public class PlayAnimationAction : ActionBase
    {
        [Space]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationName;
        protected override void ActionToPerform()
        {
            _animator.Play(_animationName, 0);
        }
    }
}