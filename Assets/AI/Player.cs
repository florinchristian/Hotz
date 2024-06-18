using System.Collections;
using AI;
using UnityEngine;

namespace AI
{
    public class Player : MonoBehaviour
    {
        private bool _isVisible = true;
    
        void Start()
        {
        
        }
    
        void Update()
        {
        
        }

        public bool IsVisible()
        {
            return _isVisible;
        }

        public void SetInvisible(float duration)
        {
            _isVisible = false;

            var coroutine = InvisibilityTimeout(duration);
        
            StartCoroutine(coroutine);
        }
        public void CloseLights(float duration)
        {
            _isVisible = false;

            var coroutine = NightVisionTimeOut(duration);
        
            StartCoroutine(coroutine);
        }

        private IEnumerator InvisibilityTimeout(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isVisible = true;
        }
        
        private IEnumerator NightVisionTimeOut(float duration)
        {
            yield return new WaitForSeconds(duration);
            _isVisible = true;
            NightVision.instance.GoToLightMode();
        }
    }
}