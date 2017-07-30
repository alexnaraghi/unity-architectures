using Entitas;
using UnityEngine;

namespace EntityComponent
{
    /// <summary>
    /// This behaviour manages Entitas.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private GameSystems systems;

        private void Awake()
        {
            Config.Load("EntityComponentPrefabs/DefaultConfig");

            // get a reference to the contexts
            var contexts = Contexts.sharedInstance;

            // create the systems by creating individual features
            systems = new GameSystems(contexts);
            systems.Initialize();
        }

        private void Update()
        {
            // The frame after clearing the game state, we need to re-initialize.
            if (systems.requiresReinitialization)
            {
                systems.Initialize();
                systems.requiresReinitialization = false;
            }

            // call Execute() on all the IExecuteSystems and 
            // ReactiveSystems that were triggered last frame
            systems.Execute();
            // call cleanup() on all the ICleanupSystems
            systems.Cleanup();
        }
    }
}