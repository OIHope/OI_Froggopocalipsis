public interface IInvincibility
{
    public bool Invincible { get; set; }
    public void MakeInvincible(bool value);
    public void ToggleColliders(bool value);
}
