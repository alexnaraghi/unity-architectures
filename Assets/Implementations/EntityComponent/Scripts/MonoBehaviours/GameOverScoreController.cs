using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace EntityComponent
{
    /// <summary>
    /// Displays the score after the game ends.
    /// </summary>
    public class GameOverScoreController : MonoBehaviour
    {
        [SerializeField] Text text;

        private void Start()
        {
            var entityLink = GetComponent<EntityLink>();
            if (text != null && entityLink != null)
            {
                var gameEntity = entityLink.entity as GameEntity;
                if (gameEntity != null && gameEntity.hasScore)
                {
                    text.text = gameEntity.score.value.ToString();
                }
            }
        }
    }
}