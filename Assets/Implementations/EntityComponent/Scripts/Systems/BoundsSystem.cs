using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public sealed class BoundsSystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> entities;

        public BoundsSystem(Contexts contexts)
        {
            entities = contexts.game.GetGroup(GameMatcher.Position);
        }

        public void Execute()
        {
            foreach (GameEntity e in entities.GetEntities())
            {
                float width = Config.instance.width;
                float height = Config.instance.height;

                bool isDirty = false;
                float x = e.position.x;
                float y = e.position.y;
                if (x <= 0f)
                {
                    x += width;
                    isDirty = true;
                }
                if (x > width)
                {
                    x -= width;
                    isDirty = true;
                }
                if (y <= 0f)
                {
                    y += height;
                    isDirty = true;
                }
                if (y > height)
                {
                    y -= height;
                    isDirty = true;
                }

                if (isDirty)
                {
                    e.ReplacePosition(x, y);
                }
            }
        }
    }
}