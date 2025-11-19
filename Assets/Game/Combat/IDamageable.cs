using UnityEngine;


namespace Blazers.Combat
{
    public interface IDamageable
    {
        bool TryDamage(int damage);
        void ForceDamage(int damage);
    }

}