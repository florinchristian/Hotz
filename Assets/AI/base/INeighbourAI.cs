using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public abstract class INeighbourAI: MonoBehaviour
    {
        public NavMeshAgent agent;
        
        public Transform aiBody;
        public Transform playerBody;
        
        public float viewRadius = 10f;
        public float viewAngle = 90f;
        
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        
        void Start()
        {
            
        }
    
        void Update()
        {
            
        }

        protected virtual Transform GetVisiblePlayer()
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    {
                        return target;
                    }
                }
            }

            return null;
        }

        private void LookAtPoint(Vector3 point)
        {
            aiBody.transform.LookAt(point);
        }

        protected void MoveTowards(Vector3 point)
        {
            // LookAtPoint(point);
            
            // var step = GetWalkingSpeed() * Time.deltaTime;
            // aiBody.position = Vector3.MoveTowards(aiBody.position, point, step);

            agent.SetDestination(point);
        }

        protected void TeleportToPoint(Vector3 point)
        {
            aiBody.position = point;
        }

        protected bool PlayerInRange(Transform playerTransform, float range)
        {
            return Vector3.Distance(playerTransform.position, aiBody.transform.position) <= range;
        }
        
        protected bool HasArrivedAtPoint(Vector3 point)
        {
            return Vector3.Distance(aiBody.position, point) <= 0.1f;
        }
        
        protected float GetPlayerFollowRange()
        {
            return GetPlayerDetectionRange() + 2.5f;
        }

        public abstract void OnStart();
        
        public abstract void UpdateGameObject();
        
        protected abstract float GetPlayerDetectionRange();
        protected abstract float GetWalkingSpeed();
    }
}