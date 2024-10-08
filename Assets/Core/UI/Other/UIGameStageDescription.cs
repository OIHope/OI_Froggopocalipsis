using TMPro;
using UnityEngine;

namespace Core.Progression
{
    public class UIGameStageDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameStageDescription;

        private void Start()
        {
            GameEventsBase.OnReachNewGameStage += DisplayGameStageDescription;

            GameStage currentGameStage = GameStageManager.Instance.CurrentGameStage;
            DisplayGameStageDescription(currentGameStage);
        }
        private void DisplayGameStageDescription(GameStage gameStage)
        {
            GameStage currentGameStage = GameStageManager.Instance.CurrentGameStage;
            _gameStageDescription.text = ("Game stage is: " + currentGameStage);
        }
    }
}