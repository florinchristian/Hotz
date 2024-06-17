using System.Collections.Generic;
using AI;
using AI.impl;
using UnityEngine;

public class Neighbour : MonoBehaviour
{
    private INeighbourAI _aiComponent;
    
    void Start()
    {
        var components = GetComponents<INeighbourAI>();

        foreach (var aiComponent in components)
        {
            if (aiComponent.isActiveAndEnabled)
            {
                _aiComponent = aiComponent;
                
                break;
            }
        }
        
        _aiComponent.OnStart();
    }
    
    void Update()
    {
        _aiComponent.UpdateGameObject();
    }
}
