using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public GameObject gameOverScreen;
        public GameObject gamePauseScreen;

        [SerializeField] private GameObject gameOverFirstButton;
        [SerializeField] private GameObject pauseFirstButton;

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
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
            
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
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void PauseGame()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            
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