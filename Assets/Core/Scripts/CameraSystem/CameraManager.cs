using UnityEngine;
using System;
using Core.System;
using System.Collections;

namespace Core.Camera
{
    public class CameraManager : Manager
    {
        public static CameraManager Instance { get; private set; }
        public Action<CameraData> OnNewRoomLoaded;

        [SerializeField] private CameraFollowTarget _cameraFolowComponent;

        private CameraData _cameraCurrentData;

        public override IEnumerator InitManager()
        {
            OnNewRoomLoaded += SetupNewCameraData;
            yield return null;
        }

        public override IEnumerator SetupManager()
        {
            _cameraFolowComponent.Init();
            yield return null;
        }

        private void Awake()
        {
            SingletonMethod();
        }
        private void SetupNewCameraData(CameraData cameraData)
        {
            _cameraCurrentData = cameraData;
            _cameraFolowComponent.SetupCameraLimits(_cameraCurrentData);
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
                //DontDestroyOnLoad(gameObject);
            }
        }
    }
}