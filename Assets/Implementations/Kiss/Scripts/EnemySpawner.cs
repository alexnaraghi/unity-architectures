using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float initialObjectCount;
        [SerializeField] private float spawnIntervalSeconds;
        [SerializeField] private float minSpawnRadiusToPlayer;
        [SerializeField] private GameObject spawnPrefab;

        private float lastSpawnTime;

        public void OnEnable()
        {
            enabled = true;
            for (int i = 0; i < initialObjectCount; i++)
            {
                Spawn(spawnPrefab);
            }

            lastSpawnTime = Time.time;
        }

        private void Update()
        {
            var time = Time.time;
            if (lastSpawnTime + spawnIntervalSeconds < time)
            {
                lastSpawnTime = time;
                Spawn(spawnPrefab);
            }
        }

        public void OnDisable()
        {
            enabled = false;
            
            // Yea, could be optimized.
            // Consider managing spawned entities in a pool, and having a list of active entities
            // so we can clear them all without searching through the unity hierarchy.
            var objectsToDestroy = GameObject.FindGameObjectsWithTag("Asteroid");
            foreach(var obj in objectsToDestroy)
            {
                Destroy(obj);
            }
        }

        private void Spawn(GameObject prefab)
        {
            // Choose a spawn location that's not too close to the bounds of the player.
            Vector3 pos = new Vector3();
            bool isTooCloseToPlayer = false;
            do
            {
                pos = new Vector3(Random.Range(0f, Consts.width), Random.Range(0f, Consts.height), 0f);

                var player = GameManager.instance ? GameManager.instance.player : null;
                if (player != null)
                {
                    var offsetToPlayer = (pos - player.transform.position);
                    isTooCloseToPlayer = offsetToPlayer.sqrMagnitude < minSpawnRadiusToPlayer * minSpawnRadiusToPlayer;
                }
            }
            while (isTooCloseToPlayer);

            if (prefab != null)
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}