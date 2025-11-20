using UnityEngine;


namespace Glorp.Combat
{
    public interface IDamageable
    {
        bool TryDamage(int damage);
        void ForceDamage(int damage);
    }

}