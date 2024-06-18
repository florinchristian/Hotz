using System.Collections.Generic;
using AI;
using AI.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        _aiComponent.SetGameOverCallback(() =>
        {
            SceneManager.LoadScene("Loser");
        });
        
        _aiComponent.OnStart();
    }
    
    void Update()
    {
        _aiComponent.UpdateGameObject();
    }

    public void NotifyThrownObject(Vector3 point)
    {
        _aiComponent.OnObjectDetect(point);
    }
}
