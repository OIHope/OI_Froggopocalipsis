using System.Collections.Generic;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class OnTriggerEnterAction : MonoBehaviour
    {
        [SerializeField] private List<ActionBase> _actions;

        private void OnTriggerEnter(Collider other)
        {
            foreach (var action in _actions) action.Execute();
        }
    }
}