using _Project.Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class MainMenu : MonoBehaviour {

        void Start() {
            if (AudioManager.Instance != null) {
                AudioManager.Instance.PlayMenuAudio();
            }
            else {
                Debug.LogError("AudioManager not found in scene!");
            }
        }
        public void StartGame() {
            SceneManager.LoadScene("Level2");
        }

        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
