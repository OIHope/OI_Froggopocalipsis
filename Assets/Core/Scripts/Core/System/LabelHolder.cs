using PlayerSystem;
using TMPro;
using UnityEngine;

namespace Core.Language
{
    public class LabelHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TextData _basicTextLine;

        [SerializeField] private bool _incudesContolDisplayText = false;
        [SerializeField] private TextData _keyboardControlsTextLine;
        [SerializeField] private TextData _gamepadControlsTextLine;


        private void OnEnable()
        {
            GameEventsBase.OnLanguageChanged += UpdateLabel;
            InputManager.Instance.OnInputChanged += UpdateLabel;
            UpdateLabel();
        }

        private void OnDisable()
        {
            GameEventsBase.OnLanguageChanged -= UpdateLabel;
            InputManager.Instance.OnInputChanged -= UpdateLabel;
        }

        private void UpdateLabel()
        {
            _label.text = _incudesContolDisplayText ? ControlsText() : _basicTextLine.Line;
        }
        private string ControlsText()
        {
            bool usingGamepad = !InputManager.Instance.IsUsingGamepad;
            return usingGamepad ? _keyboardControlsTextLine.Line : _gamepadControlsTextLine.Line;
        }
    }
}