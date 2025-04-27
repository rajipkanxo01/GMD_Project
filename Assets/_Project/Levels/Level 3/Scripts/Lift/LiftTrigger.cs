using UnityEngine;
using UnityEngine.Events;

namespace _Project.Levels.Level_3.Scripts.Lift
{
    public class LiftTrigger : MonoBehaviour
    {
        [SerializeField] private Transform triggerPoint;
        [SerializeField] private float triggerThreshold = 0.1f;
        [SerializeField] private UnityEvent onFullPushed;

        private Vector3 _startPosition;
        private bool _triggered = false;
        private bool _isBeingPushedByBox = false;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            float distance = Vector2.Distance(transform.position, triggerPoint.position);

            if (!_triggered && _isBeingPushedByBox && distance < triggerThreshold)
            {
                _triggered = true;
                onFullPushed.Invoke();
            }

            _isBeingPushedByBox = false;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.collider.CompareTag("Box"))
            {
                _isBeingPushedByBox = true;
            }
        }
    }
}