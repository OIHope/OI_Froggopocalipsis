using UnityEngine;

namespace ActionExecuteSystem
{
    public abstract class ActionBase : MonoBehaviour
    {
        [SerializeField] private bool _executeOnce = false;
        [SerializeField] private bool _executeOnAwake = false;

        private bool _executed = false;

        private void Awake()
        {
            if (_executeOnAwake)
            {
                Execute();
                _executed = true;
            }
        }
        public void Execute()
        {
            if(_executed && _executeOnce) return;
            _executed = true;

            ActionToPerform();
        }

        protected abstract void ActionToPerform();
    }
}