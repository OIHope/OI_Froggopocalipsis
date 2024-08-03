using UnityEngine;

namespace Utilities
{
    public class SpriteBillboardComponent : MonoBehaviour
    {
        [SerializeField] private bool freezeXZaxis = false;
        void Update()
        {
            if (freezeXZaxis)
            {
                transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
            }
            else
            {
                transform.rotation = Camera.main.transform.rotation;
            }
        }
    }
}