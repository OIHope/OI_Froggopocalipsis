using Core.Progression;

namespace ActionExecuteSystem
{
    public class CloseLevelUpMenuAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            PlayerProgressionManager.Instance.OnCloseLevelUpMenu?.Invoke();
        }
    }
}