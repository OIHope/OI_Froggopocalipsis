using Core.Camera;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class SwitchCameraPerspectiveAction : ActionBase
    {
        [Header("Main Camera Data must be exactly what level cameraData is!")]
        [SerializeField] private CameraData _mainCameraData;
        [SerializeField] private CameraData _secondaryCameraData;

        [SerializeField] private bool _isTriggerZone = false;

        private bool _isOnMain = true;
        protected override void ActionToPerform()
        {
            if (_isOnMain)
            {
                CameraManager.Instance.OnNewRoomLoaded(_secondaryCameraData);
                _isOnMain = false;
            }
            else
            {
                CameraManager.Instance.OnNewRoomLoaded(_mainCameraData);
                _isOnMain = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isTriggerZone) return;
            if (!other.CompareTag("Player")) return;

            Execute();
        }
        private void OnTriggerExit(Collider other)
        {
            if (!_isTriggerZone) return;
            if (!other.CompareTag("Player")) return;

            Execute();
        }
    }
}