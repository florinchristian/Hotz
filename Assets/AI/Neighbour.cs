using AI;
using UnityEngine;

public class Neighbour : MonoBehaviour
{
    private INeighbourAI _aiComponent;
    
    void Start()
    {
        _aiComponent = GetComponent<INeighbourAI>();
        
        _aiComponent.OnStart();
    }
    
    void Update()
    {
        _aiComponent.UpdateGameObject();
    }
}
