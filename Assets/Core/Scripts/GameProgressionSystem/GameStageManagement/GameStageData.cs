using UnityEngine;

namespace Core.Progression
{
    [CreateAssetMenu(menuName = "Core/Game Stage")]
    public class GameStageData : ScriptableObject
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
        public void ResetGameStage() => _currentGameStage = GameStage.Intro;
    }
}