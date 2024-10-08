using UnityEngine;

namespace Core.Progression
{
    public abstract class RoomCondition : MonoBehaviour
    {
        public abstract void PrepareCondition();
        public abstract bool ConditionMet();
        public abstract void ClearAfterYourself();
    }
}