using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class UIController : MonoBehaviour {
        public static UIController instance;
        public GameObject gameOverScreen;

        void Awake() {
            instance = this;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            gameOverScreen.SetActive(false);    
        }
        

        public void DisplayGameOverScreen(float delaySeconds) {
            StartCoroutine(ShowGameOverScreenAfterDelay(delaySeconds));
        }

        private IEnumerator ShowGameOverScreenAfterDelay(float seconds) {
            yield return new WaitForSeconds(seconds);
            gameOverScreen.SetActive(true);
        }
        public void RestartGame() {
            Debug.Log("Restarting game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
