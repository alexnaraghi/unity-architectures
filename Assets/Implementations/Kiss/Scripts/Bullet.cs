using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private float velocity;
    [SerializeField] private float lifeSeconds;
    [SerializeField] private GameObject deathParticle;

    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update () 
    {
        transform.localPosition += transform.up * Time.deltaTime * velocity;

        if(lifeSeconds + spawnTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(deathParticle != null)
        {
            Instantiate(deathParticle, transform.localPosition, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}
