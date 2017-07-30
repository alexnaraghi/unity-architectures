//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PlayerControlledComponent playerControlledComponent = new PlayerControlledComponent();

    public bool isPlayerControlled {
        get { return HasComponent(GameComponentsLookup.PlayerControlled); }
        set {
            if (value != isPlayerControlled) {
                if (value) {
                    AddComponent(GameComponentsLookup.PlayerControlled, playerControlledComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.PlayerControlled);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherPlayerControlled;

    public static Entitas.IMatcher<GameEntity> PlayerControlled {
        get {
            if (_matcherPlayerControlled == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerControlled);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerControlled = matcher;
            }

            return _matcherPlayerControlled;
        }
    }
}