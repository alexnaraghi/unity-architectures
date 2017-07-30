using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace EntityComponent
{
    public sealed class GameTransitionSystem : Feature, ICleanupSystem
    {
        readonly Contexts contexts;

        public GameTransitionSystem(Contexts contexts)
        {
            this.contexts = contexts;
        }

        public override void Cleanup()
        {
            if (contexts.game.hasGameState)
            {
                var lives = contexts.game.gameState.lives;
                if (lives <= 0)
                {
                    OpenRestartMenu(lives);
                }
            }
            else
            {
                foreach (var e in contexts.input.GetEntities())
                {
                    if (e.input.type == InputType.Continue)
                    {
                        OpenGameplay();
                    }
                }
            }
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
            ResetECS();

            // Add initial entities
            contexts.game.CreateEntity()
                .AddAsset("EntityComponentPrefabs/RestartGui");
        }

        public void ResetECS()
        {
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

            foreach (var collisionEntity in contexts.collision.GetEntities())
            {
                collisionEntity.Destroy();
            }

            foreach (var untrackedView in GameObject.FindGameObjectsWithTag("EntitasView"))
            {
                UnityEngine.Object.Destroy(gameObject);
            }
        }
    }
}