using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float followSpeed = 2f;
        [SerializeField] private float yOffset = 1f;
        [SerializeField] private int pixelsPerUnit = 32;
        
        private Vector3 _velocity = Vector3.zero;

        void LateUpdate()
        {
            var targetPos = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
            var smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, followSpeed * Time.deltaTime);

            // Round to pixel grid
            float unitsPerPixel = 1f / pixelsPerUnit;
            float roundedX = Mathf.Round(smoothPos.x / unitsPerPixel) * unitsPerPixel;
            float roundedY = Mathf.Round(smoothPos.y / unitsPerPixel) * unitsPerPixel;

            transform.position = new Vector3(roundedX, roundedY, smoothPos.z);
        }
    }
}
