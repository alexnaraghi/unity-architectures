using System;
using Entitas;
using Entitas.Unity;

namespace EntityComponent
{
    /// <summary>
    /// The root level system for the game.  Also manages the game transitions between menu and gameplay.
    /// </summary>
    public sealed class GameSystems : Feature, ICleanupSystem
    {
        readonly Contexts contexts;

        /// <summary>
        /// The controller of this feature needs to re-initialize on the next frame on a full game-state clear.
        /// This flag is used to pass that information up.  Not beautiful, but sufficient for the purpose.
        /// </summary>
        public bool requiresReinitialization
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes all sub-systems.
        /// </summary>
        public GameSystems(Contexts contexts) : base("Gameplay Systems")
        {
            this.contexts = contexts;

            Add(new KeyboardInputSystem(contexts));
            Add(new PlayerMovementSystem(contexts));
            Add(new WeaponSystem(contexts));
            Add(new EnemySpawnerSystem(contexts));
            Add(new ViewSystems(contexts));
            Add(new PhysicsSystem(contexts));
            Add(new BoundsSystem(contexts));
            Add(new TimedInvincibilitySystem(contexts));
            Add(new CollisionSystem(contexts));
            Add(new TimedDestroySystem(contexts));
            Add(new DestroyedSpawnAssetSystem(contexts));
            Add(new DestroyedSpawnEntitySystem(contexts));
            Add(new GameStateSystem(contexts));
            Add(new RemoveViewSystem(contexts));
            Add(new DestroySystem(contexts));

            OpenStartMenu();
        }

        public override void Cleanup()
        {
            if (contexts.game.hasGameState)
            {
                // If in gameplay, use lives as the method of determining when the game is over.
                var lives = contexts.game.gameState.lives;
                if (lives <= 0)
                {
                    OpenRestartMenu(lives);
                }
            }
            else
            {
                // If in a menu, check for the continue button to transition to gameplay.
                foreach (var e in contexts.input.GetEntities())
                {
                    if (e.input.type == InputType.Continue)
                    {
                        OpenGameplay();
                        break;
                    }
                }
            }

            base.Cleanup();
        }

        public void OpenStartMenu()
        {
            contexts.game.CreateEntity()
                .AddAsset("EntityComponentPrefabs/StartGui");
        }

        public void OpenGameplay()
        {
            ResetECS();

            // Add singleton components.
            var singleton = contexts.game.CreateEntity();
            singleton.AddSpawner(0f);
            singleton.AddGameState(0, Config.instance.startLives);

            // Add initial entities.
            contexts.game.CreateHud();
            contexts.game.CreatePlayer();
        }

        public void OpenRestartMenu(int finalScore)
        {
            // We have to carry over the game state so the restart gui can read it.
            var score = contexts.game.gameState.score;
            ResetECS();

            // Add entities
            var gui = contexts.game.CreateEntity();
            gui.AddAsset("EntityComponentPrefabs/RestartGui");
            gui.AddScore(score);
        }

        public void ResetECS()
        {
            // Entitas has some functions that supposedly clean up the entities completely, but I found them to
            // cause errors.  Without being an expert at the engine-level code, here I manually clear out all
            // the entities of each context, as well as lingering particles.

            foreach (var e in contexts.game.GetEntities(GameMatcher.View))
            {
                var gameObject = e.view.gameObject;
                gameObject.Unlink();
                UnityEngine.Object.Destroy(gameObject);
            }

            foreach (var gameEntity in contexts.game.GetEntities())
            {
                gameEntity.Destroy();
            }

            foreach (var gameEntity in contexts.collision.GetEntities())
            {
                gameEntity.Destroy();
            }

            foreach (var gameEntity in contexts.input.GetEntities())
            {
                gameEntity.Destroy();
            }

            foreach (var untrackedView in UnityEngine.GameObject.FindGameObjectsWithTag("EntitasView"))
            {
                UnityEngine.Object.Destroy(gameObject);
            }

            // Flag this system as needing re-initialization.
            requiresReinitialization = true;
        }
    }
}