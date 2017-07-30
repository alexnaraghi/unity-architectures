using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public sealed class SpawnerComponent : IComponent
{
    public float lastSpawnTime;
}