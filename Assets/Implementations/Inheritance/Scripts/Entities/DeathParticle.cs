using UnityEngine;

namespace Inheritance
{
    public class DeathParticle : Entity
    {
        [SerializeField] private ParticleSystem particle;

        public void Init(float x, float y)
        {
            transform.localPosition = new Vector3(x, y);
        }

        private void Update()
        {
            if(particle != null && !particle.isPlaying)
            {
                isAlive = false;
            }
        }        
    }
}
