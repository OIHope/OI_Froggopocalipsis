using Core.Progression;

namespace ActionExecuteSystem
{
    public class AddSkillPointAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            PlayerProgressionManager.Instance.OnPlayerEarnSomeEXP?.Invoke(10000);
        }
    }
}