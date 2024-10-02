using Core.DialogueSystem;
using Core.Progression;
using PlayerSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
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

        private void Start()
        {
            CloseAllPanels();

            DialogueManager.Instance.OnDialoguePanelOpenRequested += () => TogglePanel(_dialoguePanel, true);
            DialogueManager.Instance.OnDialoguePanelCloseRequested += () => TogglePanel(_dialoguePanel, false);
            PlayerProgressionManager.Instance.OnOpenLevelUpMenu += () => TogglePanel(_levelUpPanel, true);
            PlayerProgressionManager.Instance.OnCloseLevelUpMenu += () => TogglePanel(_levelUpPanel, false);

            ToggleInputMode(PlayerInputMode.Main);
            _baseInfoPanel.SetActive(true);
        }

        private void TogglePanel(GameObject panel, bool toggle)
        {
            CloseAllPanels();
            panel.SetActive(toggle);

            if (panel == _dialoguePanel)
            {
                ToggleInputMode(toggle ? PlayerInputMode.Dialogue : PlayerInputMode.Main);
            }
            else if (panel == _inGameMenuPanel || panel == _levelUpPanel)
            {
                ToggleInputMode(toggle ? PlayerInputMode.UI : PlayerInputMode.Main);
            }
            else
            {
                ToggleInputMode(PlayerInputMode.Main);
            }

            _baseInfoPanel.SetActive(!toggle);
        }

        private void CloseAllPanels()
        {
            _inGameMenuPanel.SetActive(false);
            _dialoguePanel.SetActive(false);
            _levelUpPanel.SetActive(false);
            _baseInfoPanel.SetActive(false);
        }

        private void ToggleInputMode(PlayerInputMode mode)
        {
            Debug.Log("UI reguested " + mode.ToString() + " Input Switch!");
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

            for (int i = 0; i < _attackPowerSkillImageList.Count; i++)
            {
                bool activate = i > data.AttackPowerPoints;
                _attackPowerSkillImageList[i].SetActive(!activate);
            }
            for (int i = 0; i < _attackCooldownSkillImageList.Count; i++)
            {
                bool activate = i > data.AttackCooldownPoints;
                _attackCooldownSkillImageList[i].SetActive(!activate);
            }
            for (int i = 0; i < _critChanceSkillImageList.Count; i++)
            {
                bool activate = i > data.CritChancePoints;
                _critChanceSkillImageList[i].SetActive(!activate);
            }
            for (int i = 0; i < _dodgeCooldownSkillImageList.Count; i++)
            {
                bool activate = i > data.DodgeCooldownPoints;
                _dodgeCooldownSkillImageList[i].SetActive(!activate);
            }
            for (int i = 0; i < _moveSpeedSkillImageList.Count; i++)
            {
                bool activate = i > data.MoveSpeedPoints;
                _moveSpeedSkillImageList[i].SetActive(!activate);
            }
            for (int i = 0; i < _maxHPSkillImageList.Count; i++)
            {
                bool activate = i > data.MaxHPPoints;
                _maxHPSkillImageList[i].SetActive(!activate);
            }
        }

        private void Update()
        {
            if (_menuAction.WasReleasedThisFrame())
            {
                _menuActive = !_menuActive;
                TogglePanel(_inGameMenuPanel, _menuActive);
            }

            PlayerProgressionData data = PlayerProgressionManager.Instance.SkillData();
            UpdateBasicPanel(data);
            UpdateLevelUpPanel(data);
        }
    }
}