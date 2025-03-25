
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collider) {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if (controller != null) {
                PlayerHealthBar playerHealthBar = collider.gameObject.GetComponent<PlayerHealthBar>();
                if (playerHealthBar == null) {
                    playerHealthBar = FindObjectOfType<PlayerHealthBar>();
                }
                if (playerHealthBar != null && playerHealthBar.Health < playerHealthBar.MaxHealth) {
                    playerHealthBar.AddPlayerHealth(10);
                    Destroy(gameObject);
                }
            }
        }
    }
}
