
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerHealthController.instance.AddPlayerHealth(10);
                Destroy(gameObject);
            }
        }
    }
}
