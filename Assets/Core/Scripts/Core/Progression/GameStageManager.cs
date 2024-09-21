using Core.DialogueSystem;
using UnityEngine;

namespace Core.Progression
{
    public enum GameStage
    {
        Intro, Arrival, 
        FieldEnter, FieldDeath, FieldComplete, 
        SwampEnter, SwampDeath, SwampComplete,
        ForestEnter, ForestDeath, ForestComplete,
        Outro
    }
    public class GameStageManager : MonoBehaviour
    {
        public static GameStageManager Instance { get; private set; }
        public GameStage CurrentGameStage => _currentStageData.CurrentGameStage;

        [SerializeField] private StageData _currentStageData;

        private void Awake()
        {
            SingletonAwakeMethod();
        }
        private void Start()
        {
            GameEventsBase.OnReachNewGameStage += ChangeGameStage;
            DialogueManager.Instance.OnDialogueFinish += ChangeGameStage;
        }

        private void ChangeGameStage(GameStage stage)
        {
            _currentStageData.CurrentGameStage = stage;
            Debug.Log("Game stage is: " + _currentStageData.CurrentGameStage);
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

    [CreateAssetMenu(menuName =("Core/Game Stage"))]
    public class StageData : ScriptableObject
    {
        [SerializeField] private GameStage _currentGameStage;

        public GameStage CurrentGameStage
        {
            get { return _currentGameStage; }
            set
            {
                if (value <= _currentGameStage) return;
                _currentGameStage = value;
                
            }
        }
    }
}