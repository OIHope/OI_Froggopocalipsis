using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class BackToGameplayAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            GameEventsBase.OnResumeGameplay?.Invoke();
        }
    }
}