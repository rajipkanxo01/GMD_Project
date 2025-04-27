using _Project.Scripts;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Levels.Level_3.Scripts.Obstacles.Crusher
{
    public class CrushHitDetector : MonoBehaviour
    {
        [SerializeField] private LogCrusher crusher;
        [SerializeField] private int damage = 5;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.collider.name);
            if (other.collider.CompareTag("Player"))
            {
                Debug.Log("Player hit by crusher!!");

                var playerHealthController = other.collider.GetComponent<PlayerHealthController>();
                playerHealthController.DecreaseHealth(damage);
            }
        }
    }
}