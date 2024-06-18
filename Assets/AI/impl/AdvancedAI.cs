using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace AI.impl
{
    public class AdvancedAI: INeighbourAI
    {
        private bool _isPatrolling = false;
        
        private bool _isFollowingObject = false;
        private Vector3 _detectedObject;
        
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
            if (_isFollowingObject)
            {
                MoveTowards(_detectedObject);

                if (HasArrivedAtPoint(_detectedObject))
                {
                    _isFollowingObject = false;
                }

                return;
            }
            
            var player = GetVisiblePlayer();

            if (player != null && PlayerInRange(player, GetPlayerDetectionRange()))
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
            if (!player.IsVisible())
            {
                return null;
            }
            
            return playerBody;
        }
        
        public override void OnObjectDetect(Vector3 point)
        {
            Debug.Log($"Object detected at {point}");
            
            _detectedObject = point;
            _isFollowingObject = true;
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