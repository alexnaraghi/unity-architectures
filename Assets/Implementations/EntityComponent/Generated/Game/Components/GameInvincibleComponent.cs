//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly InvincibleComponent invincibleComponent = new InvincibleComponent();

    public bool isInvincible {
        get { return HasComponent(GameComponentsLookup.Invincible); }
        set {
            if (value != isInvincible) {
                if (value) {
                    AddComponent(GameComponentsLookup.Invincible, invincibleComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Invincible);
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

    static Entitas.IMatcher<GameEntity> _matcherInvincible;

    public static Entitas.IMatcher<GameEntity> Invincible {
        get {
            if (_matcherInvincible == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Invincible);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInvincible = matcher;
            }

            return _matcherInvincible;
        }
    }
}
