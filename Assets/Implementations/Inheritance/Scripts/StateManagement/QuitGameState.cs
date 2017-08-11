using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Used as the bottom-level state in the state stack.  If there's no more states to run, quit the application.
    /// </summary>
    public class QuitGameState : GameState
    {
        public override void Begin()
        {
            IsComplete = true;
        }

        public override GameState Complete()
        {
            Application.Quit();
            return null;
        }
    }
}
