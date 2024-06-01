using System;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class NeighbourAI : MonoBehaviour
{
    public Dictionary<string, INeighbourAI> AIType = new Dictionary<string, INeighbourAI>();
    
    void Start()
    {
        AIType["Standard"] = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
