using UnityEngine;

namespace AI.impl
{
    public class ModerateAI: INeighbourAI
    {
        public override void OnStart()
        {
            Debug.Log("Initialised Moderate AI!");
        }
        
        public override void UpdateGameObject()
        {
            throw new System.NotImplementedException();
        }
        
        protected override float GetPlayerDetectionRange()
        {
            return 10.0f;
        }
        
        protected override float GetWalkingSpeed()
        {
            return 5.0f;
        }
    }
}