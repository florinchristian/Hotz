using UnityEngine;

namespace AI.impl
{
    public class AdvancedAI: INeighbourAI
    {
        public override void OnStart()
        {
            Debug.Log("Initialised Advanced AI!");
        }
        
        public override void UpdateGameObject()
        {
            throw new System.NotImplementedException();
        }
        
        protected override float GetPlayerDetectionRange()
        {
            return 20.0f;
        }
        
        protected override float GetWalkingSpeed()
        {
            return 1.0f;
        }
    }
}