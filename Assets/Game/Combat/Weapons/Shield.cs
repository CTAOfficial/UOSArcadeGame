using Glorp.Combat;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    public void ForceDamage(int damage)
    {
    }
    public bool TryDamage(int damage)
    {
        return true;
    }
}
