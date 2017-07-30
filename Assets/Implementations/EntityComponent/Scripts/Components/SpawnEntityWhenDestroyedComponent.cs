using Entitas;

public enum EntityFactoryType
{
    SmallAsteroid
}

[Game]
public sealed class SpawnEntityWhenDestroyedComponent: IComponent
{
    public EntityFactoryType entity;
    public int count;
} 