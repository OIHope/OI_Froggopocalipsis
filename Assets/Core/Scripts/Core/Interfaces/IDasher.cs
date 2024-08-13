using Components;

public interface IDasher
{
    public DamageDealer DashDamageDealerComponent { get; }
    public CooldownComponent DashCooldown { get; }
}
