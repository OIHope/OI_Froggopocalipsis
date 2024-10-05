using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class DestroyObjectAction : ActionBase
    {
        [SerializeField] private GameObject obj;
        protected override void ActionToPerform()
        {
            Destroy(obj);
        }
    }
}