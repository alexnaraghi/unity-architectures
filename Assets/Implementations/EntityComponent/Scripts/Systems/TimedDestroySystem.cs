using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public class TimedDestroySystem : IExecuteSystem
    {
        readonly IGroup<GameEntity> entities;

        public TimedDestroySystem(Contexts contexts)
        {
            entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TimedDestroy));
        }

        public void Execute()
        {
            foreach (GameEntity e in entities.GetEntities())
            {
                if (Time.time >= e.timedDestroy.destroyTime)
                {
                    e.isDestroyed = true;
                }
            }
        }
    }
}