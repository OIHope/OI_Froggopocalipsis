using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Stage
{
    public enum TransitionDirection { Forward, Backward }
    public enum CurrentLevelStage { Empty, Hub, Field, Swamp, Forest}
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager Instance { get; private set; }
        public Action<TransitionDirection, CurrentLevelStage> OnTransitionEnter;
        public Action OnRoomSwitchStart;
        public Action OnRoomSwitchEnd;

        [SerializeField] private LevelStageSO _hubStageData;
        [SerializeField] private LevelStageSO _fieldStageData;
        [SerializeField] private LevelStageSO _swampStageData;
        [SerializeField] private LevelStageSO _forestStageData;
        [Space]
        [SerializeField] private Transform _parent;
        [SerializeField] private CurrentLevelStage _startStage;

        private CurrentLevelStage _stageKey;
        private List<GameObject> _currentStageBuffer;
        private List<TransitionData> _currentTransitionBuffer;
        private int _currentRoomIndex;

        private void Awake()
        {
            SingletonAwakeMethod();

            _currentStageBuffer = new List<GameObject>();
            _currentTransitionBuffer = new List<TransitionData>();
            _currentRoomIndex = -1;

            FillStageBuffer(_startStage);

            OnTransitionEnter += StartTransition;
        }

        private void StartTransition(TransitionDirection changeDirection, CurrentLevelStage requestedStage)
        {
            StartCoroutine(ChangeStage(changeDirection, requestedStage));
        }
        private IEnumerator ChangeStage(TransitionDirection changeDirection, CurrentLevelStage requestedStage)
        {
            OnRoomSwitchStart?.Invoke();

            yield return new WaitForSeconds(1f);

            int stepDirection = changeDirection == TransitionDirection.Forward ? 1 : -1;
            int roomLimit = _currentStageBuffer.Count;
            int nextIndex = _currentRoomIndex + stepDirection;


            if (nextIndex >= roomLimit || nextIndex < 0)
            {
                _currentRoomIndex = 0;
                FillStageBuffer(requestedStage);
            }
            else
            {
                _currentRoomIndex = nextIndex;
                SwitchRoom(nextIndex);
            }

            MovePlayerToEntrance(changeDirection);

            yield return new WaitForSeconds(0.5f);

            OnRoomSwitchEnd?.Invoke();
        }
        private void SwitchRoom(int stageIndex)
        {
            for (int i = 0; i < _currentStageBuffer.Count; i++)
            {
                bool enable = i == stageIndex;
                _currentStageBuffer[i].SetActive(enable);
            }

            _currentRoomIndex = stageIndex;
        }

        private void FillStageBuffer(CurrentLevelStage bufferKey)
        {
            if (bufferKey == _stageKey) return;

            EmptyBuffer();
            List<GameObject> roomBuffer = GetCurrentStage(bufferKey).LevelStage;

            for (int i = 0; i < roomBuffer.Count; i++)
            {
                GameObject room = roomBuffer[i];
                GameObject roomInstance = Instantiate(room, Vector3.zero, Quaternion.identity, _parent);
                TransitionData roomTransitionData = roomInstance.GetComponent<TransitionComponent>().TransData;

                _currentStageBuffer.Add(roomInstance);
                _currentTransitionBuffer.Add(roomTransitionData);

                roomInstance.SetActive(false);
            }

            SwitchRoom(0);
            _stageKey = bufferKey;
        }
        private void EmptyBuffer()
        {
            foreach (var item in _currentStageBuffer)
            {
                Destroy(item.gameObject);
            }
            _currentStageBuffer.Clear();
            _currentTransitionBuffer.Clear();
        }

        private void MovePlayerToEntrance(TransitionDirection entranceDirection)
        {
            TransitionData enteredRoomData = _currentTransitionBuffer[_currentRoomIndex];
            Vector3 entrancePos = entranceDirection == TransitionDirection.Forward ? enteredRoomData.StartPointPos : enteredRoomData.EndPointPos;

            PlayerManager.Instance.OnPlayerChangeLevelStage(entrancePos);
        }

        private LevelStageSO GetCurrentStage(CurrentLevelStage stage)
        {
            switch (stage)
            {
                case CurrentLevelStage.Hub:
                    return _hubStageData;
                case CurrentLevelStage.Field:
                    return _fieldStageData;
                case CurrentLevelStage.Swamp:
                    return _swampStageData;
                case CurrentLevelStage.Forest:
                    return _forestStageData;
                default:
                    return _hubStageData;
            }
        }
        private void SingletonAwakeMethod()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}