public interface IInteractable
{
    public void Interact();
    public void DisplayInteraction();
    public void HideInteraction();
}
public interface IInteractor
{
    public bool RequestInteraction();
    public bool RequestAlternativeInteraction();
}
