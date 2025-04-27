using UnityEngine;

namespace _Project.Levels.Level_3.Scripts.MovingGround
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float moveDistance = 3f;

        private Vector3 _startPos;
        private bool _isActivated = false;
        private float _movementTime = 0f;

        void Start()
        {
            _startPos = transform.position;
        }

        void FixedUpdate()
        {
            if (_isActivated)
            {
                _movementTime += Time.fixedDeltaTime;
                float newPos = Mathf.PingPong(_movementTime * moveSpeed, moveDistance);
                transform.position = new Vector3(_startPos.x, _startPos.y + newPos, _startPos.z);
            }
        }

        public void ActivatePlatform()
        {
            _isActivated = true;
        }

        public void DeactivatePlatform()
        {
            _isActivated = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.transform.SetParent(transform);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.transform.SetParent(null);
            }
        }
    }
}