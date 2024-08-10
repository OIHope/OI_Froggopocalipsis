using EnemySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem.EnemySystem
{
    [CreateAssetMenu(fileName = "TakeDamage SubState", menuName = ("State Machine/Enemy/SubState/TakeDamage SubState"))]
    public class TakeDamageSubStateSO : SubStateSO<EnemyState, EnemySubState, SimpleZombieControllerDataAccessor>
    {
        
    }
}