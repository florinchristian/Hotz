using UnityEngine;

namespace AI.impl
{
    public class ModerateAI: BasicAI
    {
        public override void OnStart()
        {
            Debug.Log("Initialised Moderate AI!");
            
            TeleportToPoint(patrolPoints[0]);
        }
        
        protected override Transform GetVisiblePlayer()
        {
            return playerBody;
        }

        protected override float GetPlayerDetectionRange()
        {
            return 7.0f;
        }
        
        protected override float GetWalkingSpeed()
        {
            return 2.5f;
        }
    }
}