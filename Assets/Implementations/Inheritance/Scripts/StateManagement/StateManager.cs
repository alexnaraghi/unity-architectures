using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    /// <summary>
    /// Represents a stack of states that are transitioned from/to based on whether the top-most state
    /// is complete.  GameStates are responsible for controlling the game and deciding what the next state is.
    /// 
    /// If a game state does not choose a successor state, the next top of the stack becomes the active state,
    /// until there are no states left.
    /// 
    /// It is suggested to put the QuitGameState at the bottom of the stack, so if there's nothing left to
    /// do, the application exits.
    /// </summary>
    public abstract class StateManager : MonoBehaviour
    {
        private Stack<GameState> gameStates = new Stack<GameState>();

        private void Update()
        {
            if(gameStates.Count > 0)
            {
                // Execute the top-most game state.
                var gameState = gameStates.Peek();

                gameState.Execute();

                // If the game state is completed, execute the complete action and move to its successor, or 
                // move to the next state in the stack if no successor was chosen.
                if(gameState.IsComplete)
                {
                    var nextState = gameState.Complete();
                    gameStates.Pop();
                    PushState(nextState);
                }
            }
        }

        protected void PushState(GameState state)
        {
            if(state != null)
            {
                gameStates.Push(state);
                state.Begin();
            }
        }
    }
}
