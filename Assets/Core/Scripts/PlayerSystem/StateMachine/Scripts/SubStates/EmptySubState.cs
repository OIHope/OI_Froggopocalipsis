using PlayerSystem;
using UnityEngine;

namespace BehaviourSystem
{
    [CreateAssetMenu(fileName = "Empty SubState", menuName = ("State Machine/Player/SubState/Empty SubState"))]
    public class EmptySubState : SubStateSO<PlayerStates, PlayerSubStates, PlayerControllerDataAccessor>
    {
        
    }
}