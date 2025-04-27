using UnityEngine;

namespace _Project.Scripts.Health {
    public class DamageZone : MonoBehaviour {
        private void OnTriggerStay2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerHealthController.instance.TakeObstacleDamage(5);
                if (PlayerHealthController.instance.CurrentHealth <= 0) {
                    collider.gameObject.SetActive(false);
                }
            }
        }
    }
}
