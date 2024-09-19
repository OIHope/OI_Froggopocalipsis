using Components;
using UnityEngine;

namespace Core.Camera
{
    public class CameraAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _cameraAnimator;
        [Space]
        [SerializeField] private string _idleAnimName;
        [SerializeField] private string _hitAnimName;
        [SerializeField] private string _playerTakeDamageAnimName;
        [SerializeField] private string _playerDeathAnimName;
        [SerializeField] private string _enemyDeathAnimName;


        private AnimationComponent _animationComponent;

        private void Awake()
        {
            _animationComponent = new(_cameraAnimator);

            GameEventsBase.OnPlayerHit += (() => PlayAnimation(_playerTakeDamageAnimName));
            GameEventsBase.OnPlayerDeath += (() => PlayAnimation(_playerDeathAnimName));

            GameEventsBase.OnEnemyHit += (() => PlayAnimation(_hitAnimName));
            GameEventsBase.OnEnemyDeath += (() => PlayAnimation(_enemyDeathAnimName));
        }

        private void PlayAnimation(string animName)
        {
            _animationComponent.PlayAnimation(animName);
        }
    }
}