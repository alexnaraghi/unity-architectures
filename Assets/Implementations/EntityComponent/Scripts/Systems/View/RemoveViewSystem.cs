using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity>, ITearDownSystem
{
    readonly IGroup<GameEntity> views;

    public RemoveViewSystem(Contexts contexts) : base(contexts.game)
    {
        views = contexts.game.GetGroup(GameMatcher.View);
    }

    public void TearDown()
    {
        foreach(var e in views.GetEntities())
        {
            destroyView(e.view);
            e.Destroy();
        }
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return new Collector<GameEntity>(
            new[] {
                context.GetGroup(GameMatcher.View),
                context.GetGroup(GameMatcher.Destroyed)
            },
            new[] {
                GroupEvent.Removed,
                GroupEvent.Added
            }
        );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            destroyView(e.view);
            e.RemoveView();
        }
    }

    void destroyView(ViewComponent viewComponent)
    {
        var gameObject = viewComponent.gameObject;
        gameObject.Unlink();
        UnityEngine.Object.Destroy(gameObject);
    }
}