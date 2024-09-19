using UnityEngine;

public abstract class RoomCondition : MonoBehaviour
{
    public abstract void PrepareCondition();
    public abstract bool ConditionMet();
}
