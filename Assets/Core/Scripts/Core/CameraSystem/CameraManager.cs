using UnityEngine;

namespace Core.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        public System.Action<CameraData> OnNewRoomLoaded;

        [SerializeField] private CameraFollowTarget _cameraFolowComponent;

        private CameraData _cameraCurrentData;

        private void SetupNewCameraData(CameraData cameraData)
        {
            _cameraCurrentData = cameraData;
            _cameraFolowComponent.SetupCameraLimits(_cameraCurrentData);
        }

        private void Awake()
        {
            SingletonMethod();
            OnNewRoomLoaded += SetupNewCameraData;
        }
        private void SingletonMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}