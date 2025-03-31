
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collider) {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if (controller != null && controller.CurrentHealth < controller.MaxHealth) {
                controller.AddPlayerHealth(10);
                Destroy(gameObject);
            }
        }
    }
}
