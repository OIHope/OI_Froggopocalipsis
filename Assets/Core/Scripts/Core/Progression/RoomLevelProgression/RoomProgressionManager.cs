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

        private bool _allConditionsMet = false;

        private void InitConditions()
        {
            StartCoroutine(StartRoomConditionExecution());
        }

        private IEnumerator StartRoomConditionExecution()
        {
            if (_conditions.Count <= 0)
            {
                OpenDoors();
                //Debug.Log("No conditions in this room!");
                yield break;
            }

            _doorsManager.CloseDoors();
            //Debug.Log("Doors are closed till dll conditions are complete");

            foreach (var condition in _conditions)
            {
                condition.PrepareCondition();
                //Debug.Log("Preparinmg comdition...");
                yield return new WaitUntil(() => condition.ConditionMet());
                //Debug.Log("Condition is met, proceedin...");
                yield return new WaitForSeconds(_delayBetweenWaves);
            }

            //Debug.Log("Opening the room!");
            _allConditionsMet = true;
            OpenDoors();
        }

        private void Update()
        {
            if (!_allConditionsMet)
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
            }
        }

        private void OnEnable()
        {
            TransitionManager.Instance.OnRoomSwitchEnd += InitConditions;
        }

        private void OnDisable()
        {
            TransitionManager.Instance.OnRoomSwitchEnd -= InitConditions;
            StopAllCoroutines();
        }
    }

}