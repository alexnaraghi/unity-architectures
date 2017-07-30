using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kiss
{
    public class GameManager : MonoBehaviour
    {
        // Events that notify listeners when game changes have occurred.
        [SerializeField] private UnityEvent GameStartedEvent;
        [SerializeField] private UnityEvent GameEndedEvent;
        [SerializeField] private UnityEvent LivesChangedEvent;
        [SerializeField] private UnityEvent ScoreChangedEvent;

        [SerializeField] private GameObject playerPrefab;

        public GameObject player
        {
            get;
            private set;
        }

        public int score
        {
            get;
            private set;
        }

        public int lives
        {
            get;
            private set;
        }

        public static GameManager instance
        {
            get;
            private set;
        }

        public void LoseLife()
        {
            if(lives > 0)
            {
                lives--;
                LivesChangedEvent.Invoke();

                if (lives == 0)
                {
                    GameEndedEvent.Invoke();
                }
                else
                {
                    SpawnPlayer();
                }
            }
        }

        public void SpawnPlayer()
        {
            if (playerPrefab != null)
            {
                player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            }
        }

        public void IncreaseScore(int points)
        {
            score += points;
            ScoreChangedEvent.Invoke();
        }

        private void Awake()
        {
            instance = this;
            SetupGame();
        }

        private void SetupGame()
        {
            score = 0;
            ScoreChangedEvent.Invoke();

            lives = Consts.startLives;
            LivesChangedEvent.Invoke();

            SpawnPlayer();

            GameStartedEvent.Invoke();
        }
    }
}