using System;
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (controller != null && controller.Health < controller.MaxHealth) {
                controller.AddHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
