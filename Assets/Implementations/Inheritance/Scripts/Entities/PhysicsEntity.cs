using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Abstract class, holds logic for entities that use our game physics.
    /// </summary>
    public abstract class PhysicsEntity : Entity
    {
        protected Vector3 velocity;
        protected float rotation;

        protected virtual void Update()
        {
            if(rotation != 0f)
            {
                transform.Rotate(0f, 0f, rotation * Time.deltaTime);
            }

            if(velocity.sqrMagnitude > 0f)
            {
                transform.localPosition += velocity * Time.deltaTime;
            }

            // Do bounds clamping
            var pos = transform.localPosition;
            if (pos.x <= 0f)
            {
                transform.localPosition += new Vector3(Consts.width, 0f, 0f);
            }
            if (pos.x > Consts.width)
            {
                transform.localPosition += new Vector3(-Consts.width, 0f, 0f);
            }
            if (pos.y <= 0f)
            {
                transform.localPosition += new Vector3(0f, Consts.height, 0f);
            }
            if (pos.y > Consts.height)
            {
                transform.localPosition += new Vector3(0f, -Consts.height, 0f);
            }
        }

        /// <summary>
        /// Triggered whenever another phyics entity collides with this one.
        /// </summary>
        protected virtual void OnCollided(PhysicsEntity other)
        {

        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            var entity = collider.gameObject.GetComponent<PhysicsEntity>();
            if(entity != null)
            {
                OnCollided(entity);
            }
        }
    }
}
