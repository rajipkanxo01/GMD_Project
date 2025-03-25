using UnityEngine;

namespace _Project.Scripts.Health {
    public class DamageZone : MonoBehaviour {
        private void OnTriggerStay2D(Collider2D collider) {
            PlayerController controller = collider.GetComponent<PlayerController>();
            if (controller != null) {
                PlayerHealthBar playerHealthBar = collider.gameObject.GetComponent<PlayerHealthBar>();
                if (playerHealthBar == null) {
                    playerHealthBar = FindObjectOfType<PlayerHealthBar>();
                }
                if (playerHealthBar != null) {
                    playerHealthBar.TakeObstacleDamage(5);
                }
            }
        }
    }
}
