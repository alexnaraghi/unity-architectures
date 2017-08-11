using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Abstract class, holds logic for a menu that is transitioned away from by pressing the space key.
    /// </summary>
    public abstract class OnePressMenuState : GameState
    {
        private Canvas menu;

        public override void Begin()
        {
            menu = LoadMenu();
        }

        /// <summary>
        /// Implement this method to load a canvas.
        /// </summary>
        public abstract Canvas LoadMenu();

        public override void Execute()
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                IsComplete = true;
            }
        }

        public override GameState Complete()
        {
            if(menu != null)
            {
                GameObject.Destroy(menu.gameObject);
            }
            return new GameplayState();
        }
    }
}
