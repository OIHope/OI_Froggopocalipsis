using UnityEngine;

namespace Components
{
    public class AnimationComponent : ComponentBase
    {
        private Animator _animator;

        public bool IsAnimationComplete()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                return true;
            }
            return false;
        }
        public void PlayAnimation(string ANIMATION_NAME)
        {
            if(!CheckAnimationIsValid(ANIMATION_NAME)) return;

            _animator.Play(ANIMATION_NAME, 0);
        }
        public override void UpdateComponent() { }

        private bool CheckAnimationIsValid(string ANIMATION_NAME)
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName(ANIMATION_NAME)) return false;
            return true;
        }
        public AnimationComponent(Animator animator)
        {
            _animator = animator;
        }
    }
}