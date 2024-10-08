using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class ExecuteActionsInSequence : ActionBase
    {
        [SerializeField] private List<ActionBase> _actions;
        [SerializeField] private float _delay = 0.1f;
        protected override void ActionToPerform()
        {
            StartCoroutine(ExecuteAllExtionsWithDelay());
        }

        private IEnumerator ExecuteAllExtionsWithDelay()
        {
            foreach (var action in _actions)
            {
                action.Execute();
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}