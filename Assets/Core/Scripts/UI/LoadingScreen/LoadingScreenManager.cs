using Level.Stage;
using UnityEngine;

namespace UI.Transition
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject _transitionScreen;

        [SerializeField] private Animator _transitionAnimator;
        [SerializeField] private string _animationInName;
        [SerializeField] private string _animationOutName;

        private void Start()
        {
            ScreenFadeOut();
            TransitionManager.Instance.OnRoomSwitchStart += ScreenFadeIn;
            TransitionManager.Instance.OnRoomSwitchEnd += ScreenFadeOut;
        }

        private void ScreenFadeIn()
        {
            _transitionAnimator.Play(_animationInName);
        }
        private void ScreenFadeOut()
        {
            _transitionAnimator.Play(_animationOutName);
        }
    }
}