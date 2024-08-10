using Data;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(AttackDataSO attackData, Vector3 attackVector);
}
