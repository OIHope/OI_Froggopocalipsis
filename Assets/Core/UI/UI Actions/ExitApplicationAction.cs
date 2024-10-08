using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class ExitApplicationAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            Application.Quit();
        }
    }
}