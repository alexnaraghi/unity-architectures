using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inheritance
{
    /// <summary>
    /// Represents the heads up display in game.
    /// </summary>
    public class UI : Entity
    {
        [SerializeField] private Text text;
        [SerializeField] private GameObject[] lifeCounters;

        private void Update()
        {
            RefreshScore();
            RefreshCounters();
        }

        private void RefreshScore()
        {
            if(text != null)
            {
                text.text = GameplayState.instance.score.ToString();
            }
        }

        private void RefreshCounters()
        {
            var lives = Mathf.Clamp(GameplayState.instance.lives - 1, 0, lifeCounters.Length);

            // Refresh all the counters.
            for (int i = 0; i < lives; i++)
            {
                lifeCounters[i].SetActive(true);
            }
            for (int i = lives; i < lifeCounters.Length; i++)
            {
                lifeCounters[i].SetActive(false);
            }
        }
    }
}
