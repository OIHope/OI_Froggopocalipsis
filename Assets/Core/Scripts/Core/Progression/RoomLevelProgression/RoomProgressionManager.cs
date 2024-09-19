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

        private void InitConditions()
        {
            StartCoroutine(StartRoomConditionExecution());
        }
        private IEnumerator StartRoomConditionExecution()
        {
            _doorsManager.CloseDoors();
            int conditionsCount = _conditions.Count;
            foreach (var condition in _conditions)
            {
                condition.PrepareCondition();
                yield return new WaitUntil(() => condition.ConditionMet());
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
            _doorsManager.OpenDoors();
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