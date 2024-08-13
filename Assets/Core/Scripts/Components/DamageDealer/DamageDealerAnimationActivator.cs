using Components;
using UnityEngine;

public class DamageDealerAnimationActivator : MonoBehaviour
{
    [SerializeField] private DamageDealer _simpleDamageDealer;
    [SerializeField] private DamageDealer _powerDamageDealer;
    [SerializeField] private DamageDealer _dashDamageDealer;

    public void EnableSimpleDamageDealer() => _simpleDamageDealer?.StartAttack();
    public void DisableSimpleDamageDealer() => _simpleDamageDealer?.FinishAttack();

    public void EnablePowerDamageDealer() => _powerDamageDealer?.StartAttack();
    public void DisablePowerDamageDealer() => _powerDamageDealer?.FinishAttack();

    public void EnableDashDamageDealer() => _dashDamageDealer?.StartAttack();
    public void DisableDashDamageDealer() => _dashDamageDealer?.FinishAttack();
}
