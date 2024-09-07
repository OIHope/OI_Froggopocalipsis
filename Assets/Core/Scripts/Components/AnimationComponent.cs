using UnityEngine;

namespace Components
{
    public class AnimationComponent : ComponentBase
    {
        private Animator _animator;
        public Animator AnimatorComponent { get =>  _animator; set { _animator = value; } }

        public bool IsAnimationComplete(string ANIMATION_NAME)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName(ANIMATION_NAME))
            {
                if (stateInfo.normalizedTime >= 1.0f)
                {
                    return true;
                }
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