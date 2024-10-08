using ActionExecuteSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Collider))]
    public class GrassInteractOnMovementComponent : MonoBehaviour
    {
        [SerializeField] private List<ActionBase> _actions;

        private void ExecuteActions()
        {
            foreach (var action in _actions) action.Execute();
        }
        private void OnTriggerStay(Collider other)
        {
            IMoveInteractor interactor = other.GetComponent<IMoveInteractor>();
            if (interactor != null && interactor.InstanceInMove)
            {
                ExecuteActions();
            }
        }
    }
}