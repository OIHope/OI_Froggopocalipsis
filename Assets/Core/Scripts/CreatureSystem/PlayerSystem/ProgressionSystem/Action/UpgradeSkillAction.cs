using Core.Progression;
using UnityEngine;

namespace ActionExecuteSystem
{
    public class UpgradeSkillAction : ActionBase
    {
        [SerializeField] private PlayerSkill _skill;
        protected override void ActionToPerform()
        {
            PlayerProgressionManager.Instance.OnPlayerWantsToUpSkill?.Invoke(_skill);
        }
    }
}