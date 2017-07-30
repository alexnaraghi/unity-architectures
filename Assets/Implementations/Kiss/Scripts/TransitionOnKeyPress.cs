using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kiss
{
    /// <summary>
    /// Moves to the next state when a key is pressed.
    /// </summary>
    [RequireComponent(typeof(StateTransition))]
    public class TransitionOnKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode key;

        private StateTransition transition;

        private void Start()
        {
            transition = GetComponent<StateTransition>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(key) && transition != null)
            {
                transition.MoveToNextState();
            }
        }
    }
}