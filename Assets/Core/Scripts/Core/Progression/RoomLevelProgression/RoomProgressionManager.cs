using Level;
using Level.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Progression
{
    public class RoomProgressionManager : MonoBehaviour
    {
        [SerializeField] private List<RoomCondition> _conditions;
        [SerializeField] private float _delayBetweenWaves = 1f;
        [SerializeField] private DoorsManager _doorsManager;

        private bool _conditionsStarted = false;
        private bool _allConditionsMet = false;

        private void Start()
        {
            InitConditions();
        }

        private void InitConditions()
        {
            StartCoroutine(StartRoomConditionExecution());
        }

        private IEnumerator StartRoomConditionExecution()
        {
            if (_conditions.Count <= 0)
            {
                OpenDoors();
                yield break;
            }

            _conditionsStarted = true;
            _doorsManager.CloseDoors();

            foreach (var condition in _conditions)
            {
                condition.PrepareCondition();
                yield return new WaitUntil(() => condition.ConditionMet());
                yield return new WaitForSeconds(_delayBetweenWaves);
            }

            _conditionsStarted = false;
            _allConditionsMet = true;
            OpenDoors();
        }

        private void Update()
        {
            if (_conditionsStarted && !_allConditionsMet)
            {
                CheckAllConditions();
            }
        }

        private void CheckAllConditions()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.ConditionMet()) return;
            }

            _allConditionsMet = true;
            OpenDoors();
        }

        private void OpenDoors()
        {
            if (_doorsManager != null)
            {
                _doorsManager.OpenDoors();
                Debug.Log("Doors are opened!");
            }
        }

        private void OnEnable()
        {
            TransitionManager.Instance.OnRoomSwitchEnd += InitConditions;
        }

        private void OnDisable()
        {
            TransitionManager.Instance.OnRoomSwitchEnd -= InitConditions;
        }
    }

}