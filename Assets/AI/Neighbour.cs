using AI;
using UnityEngine;

public class NeighbourAI : MonoBehaviour
{
    private INeighbourAI _aiComponent;
    
    void Start()
    {
        _aiComponent = GetComponent<INeighbourAI>();
    }
    
    void Update()
    {
        if (PlayerIsNearby(_aiComponent.GetPlayerDetectionRange()))
        {
            
        }
        
        _aiComponent.UpdateGameObject();
    }

    bool PlayerIsNearby(float range)
    {
        return true;
    }
}
