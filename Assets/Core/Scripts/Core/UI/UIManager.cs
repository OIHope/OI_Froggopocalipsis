using Core.DialogueSystem;
using Core.Progression;
using Core.System;
using Level.Stage;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UI
{
    public class UIManager : Manager
    {
        [SerializeField] private GameObject _MainMenuPanel;
        [SerializeField] private GameObject _GameCompletePanel;

        [SerializeField] private GameObject _inGameMenuPanel;
        [SerializeField] private GameObject _dialoguePanel;

        [SerializeField] private GameObject _levelUpPanel;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _skillPointsText;
        [SerializeField] private List<GameObject> _attackPowerSkillImageList;
        [SerializeField] private List<GameObject> _attackCooldownSkillImageList;
        [SerializeField] private List<GameObject> _critChanceSkillImageList;
        [SerializeField] private List<GameObject> _dodgeCooldownSkillImageList;
        [SerializeField] private List<GameObject> _moveSpeedSkillImageList;
        [SerializeField] private List<GameObject> _maxHPSkillImageList;

        [SerializeField] private GameObject _baseInfoPanel;
        [SerializeField] private TextMeshProUGUI _plLevelText;
        [SerializeField] private TextMeshProUGUI _plEXPText;
        [SerializeField] private TextMeshProUGUI _plSkillPointsText;

        private InputAction _menuAction;
        private bool _menuActive = false;

        private bool _init = false;

        public void OpenMainMenu() => TogglePanel(_MainMenuPanel, true, false);

        public override IEnumerator InitManager()
        {
            CloseAllPanels();
            yield return null;
        }
        public override IEnumerator SetupManager()
        {
            DialogueManager.Instance.OnDialoguePanelOpenRequested += () => TogglePanel(_dialoguePanel, true, false);
            DialogueManager.Instance.OnDialoguePanelCloseRequested += () => TogglePanel(_dialoguePanel, false, true);
            PlayerProgressionManager.Instance.OnOpenLevelUpMenu += () => TogglePanel(_levelUpPanel, true, false);
            PlayerProgressionManager.Instance.OnCloseLevelUpMenu += () => TogglePanel(_levelUpPanel, false, true);

            TransitionManager.Instance.OnRoomSwitchStart += () => CloseAllPanels();
            TransitionManager.Instance.OnRoomSwitchEnd += () => _baseInfoPanel.SetActive(true);

            GameEventsBase.OnResumeGameplay += () => TogglePanel(_inGameMenuPanel, false, true);

            OpenMainMenu();

            _init = true;
            yield return null;
        }

        public void TogglePanel(GameObject panel, bool toggle, bool addPanelToggle)
        {
            CloseAllPanels();
            panel.SetActive(toggle);

            if (panel == _dialoguePanel)
            {
                ToggleInputMode(toggle ? PlayerInputMode.Dialogue : PlayerInputMode.Main);
            }
            else if (panel == _inGameMenuPanel 
                || panel == _levelUpPanel 
                || panel == _MainMenuPanel
                || panel == _GameCompletePanel)
            {
                ToggleInputMode(toggle ? PlayerInputMode.UI : PlayerInputMode.Main);
            }
            else
            {
                ToggleInputMode(PlayerInputMode.Main);
            }

            _baseInfoPanel.SetActive(addPanelToggle);
        }

        private void CloseAllPanels()
        {
            _MainMenuPanel.SetActive(false);
            _GameCompletePanel.SetActive(false);
            _inGameMenuPanel.SetActive(false);
            _dialoguePanel.SetActive(false);
            _levelUpPanel.SetActive(false);
            _baseInfoPanel.SetActive(false);
        }

        private void ToggleInputMode(PlayerInputMode mode)
        {
            PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(mode);
            _menuAction = mode == PlayerInputMode.UI ? InputManager.Instance.Input.UIInputMap.Menu :
                    InputManager.Instance.Input.MainInputMap.Menu;
        }

        private void UpdateBasicPanel(PlayerProgressionData data)
        {
            _plLevelText.text = "LVL: " + data.CurrentLVL;
            _plEXPText.text = "EXP: " + data.CurrentEXP + " / " + data.GetRequiredEXPForNextLevel();
            _plSkillPointsText.text = "Skill points: " + data.LevelUpPoints;
        }
        private void UpdateLevelUpPanel(PlayerProgressionData data)
        {
            _levelText.text = "LVL: " + data.CurrentLVL;
            _skillPointsText.text = "Skill points: " + data.LevelUpPoints;

            UpdateSkillImages(_attackPowerSkillImageList, data.AttackPowerPoints);
            UpdateSkillImages(_attackCooldownSkillImageList, data.AttackCooldownPoints);
            UpdateSkillImages(_critChanceSkillImageList, data.CritChancePoints);
            UpdateSkillImages(_dodgeCooldownSkillImageList, data.DodgeCooldownPoints);
            UpdateSkillImages(_moveSpeedSkillImageList, data.MoveSpeedPoints);
            UpdateSkillImages(_maxHPSkillImageList, data.MaxHPPoints);
        }

        private void UpdateSkillImages(List<GameObject> skillImageList, int skillPoints)
        {
            for (int i = 0; i < skillImageList.Count; i++)
            {
                skillImageList[i].SetActive(i < skillPoints);
            }
        }

        private void Update()
        {
            if (!_init) return;
            if (_menuAction.WasReleasedThisFrame())
            {
                _menuActive = !_menuActive;
                TogglePanel(_inGameMenuPanel, _menuActive, !_menuActive);
            }

            PlayerProgressionData data = PlayerProgressionManager.Instance.SkillData();
            UpdateBasicPanel(data);
            UpdateLevelUpPanel(data);
        }
    }
}