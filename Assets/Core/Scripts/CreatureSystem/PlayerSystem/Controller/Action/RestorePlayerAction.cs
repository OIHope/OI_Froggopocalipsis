using Core;
using Entity;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class RestorePlayerAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            PlayerManager.Instance.OnPlayerRestoreRequest?.Invoke();
        }
    }
}