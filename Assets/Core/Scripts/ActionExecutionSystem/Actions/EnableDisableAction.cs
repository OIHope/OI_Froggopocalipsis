using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class EnableDisableAction : ActionBase
    {
        [SerializeField] private List<GameObject> _targetList;
        [SerializeField] private bool _enableObject;
        [SerializeField] private bool _disableObject;

        protected override void ActionToPerform()
        {
            bool value = false;
            if (_enableObject) { value = true; }
            if (_disableObject) { value = false; }
            foreach (GameObject target in _targetList) target.SetActive(value);
        }
    }
}