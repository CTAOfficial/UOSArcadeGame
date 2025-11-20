using System;
using Glorp.Combat;
using UnityEngine;


namespace Glorp
{
    public class LifeEntity : MonoBehaviour, IDamageable, IKillable
    {
        public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
        [SerializeField] int _maxHealth = 1;
        public int Health { get => _health; private set => _health = value; }
        [SerializeField] int _health = 1;

        public int ScoreValue = 100;

        public bool CanBeDamaged { get => _canBeDamaged; private set => _canBeDamaged = value; }
        [SerializeField] bool _canBeDamaged = true;
        public bool IsPlayer;

        public static Action<LifeEntity> OnCreated;
        public event Action<LifeEntity> OnDamaged;
        public event Action<LifeEntity> OnDeath;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            OnCreated?.Invoke(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool TryDamage(int damage)
        {
            if (damage <= 0 || !CanBeDamaged) { return false; }

            Damage(damage);
            return true;
        }

        public void ForceDamage(int damage) => Damage(damage);
        protected virtual void Damage(int damage)
        {
            Health -= damage;
            OnDamaged?.Invoke(this);

            if (Health <= 0) { Die(); }
        }

        public void Kill(GameObject killer = null)
        {
            Die();
        }

        void Die()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
        
    }
}
