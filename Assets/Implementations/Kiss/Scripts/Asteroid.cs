using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float rotation;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private int score;
        [SerializeField] private GameObject deathSpawnObject;
        [SerializeField] private int deathSpawnCount;

        private int rotateScalar;
        private Vector3 startRot;
        private float speed;

        private void Start()
        {
            var rand = Random.Range(0f, 360f);
            transform.localRotation = Quaternion.Euler(0f, 0f, rand);

            // Randomly rotate clockwise or counter-clockwise
            rotateScalar = Random.Range(0, 2) == 0 ? 1 : -1;

            startRot = transform.up;
            speed = Random.Range(minSpeed, maxSpeed);
        }

        private void Update()
        {
            transform.Rotate(0f, 0f, rotateScalar * rotation * Time.deltaTime);
            transform.localPosition += startRot * speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.tag == "Bullet")
            {
                if (deathSpawnObject != null && deathSpawnCount > 0)
                {
                    for (int i = 0; i < deathSpawnCount; i++)
                    {
                        Instantiate(deathSpawnObject, transform.localPosition, transform.localRotation);
                    }
                }
                
                GameManager.instance.IncreaseScore(score);

                Destroy(gameObject);
            }
        }
    }
}