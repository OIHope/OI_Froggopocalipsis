using UnityEngine;

namespace ActionExecuteSystem
{
    public abstract class ActionBase : MonoBehaviour
    {
        [SerializeField] private bool _executeOnce = false;
        [Space]
        [SerializeField] private bool _executeOnEnable = false;
        [SerializeField] private bool _executeOnDisable = false;
        [Space]
        [SerializeField] private bool _executeOnAwake = false;
        [SerializeField] private bool _executeOnStart = false;
        [Space]
        [SerializeField] private bool _executeOnDestroy = false;

        private bool _executed = false;

        private void OnEnable()
        {
            if (_executeOnEnable) Execute();
        }
        private void OnDisable()
        {
            if (_executeOnDisable) Execute();
        }
        private void Awake()
        {
            if (_executeOnAwake) Execute();
        }
        private void Start()
        {
            if (_executeOnStart) Execute();
        }
        private void OnDestroy()
        {
            if (_executeOnDestroy) Execute();
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