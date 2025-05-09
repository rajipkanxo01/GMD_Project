using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class UIController : MonoBehaviour {
        public static UIController instance;
        public GameObject gameOverScreen;
        public GameObject gamePauseScreen;

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
            Time.timeScale = 1;
        }

        public void QuitGame() {
            Debug.Log("Quitting game");
            Application.Quit();
        }

        public void MainMenu() {
            Debug.Log("MainMenu");
            SceneManager.LoadSceneAsync("MainMenu");
            Time.timeScale = 1;
        }
        
        public void PauseGame() {
            Debug.Log("PauseGame");
            gamePauseScreen.SetActive(!gamePauseScreen.activeSelf);
            Time.timeScale = gamePauseScreen.activeSelf ? 0 : 1;
        }

        public void ResumeGame() {
            Debug.Log("ResumeGame");
            gamePauseScreen.SetActive(!gamePauseScreen.activeSelf);
            Time.timeScale = 1;
        }
    }
}
