using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// The main in-game logic.
    /// </summary>
    public class GameplayState : GameState
    {
        // Global access if we are currently in this state.
        // Global access is really only needed for lives, score, and creating entities.  Consider using
        // a different abstraction to limit public access to the public GameState api itself.
        public static GameplayState instance
        {
            get;
            private set;
        }

        private const string bigAsteroidName = "AsteroidBig";

        private List<Entity> entities = new List<Entity>();
        private List<Entity> deadEntities = new List<Entity>();

        private float lastSpawnTime;
        private Player activePlayer;

        public int lives
        {
            get;
            private set;
        }

        public int score
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an entity from a resources-prefab and registers it to the entity list.
        /// Use this factory method to create entities.
        /// </summary>
        public T CreateAndRegisterEntity<T>(string name) where T : Entity
        {
            var entity = Utils.InstantiateFromResources<T>(name);
            if(entity != null)
            {
                entities.Add(entity);
            }
            return entity;
        }

        public void AddScore(int amount)
        {
            score += amount;
        }

        public void LoseLife()
        {
            lives--;

            if(lives > 0)
            {
                SpawnPlayer();
            }
            else
            {
                IsComplete = true;
            }
        }

        public override void Begin()
        {
            instance = this;
            lives = 4;
            SpawnPlayer();
            CreateUI();

            // spawn the initial asteroids.
            for (int i = 0; i < Consts.initialEnemySpawnCount; i++)
            {
                SpawnAsteroid(bigAsteroidName);
            }

            lastSpawnTime = Time.time;
        }

        public override void Execute()
        {
            ExecuteEnemySpawns();
            CleanupDeadEntities();
        }

        public override GameState Complete()
        {
            // Clean up all the entities when the state is complete.
            foreach(var entity in entities)
            {
                GameObject.Destroy(entity.gameObject);
            }
            entities.Clear();

            return new GameOverState(score);
        }

        private void CleanupDeadEntities()
        {
            deadEntities.Clear();
            foreach (var entity in entities)
            {
                if (!entity.isAlive)
                {
                    deadEntities.Add(entity);
                }
            }

            foreach (var deadEntity in deadEntities)
            {
                entities.Remove(deadEntity);
                GameObject.Destroy(deadEntity.gameObject);
            }
        }

        private void ExecuteEnemySpawns()
        {
            var time = Time.time;
            if (lastSpawnTime + Consts.spawnIntervalSeconds < time)
            {
                lastSpawnTime = time;
                SpawnAsteroid(bigAsteroidName);
            }
        }

        private void SpawnPlayer()
        {
            activePlayer = CreateAndRegisterEntity<Player>("Player");
            if(activePlayer != null)
            {
                activePlayer.Init(Consts.width / 2f, Consts.height / 2f);
            }
        }

        private void CreateUI()
        {
            CreateAndRegisterEntity<UI>("UI");
        }

        private void SpawnAsteroid(string prefabName)
        {
            var asteroid = CreateAndRegisterEntity<Asteroid>(prefabName);
            if(asteroid != null)
            {
                float x;
                float y;
                bool isTooCloseToPlayer = false;
                do
                {
                    x = UnityEngine.Random.Range(0f, Consts.width);
                    y = UnityEngine.Random.Range(0f, Consts.height);

                    if (activePlayer != null)
                    {
                        var offsetToPlayer = (new Vector3(x, y, 0f) - activePlayer.transform.position);
                        isTooCloseToPlayer = offsetToPlayer.sqrMagnitude < Consts.minSpawnRadiusToPlayer * Consts.minSpawnRadiusToPlayer;
                    }
                }
                while (isTooCloseToPlayer);
                
                asteroid.Init(x, y);
            }
        }
    }
}
