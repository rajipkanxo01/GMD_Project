using _Project.Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        void Start()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMenuAudio();
            }
            else
            {
                Debug.LogError("AudioManager not found in scene!");
            }
        }

        public void StartGame()
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                AudioManager.Instance.StopMenuAudio();
            }
            else
            {
                Debug.LogError("LoadingScene not found in the scene!");
            }
        }

        public void ShowControls()
        {
            if (ControlsUIManager.Instance == null)
            {
                Debug.LogError("ControlsUIManager not found in the scene!");
                return;
            }
            ControlsUIManager.Instance.ShowControls();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}