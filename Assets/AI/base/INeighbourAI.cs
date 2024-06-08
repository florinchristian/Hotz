using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public abstract class INeighbourAI: MonoBehaviour
    {
        public Transform aiBody;
        public Transform playerBody;

        protected Transform GetVisiblePlayer()
        {
            return playerBody;
        }

        private void LookAtPoint(Vector3 point)
        {
            aiBody.transform.LookAt(point);
        }

        protected void MoveTowards(Vector3 point)
        {
            LookAtPoint(point);
            
            var step = GetWalkingSpeed() * Time.deltaTime;
            aiBody.position = Vector3.MoveTowards(aiBody.position, point, step);
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
            return Vector3.Distance(aiBody.position, point) < 0.001f;
        }
        
        protected float GetPlayerFollowRange()
        {
            return 2 * GetPlayerDetectionRange();
        }

        public abstract void OnStart();
        
        public abstract void UpdateGameObject();
        
        protected abstract float GetPlayerDetectionRange();
        protected abstract float GetWalkingSpeed();
    }
}