using UnityEngine;
using UnityEngine.UI;

namespace ActionExecuteSystem
{
    public class FirstSelectUIButtonAction : ActionBase
    {
        [SerializeField] private Button _selectThisButtonOnEnable;
        protected override void ActionToPerform()
        {
            _selectThisButtonOnEnable.Select();
        }
    }
}