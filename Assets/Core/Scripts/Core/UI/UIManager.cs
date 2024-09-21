using Core.DialogueSystem;
using PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _inGameMenuPanel;
        [SerializeField] private GameObject _dialoguePanel;

        private InputAction _menuAction;

        private bool _active = false;

        private void Start()
        {
            CloseAllPanels();
            SwitchMenuAction();

            DialogueManager.Instance.OnDialoguePanelOpenRequested += (()=>ToggleDialoguePanel(true));
            DialogueManager.Instance.OnDialoguePanelOpenRequested += (() => ToggleDialogueInput(true));

            DialogueManager.Instance.OnDialoguePanelCloseRequested += (() => ToggleDialoguePanel(false));
            DialogueManager.Instance.OnDialoguePanelCloseRequested += (() => ToggleDialogueInput(false));
        }

        #region Action Handle Region

        private void SwitchMenuAction()
        {
            if (_active)
            {
                _menuAction = InputManager.Instance.Input.UIInputMap.Menu;
            }
            else
            {
                _menuAction = InputManager.Instance.Input.MainInputMap.Menu;
            }
        }

        #endregion

        #region Panels Open Close Handle Region
        private void CloseAllPanels()
        {
            _inGameMenuPanel.SetActive(false);
            _dialoguePanel.SetActive(false);
        }

        private void ToggleInGameMenuPanel(bool toggle)
        {
            CloseAllPanels();
            SwitchMenuAction();
            ToogleTimeScale(toggle);
            _inGameMenuPanel.SetActive(toggle);
        }
        private void ToggleDialoguePanel(bool toggle)
        {
            CloseAllPanels();
            _dialoguePanel.SetActive(toggle);
        }

        private static void ToogleTimeScale(bool toggle)
        {
            Time.timeScale = toggle ? 0f : 1f;
        }

        #endregion

        #region Input Handler

        private void Update()
        {
            HandleInGameMenuPanel();
        }

        private void ToggleDialogueInput(bool toggle)
        {
            if (toggle)
            {
                PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(PlayerInputMode.Dialogue);
            }
            else
            {
                PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(PlayerInputMode.Main);
            }
        }
        private void HandleInGameMenuPanel()
        {
            if (_menuAction.WasReleasedThisFrame() && !_active)
            {
                _active = true;
                ToggleInGameMenuPanel(true);
                PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(PlayerInputMode.UI);
            }
            else if (_menuAction.WasReleasedThisFrame() && _active)
            {
                _active = false;
                ToggleInGameMenuPanel(false);
                PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(PlayerInputMode.Main);
            }
        }

        #endregion

    }
}