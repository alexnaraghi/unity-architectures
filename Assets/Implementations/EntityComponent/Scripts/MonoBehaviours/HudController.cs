using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace EntityComponent
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text label;
        [SerializeField] private GameObject[] lifeCounters;

        private void Start()
        {
            var context = Contexts.sharedInstance.game;
            context.GetGroup(GameMatcher.GameState).OnEntityUpdated += OnEntityUpdated;
            UpdateScore(context.gameState.score);
            UpdateLives(context.gameState.lives);
        }

        private void OnEntityUpdated(IGroup<GameEntity> group, GameEntity entity, int index,
            IComponent previousComponent, IComponent newComponent)
        {
            var gameState = (newComponent as GameStateComponent);
            UpdateScore(gameState.score);
            UpdateLives(gameState.lives);
        }

        private void UpdateScore(int score)
        {
            label.text = score.ToString();
        }

        private void UpdateLives(int lives)
        {
            // Go through the lives objects.  Set the right number active, and the rest inactive.
            lives = Mathf.Clamp(lives - 1, 0, lifeCounters.Length);
            for (int i = 0; i < lives; i++)
            {
                lifeCounters[i].SetActive(true);
            }
            for (int i = lives; i < lifeCounters.Length; i++)
            {
                lifeCounters[i].SetActive(false);
            }
        }

        private void OnDestroy()
        {
            var context = Contexts.sharedInstance.game;
            context.GetGroup(GameMatcher.GameState).OnEntityUpdated -= OnEntityUpdated;
        }
    }
}