using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class ActionExecutor : MonoBehaviour
    {
        [SerializeField] private List<ConditionBase> _conditions;
        [SerializeField] private List<ActionBase> _actions;

        [SerializeField] private bool _executeOnce = false;
        [SerializeField] private bool _executeOnEnable = false;
        [SerializeField] private bool _executeOnAwake = false;
        [SerializeField] private bool _executeOnStart = false;
        [SerializeField] private bool _checkEveryFrame = false;
        [SerializeField] private bool _onTriggerEnter = false;

        private bool _executed = false;

        private void OnEnable()
        {
            if (_executeOnEnable) TryExecuteActions();
        }

        private void Awake()
        {
            if (_executeOnAwake) TryExecuteActions();
        }

        private void Start()
        {
            if (_executeOnStart) TryExecuteActions();
        }
        private void FixedUpdate()
        {
            if (_checkEveryFrame) TryExecuteActions();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_onTriggerEnter) TryExecuteActions();
        }

        public void TryExecuteActions()
        {
            if (_executed && _executeOnce) return;

            if (AllConditionsMet())
            {
                foreach (var action in _actions)
                {
                    action.Execute();
                }
                _executed = true;
            }
        }

        private bool AllConditionsMet()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.ConditionIsValid())
                    return false;
            }
            return true;
        }
    }
}