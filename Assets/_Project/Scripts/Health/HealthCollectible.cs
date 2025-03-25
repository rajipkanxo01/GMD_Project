using System;
using UnityEngine;

namespace _Project.Scripts.Health {
    public class HealthCollectible : MonoBehaviour {
        private PlayerHealthBar playerHealthBar;
        private void OnTriggerEnter2D(Collider2D other) {
            PlayerController controller = other.gameObject.GetComponent<PlayerController>();
            if (controller != null && playerHealthBar.Health < playerHealthBar.MaxHealth) {
                playerHealthBar.AddPlayerHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
