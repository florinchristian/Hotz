using System.Collections.Generic;
using UnityEngine;

namespace AI.impl
{
    public class BasicAI: INeighbourAI
    {
        public List<Vector3> patrolPoints;

        private int _currentPatrolPointIndex = 1;

        private Vector3 _lastPatrolPosition;
        
        private bool _isFollowingThePlayer = false;
        private bool _isReturningToPatrol = false;
        
        public override void OnStart()
        {
            Debug.Log("Initialised Basic AI!");
            
            TeleportToPoint(patrolPoints[0]);
        }

        public override void UpdateGameObject()
        {
           Patrol();
           HandlePlayerDetection();
        }

        private void HandlePlayerDetection()
        {
            var player = GetVisiblePlayer();

            if (player == null)
            {
                return;
            }

            if (PlayerInRange(player, GetPlayerDetectionRange()))
            {
                _lastPatrolPosition = aiBody.position;
                _isFollowingThePlayer = true;
            }
            else if (_isFollowingThePlayer && !PlayerInRange(player, GetPlayerFollowRange()))
            {
                _isFollowingThePlayer = false;
                _isReturningToPatrol = true;
            }
        }

        private void Patrol()
        {
            if (_isFollowingThePlayer)
            {
                var player = GetVisiblePlayer();
                
                MoveTowards(player.position);
                
                return;
            }

            if (_isReturningToPatrol)
            {
                ReturnToPatrol();
                
                return;
            }
            
            MoveTowards(patrolPoints[_currentPatrolPointIndex]);
            
            if (HasArrivedAtPoint(patrolPoints[_currentPatrolPointIndex]))
            {
                SetNextTargetPatrolPoint();
            }
        }

        private void ReturnToPatrol()
        {
            MoveTowards(_lastPatrolPosition);

            if (HasArrivedAtPoint(_lastPatrolPosition))
            {
                _isReturningToPatrol = false;
            }
        }
        
        private void SetNextTargetPatrolPoint()
        {
            _currentPatrolPointIndex++;

            if (_currentPatrolPointIndex >= patrolPoints.Count)
            {
                _currentPatrolPointIndex = 0;
            }
        }

        protected override float GetPlayerDetectionRange()
        {
            return 5.0f;
        }

        protected override float GetWalkingSpeed()
        {
            return 1.5f;
        }

        public override void OnObjectDetect(Vector3 point)
        {
            
        }
    }
}