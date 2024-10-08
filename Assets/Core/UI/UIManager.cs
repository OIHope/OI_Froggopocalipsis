using Core.DialogueSystem;
using Core.Progression;
using Core.System;
using Level.Stage;
using PlayerSystem;
using System;
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

        [SerializeField] private GameObject _gamePausePanel;
        [SerializeField] private GameObject _dialoguePanel;

        [SerializeField] private GameObject _levelUpPanel;
        [SerializeField] private List<GameObject> _attackPowerSkillImageList;
        [SerializeField] private List<GameObject> _attackCooldownSkillImageList;
        [SerializeField] private List<GameObject> _critChanceSkillImageList;
        [SerializeField] private List<GameObject> _dodgeCooldownSkillImageList;
        [SerializeField] private List<GameObject> _moveSpeedSkillImageList;
        [SerializeField] private List<GameObject> _maxHPSkillImageList;

        [SerializeField] private GameObject _inGameUIPanel;
        [SerializeField] private TextMeshProUGUI _plLevelText;
        [SerializeField] private TextMeshProUGUI _plEXPText;
        [SerializeField] private TextMeshProUGUI _plSkillPointsText;

        private Action OnToggleMenuButtonPerformedAction;

        private InputAction _toggleMenuAction;
        private bool _actionIsAssigned = false;

        private bool _init = false;

        public override IEnumerator InitManager()
        {
            CloseAllPanels();
            yield return null;
        }
        public override IEnumerator SetupManager()
        {
            DialogueManager.Instance.OnDialoguePanelOpenRequested += OpenDialoguePanel;
            DialogueManager.Instance.OnDialoguePanelCloseRequested += OpenInGameUIPanel;
            PlayerProgressionManager.Instance.OnOpenLevelUpMenu += OpenLevelUpPanel;
            PlayerProgressionManager.Instance.OnCloseLevelUpMenu += OpenInGameUIPanel;

            TransitionManager.Instance.OnRoomSwitchStart += () => CloseAllPanels();
            TransitionManager.Instance.OnRoomSwitchEnd += OpenInGameUIPanel;

            GameEventsBase.OnResumeGameplay += OpenInGameUIPanel;

            OpenMainMenu();

            _init = true;
            yield return null;
        }

        public void OpenMainMenu()
        {
            CloseAllPanels();
            _MainMenuPanel.SetActive(true);

            SwitchInputMode(PlayerInputMode.UI);
            _toggleMenuAction = null;
            _actionIsAssigned = false;
        }
        public void OpenDialoguePanel()
        {
            CloseAllPanels();
            _dialoguePanel.SetActive(true);

            SwitchInputMode(PlayerInputMode.Dialogue);
            _toggleMenuAction = null;
            _actionIsAssigned = false;
        }
        public void OpenLevelUpPanel()
        {
            CloseAllPanels();
            _levelUpPanel.SetActive(true);
            _inGameUIPanel.SetActive(true);

            SwitchInputMode(PlayerInputMode.UI);
            OnToggleMenuButtonPerformedAction = null;
            OnToggleMenuButtonPerformedAction += OpenInGameUIPanel;
            OnToggleMenuButtonPerformedAction -= OpenPausePanel;
        }
        public void OpenPausePanel()
        {
            CloseAllPanels();
            _gamePausePanel.SetActive(true);

            SwitchInputMode(PlayerInputMode.UI);
            OnToggleMenuButtonPerformedAction = null;
            OnToggleMenuButtonPerformedAction += OpenInGameUIPanel;
            OnToggleMenuButtonPerformedAction -= OpenPausePanel;
        }
        public void OpenInGameUIPanel()
        {
            CloseAllPanels();
            _inGameUIPanel.SetActive(true);

            SwitchInputMode(PlayerInputMode.Main);
            OnToggleMenuButtonPerformedAction = null;
            OnToggleMenuButtonPerformedAction -= OpenInGameUIPanel;
            OnToggleMenuButtonPerformedAction += OpenPausePanel;
        }

        private void CloseAllPanels()
        {
            _MainMenuPanel.SetActive(false);
            _gamePausePanel.SetActive(false);
            _dialoguePanel.SetActive(false);
            _levelUpPanel.SetActive(false);
            _inGameUIPanel.SetActive(false);
        }

        private void SwitchInputMode(PlayerInputMode mode)
        {
            PlayerManager.Instance.OnRequestSwitchInputMode?.Invoke(mode);

            _toggleMenuAction = mode == PlayerInputMode.UI ? 
                InputManager.Instance.Input.UIInputMap.Menu :
                InputManager.Instance.Input.MainInputMap.Menu;

            if (_toggleMenuAction != null && !_actionIsAssigned)
            {
                _toggleMenuAction.performed += (_) => OnToggleMenuButtonPerformedAction?.Invoke();
                _actionIsAssigned = true;
            }

        }

        private void UpdateBasicPanel(PlayerProgressionData data)
        {
            _plLevelText.text = "LVL: " + data.CurrentLVL;
            _plEXPText.text = "EXP: " + data.CurrentEXP + " / " + data.GetRequiredEXPForNextLevel();
            _plSkillPointsText.text = "Skill points: " + data.LevelUpPoints;
        }
        private void UpdateLevelUpPanel(PlayerProgressionData data)
        {
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

            PlayerProgressionData data = PlayerProgressionManager.Instance.SkillData();
            UpdateBasicPanel(data);
            UpdateLevelUpPanel(data);
        }
    }
}