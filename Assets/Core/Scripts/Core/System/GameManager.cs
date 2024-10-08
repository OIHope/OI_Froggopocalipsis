using Core.Camera;
using Core.DialogueSystem;
using Core.Language;
using Core.Progression;
using Core.UI;
using Level.Stage;
using PlayerSystem;
using System.Collections;
using UI.Transition;
using UnityEngine;

namespace Core.System
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LoadingScreenManager _loadingScreenManager;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private GameStageManager _gameStageManager;
        [SerializeField] private LanguageManager _languageManager;
        [SerializeField] private UIManager _UIManager;
        [SerializeField] private DialogueManager _dialogueManager;
        [SerializeField] private PlayerProgressionManager _playerProgressionManager;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private ResetProgression _resetManager;
        [SerializeField] private TransitionManager _transitionManager;
        [Space]
        [SerializeField] private GameObject _initScreen;

        private void OnEnable()
        {
            _initScreen.SetActive(true);
            StartCoroutine(Initialization());

            GameEventsBase.OnBackToMaunMenu += () => StartCoroutine(LoadMainMenu());
        }

        private IEnumerator Initialization()
        {
            yield return InitingManagers();
            yield return SetupManagers();
        }
        private IEnumerator InitingManagers()
        {
            yield return _languageManager.InitManager();
            yield return _loadingScreenManager.InitManager();
            yield return _inputManager.InitManager();
            yield return _UIManager.InitManager();
            yield return _dialogueManager.InitManager();
            yield return _playerProgressionManager.InitManager();
            yield return _playerManager.InitManager();
            yield return _cameraManager.InitManager();
            yield return _resetManager.InitManager();
            yield return _transitionManager.InitManager();
            yield return _gameStageManager.InitManager();

            yield return new WaitForSeconds(1.5f);
        }
        private IEnumerator SetupManagers()
        {
            yield return _gameStageManager.SetupManager();

            yield return _languageManager.SetupManager();
            yield return _dialogueManager.SetupManager();
            yield return _playerProgressionManager.SetupManager();

            yield return _cameraManager.SetupManager();
            yield return _playerManager.SetupManager();
            yield return _resetManager.SetupManager();

            yield return _loadingScreenManager.SetupManager();
            yield return _transitionManager.SetupManager();
            _initScreen.SetActive(false);

            yield return _inputManager.SetupManager();
            yield return _UIManager.SetupManager();
        }

        private IEnumerator LoadMainMenu()
        {
            yield return _transitionManager.ChangeStage(TransitionDirection.Forward, EntranceDirection.Front, CurrentLevelStage.MenuScene);
            yield return new WaitForSeconds(0.5f);
            _inputManager.SwitchInputScheme(PlayerInputMode.UI);
            _UIManager.OpenMainMenu();
        }
    }
}