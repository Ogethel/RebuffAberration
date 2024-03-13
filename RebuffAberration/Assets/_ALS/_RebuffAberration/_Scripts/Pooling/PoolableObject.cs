using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ALS.Aberration
{
    /// <summary>
    /// PoolableObject
    /// </summary>
    public class PoolableObject : MonoBehaviour
    {
        public ObjectPool Parent;

        public virtual void OnDisable()
        {
            Parent.ReturnObjectToPool(this);
        }
    }
}
