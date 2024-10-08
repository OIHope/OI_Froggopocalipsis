using ActionExecuteSystem;
using UnityEngine;

namespace Core.Progression
{
    public class UpGameProgression : ActionBase
    {
        [SerializeField] private GameStage _gameStage;
        protected override void ActionToPerform()
        {
            GameEventsBase.OnReachNewGameStage?.Invoke(_gameStage);
        }
    }
}