using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Components
{
    public class AnimationComponent : ComponentBase
    {
        private Animator _animator;

        public void PlayAnimation(string ANIMATION_NAME)
        {
            AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName(ANIMATION_NAME)) return;

            _animator.Play(ANIMATION_NAME, 0);
        }
        public override void UpdateComponent() { }

        public AnimationComponent(Animator animator)
        {
            _animator = animator;
        }
    }
}