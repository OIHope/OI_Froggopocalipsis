using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ProgressBarComponent : MonoBehaviour
    {
        [SerializeField] protected Slider progressBar;
        [Space]
        [SerializeField] protected bool isUIElement = true;
        [SerializeField] protected bool billboardCamera = true;
        [Space]
        [SerializeField] protected Transform targetTransform;
        [SerializeField] protected Vector3 positionOffset = Vector3.zero;
        [Space]
        [SerializeField] private Image _fill;
        [SerializeField] private Image _background;
        [SerializeField] private TwoColorsDataSO _colorPalette;

        public void UpdateProgressBar(float currentValue, float maxValue)
        {
            progressBar.maxValue = maxValue;
            progressBar.minValue = 0f;
            progressBar.value = currentValue;

            _fill.color = _colorPalette.MainColor;
            _background.color = _colorPalette.SubColor;

            bool shouldShow = currentValue > 0 && currentValue < maxValue;
            progressBar.gameObject.SetActive(shouldShow);
        }
        protected void Update()
        {
            if (isUIElement) return;
            if (billboardCamera) transform.rotation = Camera.main.transform.rotation;
            transform.position = targetTransform.position + positionOffset;
        }
    }
}