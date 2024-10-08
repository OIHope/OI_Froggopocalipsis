using PlayerSystem;
using TMPro;
using UnityEngine;

namespace Core.Language
{
    public class LabelHolder : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TextData _basicTextLine;
        [Space(15)]
        [SerializeField] private bool _incudesContolDisplayText = false;
        [Space]
        [SerializeField] private Color keyColor;
        [Space]
        [SerializeField] private TextData _keyboardKey;
        [SerializeField] private TextData _gamepadKey;
        [Space]
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
            string key = usingGamepad ? _keyboardKey.Line : _gamepadKey.Line;
            string description = usingGamepad ? _keyboardControlsTextLine.Line : _gamepadControlsTextLine.Line;

            string hexColor = ColorUtility.ToHtmlStringRGB(keyColor);

            string formattedKey = $"<b><color=#{hexColor}>{key}</color></b>";

            return $"\"{formattedKey}\" {description}";
        }
    }
}