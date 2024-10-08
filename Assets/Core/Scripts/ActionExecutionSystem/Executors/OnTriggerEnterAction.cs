using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class OnTriggerEnterAction : MonoBehaviour
    {
        [SerializeField] private List<ActionBase> _actions;
        [SerializeField] private bool _playerOnly = true;

        [SerializeField] private List<ConditionBase> _extraConditions;

        private void OnTriggerEnter(Collider other)
        {
            if (_extraConditions.Count > 0)
            {
                bool conditionsAraValid = true;
                foreach (ConditionBase c in _extraConditions)
                {
                    bool validation = c.ConditionIsValid();
                    conditionsAraValid = validation ? conditionsAraValid : false;
                }
                if (!conditionsAraValid) return;
            }
            if (_playerOnly && !other.gameObject.CompareTag("Player")) return;
            foreach (var action in _actions) action.Execute();
        }
    }
}