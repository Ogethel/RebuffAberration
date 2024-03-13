using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ALS.Aberration
{
    /// <summary>
    /// A class should extend MonoBehaviour and implement this interface, then implement what should happen when that object takes damage.
    /// </summary>
    public interface IDamageable
    {
        void TakeDamage(int Damage);
        Transform GetTransform();
    }
}
