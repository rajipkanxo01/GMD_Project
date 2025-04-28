using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Levels.Level_2.Scripts {
  public class DeathPlane : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D other) {
      if (other.CompareTag("Player")) {
        Debug.Log("Player death");
        if (UIController.instance != null) {
          UIController.instance.DisplayGameOverScreen(0.3f);
        }
        
      }
    }
  }
}
