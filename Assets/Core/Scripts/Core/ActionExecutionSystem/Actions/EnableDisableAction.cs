using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class EnableDisableAction : ActionBase
    {
        [SerializeField] private List<GameObject> _targetList;

        private bool _enabled;

        public void EnebleAction()
        {
            _enabled = true;
            ActionToPerform();
        }
        public void DisableAction()
        {
            _enabled = false;
            ActionToPerform();
        }
        protected override void ActionToPerform()
        {
            foreach (GameObject target in _targetList) target.SetActive(_enabled);
        }
    }
}