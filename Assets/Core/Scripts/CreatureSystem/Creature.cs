using Data;
using Components;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public abstract class Creature : MonoBehaviour, IDamagable, IPushBack
    {
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        public SpriteRenderer Renderer { get => _spriteRenderer; set => _spriteRenderer = value; }

        protected bool _isAlive;
        public bool IsCreatureAlive => _isAlive;

        protected List<ComponentBase> _components;
        protected HealthComponent _healthComponent;

        public virtual void Init()
        {
            InitComponents();
            InitStateMachine();
            _healthComponent.OnDeath += CreatureDeath;
            _isAlive = true;
        }
        protected abstract void InitStateMachine();
        protected abstract void InitComponents();

        public virtual void ApplyImpulseOnCreature(Vector3 impulseDirection, float inpulsePower) { }
        public virtual void TakeDamage(AttackDataSO attackData, Vector3 attackVector, IDamagable target, IAttackableTarget attackerTransform)
        {
            IDamagable _target = this.GetComponent<IDamagable>();
            if (_target != target) return;

            int damage = attackData.GetDamage;
            Vector3 damageDirection = (transform.position - attackVector).normalized;
            _healthComponent.TakeDamage(damage);
            ApplyImpulseOnCreature(damageDirection, damage);
        }
        protected virtual void CreatureDeath(HealthComponent healthComponent)
        {
            _healthComponent.OnDeath -= CreatureDeath;
            Destroy(transform.gameObject);
            _isAlive = false;
            Debug.Log(transform.name + " is dead");
        }
        public virtual void RestoreThisCreature()
        {

        }
    }
}
