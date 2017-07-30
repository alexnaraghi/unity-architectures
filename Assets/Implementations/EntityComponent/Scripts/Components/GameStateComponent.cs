using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public sealed class GameStateComponent : IComponent 
{
    public int score;
    public int lives;
}