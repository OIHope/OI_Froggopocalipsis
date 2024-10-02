using Entity;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class OnCreatureDeathCondition : ConditionBase
    {
        [SerializeField] private Creature _creature;
        public override bool ConditionIsValid()
        {
            return !_creature.IsCreatureAlive;
        }
    }
}