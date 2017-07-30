using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kiss
{
    public class LifeCounter : MonoBehaviour
    {
        [SerializeField] private GameObject[] counters;

        void Start()
        {
            OnLivesChanged();
        }

        public void OnLivesChanged()
        {
            var lives = Mathf.Clamp(GameManager.instance.lives - 1, 0, counters.Length);

            // Refresh all the counters.
            for (int i = 0; i < lives; i++)
            {
                counters[i].SetActive(true);
            }
            for (int i = lives; i < counters.Length; i++)
            {
                counters[i].SetActive(false);
            }
        }
    }
}