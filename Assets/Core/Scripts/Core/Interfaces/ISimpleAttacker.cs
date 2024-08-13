using Components;

public interface ISimpleAttacker
{
    public DamageDealer SimpleDamageDealerComponent { get; }
    public CooldownComponent SimpleAttackCooldown { get; }
}
