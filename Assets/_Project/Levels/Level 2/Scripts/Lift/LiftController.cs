using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Levels.Level_3.Scripts.Lift
{
    public class LiftController : MonoBehaviour
    {
        [SerializeField] private Transform movingBase;
        [SerializeField] private Transform topPoint;
        [SerializeField] private Transform bottomPoint;
        [SerializeField] private float smoothTime = 0.5f;
        [SerializeField] private float waitTimeAtEnd = 2f;

        private bool _isMovingUp = false;
        private bool _isRunning = false;
        private bool _shouldStop = false;
        private Vector3 _velocity = Vector3.zero;

        public void ActivateLift()
        {
            if (!_isRunning)
            {
                Debug.Log("Activated!!");
                _shouldStop = false;
                StartCoroutine(MoveLiftLoop());
            }
        }

        public void DeactivateLift()
        {
            _shouldStop = true;
        }

        private IEnumerator MoveLiftLoop()
        {
            while (true)
            {
                var target = _isMovingUp ? topPoint.position : bottomPoint.position;

                while (Vector3.Distance(movingBase.position, target) > 0.01f)
                {
                    movingBase.position = Vector3.SmoothDamp(movingBase.position, target, ref _velocity, smoothTime);
                    yield return null;
                }

                movingBase.position = target;
                _velocity = Vector3.zero;
                
                yield return new WaitForSeconds(waitTimeAtEnd);
                
                if (_shouldStop && !_isMovingUp)
                    break;

                _isMovingUp = !_isMovingUp;

                // If should stop and we are now going down, return to bottom and stop
                if (_shouldStop && !_isMovingUp)
                    break;
            }
            
            // Ensure it snaps to bottom if stopped
            movingBase.position = bottomPoint.position;
            _velocity = Vector3.zero;
            _isRunning = false;
            _isMovingUp = false;
        }
    }
}