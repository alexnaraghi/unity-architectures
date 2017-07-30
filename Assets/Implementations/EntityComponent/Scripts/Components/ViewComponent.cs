using Entitas;
using UnityEngine;

[Game]
public sealed class ViewComponent : IComponent 
{
    public GameObject gameObject;
    public SpriteRenderer sprite;
}
