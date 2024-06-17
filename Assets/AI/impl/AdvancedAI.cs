using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace AI.impl
{
    public class AdvancedAI: INeighbourAI
    {
        private bool _isPatrolling = false;
        
        public override void OnStart()
        {
            Debug.Log("Initialised Advanced AI!");
        }

        private IEnumerator Patrol()
        {
            while (true)
            {
                TravelToNewPoint();
                
                yield return new WaitForSeconds(3);
            }
        }

        private void TravelToNewPoint()
        {
            var rotation = PickRandomRotation();
            var distance = PickRandomDistance();
                
            aiBody.Rotate(Vector3.up, rotation);
            agent.SetDestination(aiBody.transform.forward * distance);
        }

        private float PickRandomRotation()
        {
            return new Random().Next(-90, 90) * 1.0f;
        }

        private float PickRandomDistance()
        {
            return new Random().Next(0, 5) * 1.0f;
        }
        
        public override void UpdateGameObject()
        {
            var player = GetVisiblePlayer();

            if (PlayerInRange(player, GetPlayerDetectionRange()))
            {
                StopCoroutine(nameof(Patrol));
                _isPatrolling = false;
                MoveTowards(player.position);   
            }
            else if (!_isPatrolling)
            {
                StartCoroutine(nameof(Patrol));
                _isPatrolling = true;
            }
        }
        
        protected override Transform GetVisiblePlayer()
        {
            return playerBody;
        }
        
        protected override float GetPlayerDetectionRange()
        {
            return 8.0f;
        }
        
        protected override float GetWalkingSpeed()
        {
            return 1.0f;
        }
    }
}