using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

namespace EntityComponent
{
    /// <summary>
    /// Factory methods for creating game entities.
    /// </summary>
    public static class GameContextExtensions
    {
        public static GameEntity CreateAsteroidRandomized(this GameContext context, float x, float y, bool isBig)
        {
            GameEntity asteroid;
            float vX, vY, rot;
            if (isBig)
            {
                var config = Config.instance.AsteroidBig;
                RandomizeAsteroidParams(config.rotationDegPerSecond, config.minSpeed, config.maxSpeed,
                    out vX, out vY, out rot);
                asteroid = CreateAsteroidBig(context, x, y, vX, vY, rot);
            }
            else
            {
                var config = Config.instance.AsteroidSmall;
                RandomizeAsteroidParams(config.rotationDegPerSecond, config.minSpeed, config.maxSpeed,
                    out vX, out vY, out rot);
                asteroid = CreateAsteroidSmall(context, x, y, vX, vY, rot);
            }
            return asteroid;
        }

        public static GameEntity CreateAsteroidBig(this GameContext context, float x, float y,
            float velX, float velY, float rotVel)
        {
            var entity = context.CreateEntity();
            entity.AddAsset("EntityComponentPrefabs/AsteroidBig");
            entity.AddPosition(x, y);
            entity.AddVelocity(velX, velY);
            entity.AddDirection(0f);
            entity.AddRotation(rotVel);
            entity.AddSpawnAssetWhenDestroyed("EntityComponentPrefabs/DeathParticle");
            entity.AddSpawnEntityWhenDestroyed(EntityFactoryType.SmallAsteroid, Config.instance.asteroidSplitCount);
            entity.AddScore(Config.instance.AsteroidBig.scoreValue);
            return entity;
        }

        public static GameEntity CreateAsteroidSmall(this GameContext context, float x, float y,
            float velX, float velY, float rotVel)
        {
            var entity = context.CreateEntity();
            entity.AddAsset("EntityComponentPrefabs/AsteroidSmall");
            entity.AddPosition(x, y);
            entity.AddVelocity(velX, velY);
            entity.AddDirection(0f);
            entity.AddRotation(rotVel);
            entity.AddSpawnAssetWhenDestroyed("EntityComponentPrefabs/DeathParticle");
            entity.AddScore(Config.instance.AsteroidSmall.scoreValue);
            return entity;
        }

        public static GameEntity CreatePlayer(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddAsset("EntityComponentPrefabs/Player");
            entity.AddPosition(Config.instance.width / 2f, Config.instance.height / 2f);
            entity.AddVelocity(0f, 0f);
            entity.AddDirection(0f);
            entity.AddRotation(0f);
            entity.AddWeapon(float.MinValue);
            entity.isInvincible = true;
            entity.AddTimedInvincibility(Time.time + Config.instance.playerInvincibilitySeconds);
            entity.AddSpawnAssetWhenDestroyed("EntityComponentPrefabs/DeathParticle");
            entity.isPlayerControlled = true;
            return entity;
        }

        public static GameEntity CreateBullet(this GameContext context, float x, float y, float direction)
        {
            // Calculate the initial velocity of the bullet based on it's direction
            var dirRad = Mathf.Deg2Rad * (-direction + 90f);
            Vector2 v = new Vector2(Mathf.Cos(dirRad), Mathf.Sin(dirRad)) * Config.instance.bulletVelocity;

            var entity = context.CreateEntity();
            entity.AddAsset("EntityComponentPrefabs/Bullet");
            entity.AddPosition(x, y);
            entity.AddVelocity(v.x, v.y);
            entity.AddDirection(direction);
            entity.AddRotation(0f);
            entity.AddTimedDestroy(Time.time + Config.instance.bulletLifeSeconds);
            return entity;
        }

        public static GameEntity CreateHud(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddAsset("EntityComponentPrefabs/Hud");
            return entity;
        }

        /// <summary>
        /// Helper method to randomize some parameters for asteroids.
        /// </summary>
        /// <param name="rotationDegPerSecond">The rotation speed in degrees per second.</param>
        /// <param name="minSpeed">The min speed.</param>
        /// <param name="maxSpeed">The max speed.</param>
        /// <param name="vX">(out) A random start x velocity.</param>
        /// <param name="vY">(out) A random start y velocity.</param>
        /// <param name="rot">(out) A random rotation.</param>
        private static void RandomizeAsteroidParams(float rotationDegPerSecond, float minSpeed, float maxSpeed,
            out float vX, out float vY, out float rot)
        {
            var rotateScalar = UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1;
            var dir = UnityEngine.Random.Range(0f, 360f);
            var speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
            var v = new Vector2(Mathf.Cos(dir * Mathf.Deg2Rad), Mathf.Sin(dir * Mathf.Deg2Rad)) * speed;

            vX = v.x;
            vY = v.y;
            rot = rotateScalar * rotationDegPerSecond;
        }
    }
}