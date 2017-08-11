using UnityEngine;

namespace Inheritance
{
    public class Player : PhysicsEntity
    {
        [Header ("Movement")]
        [SerializeField] private float accelerationSpeed;
        [SerializeField] private float rotationDegreesPerSecond;
        [SerializeField] private float maxVelocityPerSecond;

        [Header ("Weapon")]
        [SerializeField] private GameObject weaponOrigin;
        [SerializeField] private string bulletPrefabName;
        [SerializeField] private float shotsPerSecond;

        [Header ("Invulnerability Animation")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float invulnerabilityPeriod;
        [SerializeField] private float animNumIntervals;
        [SerializeField] private float animAlphaHigh;
        [SerializeField] private float animAlphaLow;

        [Header ("Dying")]
        [SerializeField] private string deathParticleName;

        private float lastShotTime;
        private float spawnTime;

        public override bool isInvulnerable
        {
            get
            {
                return Time.time < spawnTime + invulnerabilityPeriod;
            }
        }

        public void Init(float x, float y)
        {
            transform.position = new Vector3(x, y, 0f);
            velocity = new Vector3(3f, 0f, 0f);
            spawnTime = Time.time;
        }

        protected override void Update()
        {
            float aX = 0f, aY = 0f;
            
            // Update velocity
            {
                if (Input.GetKey(KeyCode.W))
                {
                    aY += 1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    aX -= 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    aY -= 1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    aX += 1f;
                }

                velocity += new Vector3(aX, aY, 0f).normalized * accelerationSpeed;
                if (velocity.magnitude > maxVelocityPerSecond)
                {
                    velocity.Normalize();
                    velocity *= maxVelocityPerSecond;
                }
            }

            // Update rotation
            {
                rotation = 0f;
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rotation += 1f;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rotation -= 1f;
                }
                rotation *= rotationDegreesPerSecond;
            }

            // Shoot
            var time = Time.time;
            if (Input.GetKey(KeyCode.Space) && (lastShotTime + 1f / shotsPerSecond < time))
            {
                lastShotTime = time;
                Fire();
            }
            
            // Update invulnerability effect
            if(spriteRenderer != null)
            {
                if(isInvulnerable)
                {
                    // Do a blink effect until the invulnerability period ends
                    float invulnerabilityTime = Time.time - spawnTime;
                    if ((int)(invulnerabilityTime / invulnerabilityPeriod * animNumIntervals) % 2 == 0)
                    {
                        spriteRenderer.color = new Color(1f, 1f, 1f, animAlphaLow);
                    }
                    else
                    {
                        spriteRenderer.color = new Color(1f, 1f, 1f, animAlphaHigh);
                    }
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
            }

            base.Update();
        }

        private void Fire()
        {
            var origin = weaponOrigin ?? gameObject;

            var projectile = GameplayState.instance.CreateAndRegisterEntity<Bullet>(bulletPrefabName);
            if (projectile != null)
            {
                projectile.Init(origin.transform.position, origin.transform.rotation.eulerAngles.z);
            }
        }

        protected override void OnCollided(PhysicsEntity other)
        {
            if(isAlive && !isInvulnerable && other is Asteroid)   
            {
                isAlive = false;

                var particle = GameplayState.instance.CreateAndRegisterEntity<DeathParticle>(deathParticleName);
                if(particle != null)
                {
                    particle.Init(transform.localPosition.x, transform.localPosition.y);
                }

                GameplayState.instance.LoseLife();
            }
        }
    }
}
