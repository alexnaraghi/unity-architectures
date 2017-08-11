using System;
using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Abstract class, encapsulates code common to all asteroid types.
    /// </summary>
    public abstract class Asteroid : PhysicsEntity
    {
        [SerializeField] private float rotationDegrees;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int score;
        [SerializeField] private string deathParticleName;

        public void Init(float x, float y)
        {
            transform.localPosition = new Vector3(x, y, 0f);

            var rand = UnityEngine.Random.Range(0f, 360f);
            transform.localRotation = Quaternion.Euler(0f, 0f, rand);

            // Randomly rotate clockwise or counter-clockwise
            var rotateScalar = UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;
            rotation = rotateScalar * rotationDegrees;

            velocity = transform.up * UnityEngine.Random.Range(minSpeed, maxSpeed);
        }

        protected override void OnCollided(PhysicsEntity other)
        {
            if(isAlive && other is Bullet)   
            {
                isAlive = false;
                OnDead();
            }
        }

        protected virtual void OnDead()
        {
            GameplayState.instance.AddScore(score);

            var particle = GameplayState.instance.CreateAndRegisterEntity<DeathParticle>(deathParticleName);
            if(particle != null)
            {
                particle.Init(transform.localPosition.x, transform.localPosition.y);
            }
        }
    }
}
