using ActionExecuteSystem;
using Level.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progression
{
    public class RoomProgressionManager : MonoBehaviour
    {
        [SerializeField] private List<RoomCondition> _roomConditions;
        [SerializeField] private List<ConditionBase> _gameConditions;
        [SerializeField] private float _delayBetweenWaves = 1f;
        [Space]
        [SerializeField] private List<ActionBase> _actionOnOpen;
        [SerializeField] private List<ActionBase> _actionOnClose;

        private bool _inited = false;
        private bool _allConditionsMet = false;
        private Coroutine _roomConditionCoroutine;

        private void InitConditions()
        {
            if (_roomConditionCoroutine != null)
            {
                StopCoroutine(_roomConditionCoroutine);
            }

            ExecuteActionsOnClose();

            if (_roomConditions.Count > 0)
            {
                _roomConditionCoroutine = StartCoroutine(StartRoomConditionExecution());
            }
            _inited = true;
        }

        private IEnumerator StartRoomConditionExecution()
        {
            foreach (var condition in _roomConditions)
            {
                condition.PrepareCondition();
                yield return new WaitUntil(() => condition.ConditionMet());
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
        }

        private void FixedUpdate()
        {
            if (!_inited) return;
            CheckAllConditions();
        }

        private void CheckAllConditions()
        {
            bool allConditionsValid = true;

            foreach (var condition in _roomConditions)
            {
                if (!condition.ConditionMet())
                {
                    allConditionsValid = false;
                    break;
                }
            }

            if (allConditionsValid)
            {
                foreach (var condition in _gameConditions)
                {
                    if (!condition.ConditionIsValid())
                    {
                        allConditionsValid = false;
                        break;
                    }
                }
            }

            if (allConditionsValid && !_allConditionsMet)
            {
                _allConditionsMet = true;
                Debug.Log("All conditions are met");
                ExecuteActionsOnOpen();
            }
            else if (!allConditionsValid && _allConditionsMet)
            {
                _allConditionsMet = false;
                Debug.Log("No conditions are met");
                ExecuteActionsOnClose();
            }
        }

        private void ExecuteActionsOnClose()
        {
            if (_actionOnClose.Count <= 0) return;
            Debug.Log("Opening doors");
            foreach (var action in _actionOnClose)
            {
                action.Execute();
            }
        }

        private void ExecuteActionsOnOpen()
        {
            if (_actionOnOpen.Count <= 0) return;
            Debug.Log("Closing doors");
            foreach (var action in _actionOnOpen)
            {
                action.Execute();
            }
        }

        private void OnEnable()
        {
            _inited = false;
            _allConditionsMet = false;
            TransitionManager.Instance.OnRoomSwitchEnd += InitConditions;
        }

        private void OnDisable()
        {
            TransitionManager.Instance.OnRoomSwitchEnd -= InitConditions;
            StopAllCoroutines();
        }
    }



}