using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float followSpeed = 2f;
        [SerializeField] private float yOffset = 1f;

        private Vector3 _velocity = Vector3.zero;

        void LateUpdate()
        {
            var newPos = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref _velocity, followSpeed * Time.deltaTime);
        }
    }
}