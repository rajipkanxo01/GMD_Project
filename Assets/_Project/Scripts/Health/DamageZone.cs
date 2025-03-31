using UnityEngine;

namespace _Project.Scripts.Health {
    public class DamageZone : MonoBehaviour {
        private void OnTriggerStay2D(Collider2D collider) {
            Debug.Log("Obstacle");
            PlayerController controller = collider.GetComponent<PlayerController>();
            if (controller != null) {
                controller.TakeObstacleDamage(5);
            }
        }
    }
}
