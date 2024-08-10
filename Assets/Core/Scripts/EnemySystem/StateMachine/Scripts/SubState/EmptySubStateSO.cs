using EnemySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "Empty SubState", menuName = ("State Machine/Enemy/SubState/Empty SubState"))]
    public class EmptySubStateSO : SubStateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        
    }
}