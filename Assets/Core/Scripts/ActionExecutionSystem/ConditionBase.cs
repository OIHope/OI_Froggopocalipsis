using UnityEngine;

namespace ActionExecuteSystem
{
    public enum ConditionRequest
    {
        Equals, MoreThan, LessThan, MoreOrEquals, LessOrEquals
    }
    public abstract class ConditionBase : MonoBehaviour
    {
        public ConditionRequest request;
        public abstract bool ConditionIsValid();
    }
}