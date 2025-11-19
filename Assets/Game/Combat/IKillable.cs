using UnityEngine;


namespace Blazers.Combat
{
    public interface IKillable
    {
        void Kill(GameObject killer = null);
    }
}
