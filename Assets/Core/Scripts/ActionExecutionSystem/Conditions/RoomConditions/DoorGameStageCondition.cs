using UnityEngine;

namespace Core.Progression
{
    public class DoorGameStageCondition : RoomCondition
    {
        [SerializeField] private GameStage _stageToPass;
        public override void ClearAfterYourself()
        {
            
        }

        public override bool ConditionMet()
        {
            return _stageToPass <= GameStageManager.Instance.CurrentGameStage;
        }

        public override void PrepareCondition()
        {
            
        }
    }
}