using System;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class CollisionSystem : IExecuteSystem, ICleanupSystem
    {
        readonly IGroup<CollisionEntity> collisions;

        public CollisionSystem(Contexts contexts)
        {
            collisions = contexts.collision.GetGroup(CollisionMatcher.Collision);
        }

        public void Execute()
        {
            foreach (var e in collisions.GetEntities())
            {
                var collision = e.collision;
                var gameSource = (collision.source as GameEntity);
                var gameTarget = (collision.target as GameEntity);

                if (gameSource.isEnabled && gameTarget.isEnabled && !gameSource.isInvincible && !gameTarget.isInvincible)
                {
                    // Note that asteroids do not get destroyed when they collide with a player.
                    if(!gameSource.isPlayerControlled)
                    {
                        gameTarget.isDestroyed = true;       
                    }

                    if(!gameTarget.isPlayerControlled)
                    {
                        gameSource.isDestroyed = true;
                    }
                }
            }
        }

        public void Cleanup()
        {
            foreach (var e in collisions.GetEntities())
            {
                e.Destroy();
            }
        }
    }
}