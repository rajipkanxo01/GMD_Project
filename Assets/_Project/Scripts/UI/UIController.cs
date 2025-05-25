using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameObject gameOverScreen;
        public GameObject gamePauseScreen;

        void Awake()
        {
            instance = this;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameOverScreen.SetActive(false);
        }

        public void DisplayGameOverScreen(float delaySeconds)
        {
            StartCoroutine(ShowGameOverScreenAfterDelay(delaySeconds));
        }

        private IEnumerator ShowGameOverScreenAfterDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            gameOverScreen.SetActive(true);
        }

        public void RestartGame()
        {
            SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            /*int index = SceneManager.GetSceneByName("MainMenu").buildIndex;
            Debug.Log("Main Menu index: " + index);
            SceneLoader.Instance.LoadScene(0);*/

            SceneManager.LoadSceneAsync("MainMenu");

        }

        public void PauseGame()
        {
            gamePauseScreen.SetActive(!gamePauseScreen.activeSelf);
            Time.timeScale = gamePauseScreen.activeSelf ? 0 : 1;
        }

        public void ResumeGame()
        {
            gamePauseScreen.SetActive(!gamePauseScreen.activeSelf);
            Time.timeScale = 1;
        }
    }
}