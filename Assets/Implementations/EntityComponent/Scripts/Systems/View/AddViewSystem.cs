using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem<GameEntity>
{
    readonly Transform viewContainer = new GameObject("Game Views").transform;
    readonly GameContext context;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Asset);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAsset && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            var prefab = Resources.Load<GameObject>(e.asset.name);
            if(prefab != null)
            {
                var go = GameObject.Instantiate(prefab);
                var sprite = go.GetComponent<SpriteRenderer>();
                go.transform.SetParent(viewContainer, worldPositionStays: false);
                e.AddView(go, sprite);
                go.Link(e, context);
            }
        }
    }
}