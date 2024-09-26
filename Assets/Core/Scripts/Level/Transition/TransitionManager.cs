using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Stage
{
    public enum TransitionDirection { Forward, Backward}
    public enum EntranceDirection { Front, Back, Custom1, Custom2, Custom3 }
    public enum CurrentLevelStage { Empty, IntroScene, Hub, Field, Swamp, Forest, OutroScene}
    public class TransitionManager : MonoBehaviour
    {
        public static TransitionManager Instance { get; private set; }
        public Action<TransitionDirection, EntranceDirection, CurrentLevelStage> OnTransitionEnter;
        public Action OnRoomSwitchStart;
        public Action OnRoomSwitchEnd;

        [SerializeField] private LevelStageSO _introStageData;
        [SerializeField] private LevelStageSO _hubStageData;
        [SerializeField] private LevelStageSO _fieldStageData;
        [SerializeField] private LevelStageSO _swampStageData;
        [SerializeField] private LevelStageSO _forestStageData;
        [SerializeField] private LevelStageSO _outroStageData;
        [Space]
        [SerializeField] private Transform _parent;
        [SerializeField] private CurrentLevelStage _startStage;
        [SerializeField] private EntranceDirection _startEntrance;

        private CurrentLevelStage _stageKey;
        private List<GameObject> _currentStageBuffer;
        private List<TransitionData> _currentTransitionBuffer;
        private int _currentRoomIndex;
        private int _roomLimit => _currentStageBuffer.Count;

        private void Awake()
        {
            SingletonAwakeMethod();

            _currentStageBuffer = new List<GameObject>();
            _currentTransitionBuffer = new List<TransitionData>();
            _currentRoomIndex = -1;

            StartTransition(TransitionDirection.Forward, _startEntrance, _startStage);

            //SwitchRoom(0);

            OnTransitionEnter += StartTransition;
        }

        private void StartTransition(TransitionDirection changeDirection, EntranceDirection entrance, CurrentLevelStage requestedStage)
        {
            StartCoroutine(ChangeStage(changeDirection, entrance, requestedStage));
        }
        private IEnumerator ChangeStage(TransitionDirection changeDirection, EntranceDirection entrance, CurrentLevelStage requestedStage)
        {
            OnRoomSwitchStart?.Invoke();
            yield return new WaitForSeconds(1f);

            int stepDirection = changeDirection == TransitionDirection.Forward ? 1 : -1;
            int nextIndex;

            bool onTheSameStage = requestedStage == _stageKey;

            if (!onTheSameStage)
            {
                FillStageBuffer(requestedStage);
                nextIndex = stepDirection > 0 ? 0 : _roomLimit - 1;
            }
            else
            {
                nextIndex = _currentRoomIndex + stepDirection;
            }

            _currentRoomIndex = nextIndex;

            SwitchRoom(nextIndex);
            MovePlayerToEntrance(entrance);

            yield return new WaitForSeconds(0.5f);
            OnRoomSwitchEnd?.Invoke();
        }
        private void SwitchRoom(int switchIndex)
        {
            for (int i = 0; i < _currentStageBuffer.Count; i++)
            {
                bool enable = i == switchIndex;
                _currentStageBuffer[i].SetActive(enable);
            }

            _currentRoomIndex = switchIndex;
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

        private void MovePlayerToEntrance(EntranceDirection entrance)
        {
            TransitionData enteredRoomData = _currentTransitionBuffer[_currentRoomIndex];
            Vector3 entrancePos = entrance switch
            {
                EntranceDirection.Front => enteredRoomData.StartPointPos,
                EntranceDirection.Back => enteredRoomData.EndPointPos,
                EntranceDirection.Custom1 => enteredRoomData.CustomPointPos1,
                EntranceDirection.Custom2 => enteredRoomData.CustomPointPos2,
                EntranceDirection.Custom3 => enteredRoomData.CustomPointPos3,
                _ => enteredRoomData.StartPointPos
            };
            PlayerManager.Instance.OnPlayerChangeLevelStage?.Invoke(entrancePos);
        }

        private LevelStageSO GetCurrentStage(CurrentLevelStage stage)
        {
            switch (stage)
            {
                case CurrentLevelStage.IntroScene:
                    return _introStageData;
                case CurrentLevelStage.Hub:
                    return _hubStageData;
                case CurrentLevelStage.Field:
                    return _fieldStageData;
                case CurrentLevelStage.Swamp:
                    return _swampStageData;
                case CurrentLevelStage.Forest:
                    return _forestStageData;
                case CurrentLevelStage.OutroScene:
                    return _outroStageData;
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