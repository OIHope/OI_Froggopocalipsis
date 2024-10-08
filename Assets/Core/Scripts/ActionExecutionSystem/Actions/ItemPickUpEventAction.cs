namespace ActionExecuteSystem
{
    public class ItemPickUpEventAction : ActionBase
    {
        protected override void ActionToPerform()
        {
            GameEventsBase.OnPlotItemPickUp?.Invoke();
        }
    }
}