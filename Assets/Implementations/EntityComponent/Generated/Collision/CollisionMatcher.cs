//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CollisionMatcher {

    public static Entitas.IAllOfMatcher<CollisionEntity> AllOf(params int[] indices) {
        return Entitas.Matcher<CollisionEntity>.AllOf(indices);
    }

    public static Entitas.IAllOfMatcher<CollisionEntity> AllOf(params Entitas.IMatcher<CollisionEntity>[] matchers) {
          return Entitas.Matcher<CollisionEntity>.AllOf(matchers);
    }

    public static Entitas.IAnyOfMatcher<CollisionEntity> AnyOf(params int[] indices) {
          return Entitas.Matcher<CollisionEntity>.AnyOf(indices);
    }

    public static Entitas.IAnyOfMatcher<CollisionEntity> AnyOf(params Entitas.IMatcher<CollisionEntity>[] matchers) {
          return Entitas.Matcher<CollisionEntity>.AnyOf(matchers);
    }
}