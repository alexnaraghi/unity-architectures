using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The simple idea is that a prefab encapsulates all logic of managing a state, so destroying and 
/// instantiating prefabs is sufficient to move between different game states.
/// </summary>
public class StateTransition : MonoBehaviour 
{
    [SerializeField] private StateTransition nextStatePrefab;

    public void MoveToNextState()
    {
        if(nextStatePrefab != null)
        {
            Destroy(gameObject);
            Instantiate(nextStatePrefab);
        }
    }
}
