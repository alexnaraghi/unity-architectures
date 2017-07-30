using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class TimedInvincibilityComponent : IComponent 
{
    public float endTime;
}