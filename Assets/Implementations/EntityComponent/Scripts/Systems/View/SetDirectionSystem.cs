using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SetDirectionSystem : ReactiveSystem<GameEntity>
{
    public SetDirectionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Direction);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasDirection;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            float angle = e.direction.value;
            e.view.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
        }
    }
}