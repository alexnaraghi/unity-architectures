using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kiss
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private Text text;

        void Start()
        {
            OnScoreChanged();
        }

        public void OnScoreChanged()
        {
            var score = GameManager.instance.score;

            if (text != null)
            {
                text.text = score.ToString();
            }
        }
    }
}