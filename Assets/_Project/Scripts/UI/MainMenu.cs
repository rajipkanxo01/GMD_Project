using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class MainMenu : MonoBehaviour {
        public void StartGame() {
            SceneManager.LoadScene("Level2");
        }

        public void QuitGame() {
            Application.Quit();
            Debug.Log("Quit");
        }
    }
}
