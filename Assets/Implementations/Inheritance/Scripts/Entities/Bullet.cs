using System;
using UnityEngine;

namespace Inheritance
{
    public class Bullet : PhysicsEntity
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeSeconds;
        [SerializeField] private string deathParticlePrefabName;

        private float spawnTime;

        public void Init(Vector3 origin, float rotationDegrees)
        {
            transform.localPosition = origin;
            transform.localRotation = Quaternion.AngleAxis(rotationDegrees, Vector3.forward);
            spawnTime = Time.time;
            velocity = transform.up * speed;
        }

        protected override void Update()
        {
            base.Update();

            if(lifeSeconds + spawnTime < Time.time)
            {
                isAlive = false;
            }
        }

        protected override void OnCollided(PhysicsEntity other)
        {
            if(other is Asteroid)
            {
                isAlive = false;
            }
        }
    }
}
