using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntityComponent
{
    [CreateAssetMenu(order = 1)]
    public class Config : ScriptableObject
    {
        public static Config instance
        {
            get;
            private set;
        }

        // The load must be manually called, we don't support lazy loading to conform to Entitas's philosophy
        // on straighforward, deterministic initialization.
        public static void Load(string path)
        {
            if (instance == null)
            {
                instance = Resources.Load<Config>(path);
            }
        }

        [Header("Gameplay")]
        public float width;
        public float height;
        public int startLives;

        [Header("Spawner")]
        public int numInitialEnemySpawns;
        public float enemySpawnSeconds;

        [Header("Asteroids")]
        public int asteroidSplitCount;
        public AsteroidConfig AsteroidBig;
        public AsteroidConfig AsteroidSmall;

        [Header("Weapon")]
        public float weaponCooldownSeconds;
        public float bulletVelocity;
        public float bulletLifeSeconds;

        [Header("Player")]
        public float playerAcceleration;
        public float playerRotation;
        public float playerMaxVelocity;
        public float playerBlinkIntervals;
        public float playerInvincibilitySeconds;

        [Header("Controls")]
        public KeyBinding[] bindings;

    }

    [Serializable]
    public struct AsteroidConfig
    {
        public float rotationDegPerSecond;
        public float minSpeed;
        public float maxSpeed;
        public int scoreValue;
    }

    [Serializable]
    public struct KeyBinding    
    {
        public InputType input;
        public KeyCode keyCode;
        public bool triggerWhenDown;
    }
}