using Data;
using Components;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public abstract class Creature : MonoBehaviour, IDamagable, IPushBack
    {

        protected List<ComponentBase> _components;
        protected HealthComponent _healthComponent;

        protected virtual void Awake()
        {
            InitComponents();
            InitStateMachine();
            _healthComponent.OnDeath += CreatureDeath;
        }
        protected abstract void InitStateMachine();
        protected abstract void InitComponents();

        public virtual void ApplyImpulseOnCreature(Vector3 impulseDirection, float inpulsePower) { }
        public virtual void TakeDamage(AttackDataSO attackData, Vector3 attackVector)
        {
            int damage = attackData.GetDamage;
            Vector3 damageDirection = (transform.position - attackVector).normalized;
            _healthComponent.TakeDamage(damage);
            ApplyImpulseOnCreature(damageDirection, damage);
        }
        protected virtual void CreatureDeath(HealthComponent healthComponent)
        {
            _healthComponent.OnDeath -= CreatureDeath;
            Destroy(transform.gameObject);
            Debug.Log(transform.name + " is dead");
        }
    }
}
