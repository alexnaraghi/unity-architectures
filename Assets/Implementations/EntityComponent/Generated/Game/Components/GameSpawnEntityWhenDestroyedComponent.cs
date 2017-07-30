//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpawnEntityWhenDestroyedComponent spawnEntityWhenDestroyed { get { return (SpawnEntityWhenDestroyedComponent)GetComponent(GameComponentsLookup.SpawnEntityWhenDestroyed); } }
    public bool hasSpawnEntityWhenDestroyed { get { return HasComponent(GameComponentsLookup.SpawnEntityWhenDestroyed); } }

    public void AddSpawnEntityWhenDestroyed(EntityFactoryType newEntity, int newCount) {
        var index = GameComponentsLookup.SpawnEntityWhenDestroyed;
        var component = CreateComponent<SpawnEntityWhenDestroyedComponent>(index);
        component.entity = newEntity;
        component.count = newCount;
        AddComponent(index, component);
    }

    public void ReplaceSpawnEntityWhenDestroyed(EntityFactoryType newEntity, int newCount) {
        var index = GameComponentsLookup.SpawnEntityWhenDestroyed;
        var component = CreateComponent<SpawnEntityWhenDestroyedComponent>(index);
        component.entity = newEntity;
        component.count = newCount;
        ReplaceComponent(index, component);
    }

    public void RemoveSpawnEntityWhenDestroyed() {
        RemoveComponent(GameComponentsLookup.SpawnEntityWhenDestroyed);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherSpawnEntityWhenDestroyed;

    public static Entitas.IMatcher<GameEntity> SpawnEntityWhenDestroyed {
        get {
            if (_matcherSpawnEntityWhenDestroyed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SpawnEntityWhenDestroyed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpawnEntityWhenDestroyed = matcher;
            }

            return _matcherSpawnEntityWhenDestroyed;
        }
    }
}