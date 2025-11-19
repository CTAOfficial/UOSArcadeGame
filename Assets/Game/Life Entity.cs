using System;
using Blazers.Combat;
using UnityEngine;


namespace Blazers
{
    public class LifeEntity : MonoBehaviour, IDamageable, IKillable
    {
        public int Health { get; private set; } = 1;

        public bool CanBeDamaged { get => _canBeDamaged; private set => _canBeDamaged = value; }
        [SerializeField] bool _canBeDamaged;

        public event Action<LifeEntity> OnDeath;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

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

            if (Health <= 0) { Die(); }
        }

        public void Kill(GameObject killer = null)
        {
            throw new System.NotImplementedException();
        }

        void Die()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
        
    }
}
