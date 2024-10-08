using Core.System;
using Level.Stage;
using System.Collections;
using UnityEngine;

namespace UI.Transition
{
    public class LoadingScreenManager : Manager
    {
        [SerializeField] private GameObject _transitionScreen;

        [SerializeField] private Animator _transitionAnimator;
        [SerializeField] private string _animationInName;
        [SerializeField] private string _animationOutName;

        private void ScreenFadeIn()
        {
            _transitionAnimator.Play(_animationInName);
        }
        private void ScreenFadeOut()
        {
            _transitionAnimator.Play(_animationOutName);
        }

        public override IEnumerator InitManager()
        {
            _transitionScreen.SetActive(false);
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            _transitionScreen.SetActive(true);
            ScreenFadeIn();
            TransitionManager.Instance.OnRoomSwitchStart += ScreenFadeIn;
            TransitionManager.Instance.OnRoomSwitchEnd += ScreenFadeOut;
            yield return new WaitForSeconds(0.5f);
        }
    }
}