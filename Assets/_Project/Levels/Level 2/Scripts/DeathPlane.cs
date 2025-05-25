using _Project.Scripts;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Levels.Level_2.Scripts {
  public class DeathPlane : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D other) {
      if (other.CompareTag("Player")) {
        Debug.Log("Player death");
          PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) {
                player.PlayerDie();
            }
        if (UIController.instance != null) {
          UIController.instance.DisplayGameOverScreen(0.1f);
        }
        
      }
    }
  }
}
