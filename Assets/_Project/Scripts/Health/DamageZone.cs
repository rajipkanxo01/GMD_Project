using UnityEngine;

namespace _Project.Scripts.Health {
    public class DamageZone : MonoBehaviour {
        private void OnTriggerStay2D(Collider2D otherGameObject) {
            PlayerController controller = otherGameObject.GetComponent<PlayerController>();
            if (controller != null) {
               
            }
        }
    }
}
