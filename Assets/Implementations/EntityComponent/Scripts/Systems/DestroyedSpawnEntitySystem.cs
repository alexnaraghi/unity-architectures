using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public sealed class DestroyedSpawnEntitySystem : ReactiveSystem<GameEntity>
    {
        readonly GameContext context;

        public DestroyedSpawnEntitySystem(Contexts contexts) : base(contexts.game)
        {
            context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Destroyed);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyed && entity.hasSpawnEntityWhenDestroyed && entity.hasPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var type = e.spawnEntityWhenDestroyed.entity;
                var pos = e.position;

                switch (type)
                {
                    case EntityFactoryType.SmallAsteroid:
                        for (int i = 0; i < e.spawnEntityWhenDestroyed.count; i++)
                        {
                            context.CreateAsteroidRandomized(pos.x, pos.y, isBig: false);
                        }
                        break;
                }
            }
        }
    }
}