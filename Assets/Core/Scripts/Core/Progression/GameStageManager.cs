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
        [SerializeField] private StageData _currentStageData;

        private void Awake()
        {
            GameEventsBase.OnReachNewGameStage += ChangeGameStage;
        }

        private void ChangeGameStage(GameStage stage)
        {
            _currentStageData.CurrentGameStage = stage;
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