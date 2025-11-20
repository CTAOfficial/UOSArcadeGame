using UnityEngine;


namespace Glorp.Combat
{
    public interface IDamageable
    {
        GameObject gameObject { get; }
        string tag { get; }

        bool TryDamage(int damage);
        void ForceDamage(int damage);
    }

}