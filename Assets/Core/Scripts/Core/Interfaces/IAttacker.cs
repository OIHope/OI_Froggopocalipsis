using Components;

public interface IAttacker
{
    public DamageDealer DamageDealerComponent { get; }
    public CooldownComponent AttackCooldown { get; }
}
