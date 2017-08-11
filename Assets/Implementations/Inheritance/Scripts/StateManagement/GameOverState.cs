using UnityEngine;
using UnityEngine.UI;

namespace Inheritance
{
    public class GameOverState : OnePressMenuState
    {
        private int finalScore;
        
        public GameOverState(int score)
        {
            finalScore = score;
        }

        public override Canvas LoadMenu()
        {
            var menu = Utils.InstantiateFromResources<Canvas>("RestartState");

            if(menu != null)
            {
                var scoreText = menu.GetComponentInChildren<Text>();
                if(scoreText != null)
                {
                    scoreText.text = finalScore.ToString();
                }
            }

            return menu;
        }
    }
}
