using Core.Progression;
using Level.Stage;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class PlayerDeathAction : ActionBase
    {
        private void OnEnable()
        {
            GameEventsBase.OnPlayerDeath += Execute;
        }
        private void OnDisable()
        {
            GameEventsBase.OnPlayerDeath -= Execute;
        }
        protected override void ActionToPerform()
        {
            SetGameStage();
            Invoke(nameof(TransitionToHub), 2f);
        }

        private void SetGameStage()
        {
            GameStage currentGameStage = GameStageManager.Instance.CurrentGameStage;

            if (currentGameStage >= GameStage.ForestEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.ForestDeath);
                return;
            }
            if (currentGameStage >= GameStage.SwampEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.SwampDeath);
                return;
            }
            if (currentGameStage >= GameStage.FieldEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.FieldDeath);
                return;
            }
        }
        private void TransitionToHub()
        {
            TransitionManager.Instance.OnTransitionEnter?.Invoke(TransitionDirection.Forward, EntranceDirection.Front, CurrentLevelStage.Hub);
        }
    }
}