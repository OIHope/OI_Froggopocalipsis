namespace ActionExecuteSystem
{
    public class BackToMenuAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            GameEventsBase.OnBackToMaunMenu?.Invoke();
        }
    }
}