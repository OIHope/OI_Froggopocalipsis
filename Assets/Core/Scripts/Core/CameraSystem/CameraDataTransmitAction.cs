using ActionExecuteSystem;
using UnityEngine;

namespace Core.Camera
{
    public class CameraDataTransmitAction : ActionBase
    {
        [SerializeField] private CameraData _cameraDataToTransmit;
        protected override void ActionToPerform()
        {
            Debug.Log("Loaded " + _cameraDataToTransmit.name + " data");
            CameraManager.Instance.OnNewRoomLoaded?.Invoke(_cameraDataToTransmit);
        }
    }
}