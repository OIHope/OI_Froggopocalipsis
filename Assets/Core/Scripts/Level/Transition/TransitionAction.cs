using Level.Stage;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class TransitionAction : ActionBase
    {
        [SerializeField] private TransitionDirection direction;
        [SerializeField] private CurrentLevelStage stage;
        protected override void ActionToPerform()
        {
            TransitionManager.Instance.OnTransitionEnter.Invoke(direction, stage);
        }
    }
}