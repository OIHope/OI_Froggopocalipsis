using Core.Progression;
using Level.Stage;

namespace ActionExecuteSystem
{
    public class PlayerDeathAction : ActionBase
    {
        private void Awake()
        {
            GameEventsBase.OnPlayerDeath += Execute;
        }
        protected override void ActionToPerform()
        {
            SetGameStage();
            Invoke("TransitionToHub", 2f);
        }

        private void SetGameStage()
        {
            GameStage currentGameStage = GameStageManager.Instance.CurrentGameStage;

            if (currentGameStage >= GameStage.FieldEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.FieldDeath);
            }
            if (currentGameStage >= GameStage.SwampEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.SwampDeath);
            }
            if (currentGameStage >= GameStage.ForestEnter)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(GameStage.ForestDeath);
            }
            if (currentGameStage >= GameStage.ForestComplete)
            {
                GameEventsBase.OnReachNewGameStage?.Invoke(currentGameStage);
            }
        }
        private void TransitionToHub()
        {
            TransitionManager.Instance.OnTransitionEnter?.Invoke(TransitionDirection.Forward, EntranceDirection.Front, CurrentLevelStage.Hub);
        }
    }
}