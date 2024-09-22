using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utilities
{
    public class LightCookieScrollUV : MonoBehaviour
    {
        [SerializeField] private Material _lightMaterial;
        [SerializeField] private Vector2 speed;

        private void FixedUpdate()
        {
            Vector2 offset = _lightMaterial.GetVector("_CloudTex_ST");
            offset = offset * speed * Time.time;

            //lightData.lightCookieOffset = speed * Time.time;
        }
    }
}