using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace EntityComponent
{
    public sealed class DestroyedSpawnAssetSystem : ReactiveSystem<GameEntity>
    {
        public DestroyedSpawnAssetSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Destroyed);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDestroyed && entity.hasSpawnAssetWhenDestroyed && entity.hasPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var prefab = Resources.Load<GameObject>(e.spawnAssetWhenDestroyed.asset);
                if (prefab != null)
                {
                    // This object is disconnected from the entity system, we assume it is visual only and 
                    // cleans up itself.
                    var pos = e.position;
                    var go = GameObject.Instantiate(prefab, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
                    go.tag = "EntitasView";
                }
            }
        }
    }
}