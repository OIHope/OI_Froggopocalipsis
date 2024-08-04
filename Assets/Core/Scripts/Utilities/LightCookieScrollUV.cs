using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utilities
{
    public class LightCookieScrollUV : MonoBehaviour
    {
        [SerializeField] private UniversalAdditionalLightData lightData;
        [SerializeField] private Vector2 speed;

        private void FixedUpdate()
        {
            lightData.lightCookieOffset = speed * Time.time;
        }
    }
}