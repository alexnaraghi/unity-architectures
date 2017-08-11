using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Abstract class, acts as the base for all game entities with lifecycle management.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        public bool isAlive
        {
            get;
            set;
        }

        public virtual bool isInvulnerable
        {
            get
            {
                return false;
            }
        }

        protected virtual void Awake()
        {
            isAlive = true;
        }
    }
}
