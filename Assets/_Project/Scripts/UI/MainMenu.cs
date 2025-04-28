using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI {
    public class MainMenu : MonoBehaviour {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            
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
