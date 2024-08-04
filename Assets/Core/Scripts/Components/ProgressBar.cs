using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] protected Slider progressBar;
        [SerializeField] protected bool isUIElement = true;
        [SerializeField] protected bool billboardCamera = true;
        [SerializeField] protected Transform targetTransform;
        [SerializeField] protected Vector3 positionOffset = Vector3.zero;

        public void UpdateProgressBar(float currentValue, float maxValue)
        {
            progressBar.maxValue = maxValue;
            progressBar.minValue = 0f;
            progressBar.value = currentValue;
        }
        protected void Update()
        {
            if (isUIElement) return;
            if (billboardCamera) transform.rotation = Camera.main.transform.rotation;
            transform.position = targetTransform.position + positionOffset;
        }
    }
}