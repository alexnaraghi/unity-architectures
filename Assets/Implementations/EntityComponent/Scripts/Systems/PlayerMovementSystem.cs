using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class PlayerMovementSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> players;
        readonly IGroup<InputEntity> keys;

        public PlayerMovementSystem(Contexts contexts)
        {
            players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerControlled, GameMatcher.Velocity, GameMatcher.Rotation));
            keys = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            float playerAcceleration = Config.instance.playerAcceleration;
            float playerRotation = Config.instance.playerRotation;
            GameEntity[] playerEntities = players.GetEntities();

            var aX = 0f;
            var aY = 0f;
            float rot = 0f;

            foreach (InputEntity e in keys.GetEntities())
            {
                switch (e.input.type)
                {
                    case InputType.MoveLeft: aX -= 1f; break;
                    case InputType.MoveUp: aY += 1f; break;
                    case InputType.MoveRight: aX += 1f; break;
                    case InputType.MoveDown: aY -= 1f; break;

                    case InputType.RotateClockwise: rot += 1f; break;
                    case InputType.RotateCounterClockwise: rot -= 1f; break;
                }
            }

            foreach (var player in playerEntities)
            {
                float maxVelocity = Config.instance.playerMaxVelocity;

                var v = new Vector2(player.velocity.x + aX * playerAcceleration,
                                    player.velocity.y + aY * playerAcceleration);

                // Clamp
                if (v.sqrMagnitude > maxVelocity * maxVelocity)
                {
                    v = v.normalized * maxVelocity;
                }
                player.ReplaceVelocity(v.x, v.y);

                player.ReplaceRotation(rot * playerRotation);
            }
        }
    }
}