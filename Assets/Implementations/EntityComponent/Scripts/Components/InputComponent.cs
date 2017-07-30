using Entitas;

public enum InputType
{
    MoveUp,
    MoveLeft,
    MoveDown,
    MoveRight,
    RotateClockwise,
    RotateCounterClockwise,
    Fire,
    Continue
}

[Input]
public sealed class InputComponent : IComponent 
{
    public InputType type;
}