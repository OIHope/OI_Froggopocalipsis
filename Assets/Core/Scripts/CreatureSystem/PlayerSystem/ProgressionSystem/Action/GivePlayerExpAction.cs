using Core.Progression;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class GivePlayerExpAction : ActionBase
    {
        [SerializeField] private int _expValue;
        protected override void ActionToPerform()
        {
            PlayerProgressionManager.Instance.OnPlayerEarnSomeEXP?.Invoke(_expValue);
        }
    }
}