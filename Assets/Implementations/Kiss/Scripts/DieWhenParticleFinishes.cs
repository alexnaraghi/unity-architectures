using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class DieWhenParticleFinishes : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        private void Update()
        {
            if(particle != null && !particle.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}