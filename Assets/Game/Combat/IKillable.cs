using UnityEngine;


namespace Glorp.Combat
{
    public interface IKillable
    {
        void Kill(GameObject killer = null);
    }
}
