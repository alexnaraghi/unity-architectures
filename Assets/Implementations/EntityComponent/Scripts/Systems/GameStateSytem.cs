using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    /// <summary>
    /// Reactive system that keeps the key gameplay state up to date.  Takes care of player respawning.
    /// </summary>
    public sealed class GameStateSystem : ReactiveSystem<GameEntity>
    {
        readonly GameContext context;

        public GameStateSystem(Contexts contexts) : base(contexts.game)
        {
            context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Destroyed);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyed && (entity.isPlayerControlled || entity.hasScore);
        }

        protected override void Execute(List<GameEntity> entities)
        {
            int lives = context.gameState.lives;
            int score = context.gameState.score;
            bool isDirty = false;
            foreach (var e in entities)
            {
                if (e.hasScore)
                {
                    score += e.score.value;
                    isDirty = true;
                }

                if (e.isPlayerControlled)
                {
                    lives--;
                    isDirty = true;

                    if (lives > 0)
                    {
                        context.CreatePlayer();
                    }
                }
            }

            if (isDirty)
            {
                context.ReplaceGameState(score, lives);
            }
        }
    }
}