using System;
using System.Linq;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class EnemySpawnerSystem : IInitializeSystem, IExecuteSystem
    {
        readonly GameContext context;

        public EnemySpawnerSystem(Contexts contexts)
        {
            context = contexts.game;
        }

        public void Initialize()
        {
            for (int i = 0; i < Config.instance.numInitialEnemySpawns; i++)
            {
                SpawnAsteroid();
            }

            if (context.hasSpawner)
            {
                context.spawner.lastSpawnTime = Time.time;
            }
        }

        public void Execute()
        {
            var time = Time.time;
            if (context.hasSpawner)
            {
                if (context.spawner.lastSpawnTime + Config.instance.enemySpawnSeconds < time)
                {
                    context.ReplaceSpawner(time);
                    SpawnAsteroid();
                }
            }
        }

        private void SpawnAsteroid()
        {
            //TODO: Don't create an asteroid within the minimum radius of the player.
            float x = UnityEngine.Random.Range(0f, Config.instance.width);
            float y = UnityEngine.Random.Range(0f, Config.instance.height);
            context.CreateAsteroidRandomized(x, y, isBig: true);
        }
    }
}