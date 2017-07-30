using Entitas;
using UnityEngine;

namespace EntityComponent
{
    /// <summary>
    /// Applies velocity and rotation.
    /// </summary>
    public class PhysicsSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> velocities;
        readonly IGroup<GameEntity> rotations;

        public PhysicsSystem(Contexts contexts)
        {
            velocities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Velocity, GameMatcher.Position));
            rotations = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Rotation, GameMatcher.Direction));
        }

        public void Execute()
        {
            foreach (GameEntity e in rotations.GetEntities())
            {
                e.ReplaceDirection(e.direction.value + e.rotation.value * Time.deltaTime);
            }

            foreach (GameEntity e in velocities.GetEntities())
            {
                float newX = e.position.x + e.velocity.x * Time.deltaTime;
                float newY = e.position.y + e.velocity.y * Time.deltaTime;
                e.ReplacePosition(newX, newY);
            }
        }
    }
}