using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

[Collision]
public sealed class CollisionComponent: IComponent
{
    public IEntity source;
    public IEntity target;
}