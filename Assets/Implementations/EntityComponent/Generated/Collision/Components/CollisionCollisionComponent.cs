//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CollisionEntity {

    public CollisionComponent collision { get { return (CollisionComponent)GetComponent(CollisionComponentsLookup.Collision); } }
    public bool hasCollision { get { return HasComponent(CollisionComponentsLookup.Collision); } }

    public void AddCollision(Entitas.IEntity newSource, Entitas.IEntity newTarget) {
        var index = CollisionComponentsLookup.Collision;
        var component = CreateComponent<CollisionComponent>(index);
        component.source = newSource;
        component.target = newTarget;
        AddComponent(index, component);
    }

    public void ReplaceCollision(Entitas.IEntity newSource, Entitas.IEntity newTarget) {
        var index = CollisionComponentsLookup.Collision;
        var component = CreateComponent<CollisionComponent>(index);
        component.source = newSource;
        component.target = newTarget;
        ReplaceComponent(index, component);
    }

    public void RemoveCollision() {
        RemoveComponent(CollisionComponentsLookup.Collision);
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
public sealed partial class CollisionMatcher {

    static Entitas.IMatcher<CollisionEntity> _matcherCollision;

    public static Entitas.IMatcher<CollisionEntity> Collision {
        get {
            if (_matcherCollision == null) {
                var matcher = (Entitas.Matcher<CollisionEntity>)Entitas.Matcher<CollisionEntity>.AllOf(CollisionComponentsLookup.Collision);
                matcher.componentNames = CollisionComponentsLookup.componentNames;
                _matcherCollision = matcher;
            }

            return _matcherCollision;
        }
    }
}
