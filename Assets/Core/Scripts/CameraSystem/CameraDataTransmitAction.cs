using ActionExecuteSystem;
using UnityEngine;

namespace Core.Camera
{
    public class CameraDataTransmitAction : ActionBase
    {
        [SerializeField] private CameraData _cameraDataToTransmit;
        protected override void ActionToPerform()
        {
            CameraManager.Instance.OnNewRoomLoaded?.Invoke(_cameraDataToTransmit);
        }
    }
}