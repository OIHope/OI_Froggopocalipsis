using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class UnstackPlayerAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            GameEventsBase.OnPlayerIsStuckAndNeedsHelp?.Invoke();
        }
    }
}