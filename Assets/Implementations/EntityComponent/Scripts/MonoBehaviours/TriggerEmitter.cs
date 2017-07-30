using Entitas.Unity;
using UnityEngine;

namespace EntityComponent
{
    /// <summary>
    /// Passes collsion events from Unity into entitas by creating collision entities.
    /// </summary>
    public class TriggerEmitter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            var source = gameObject.GetEntityLink();
            var target = collider.gameObject.GetEntityLink();

            Contexts.sharedInstance.collision.CreateEntity()
                .AddCollision(source.entity, target.entity);
        }
    }
}