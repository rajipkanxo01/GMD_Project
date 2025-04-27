using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class LevelChanger : MonoBehaviour
    {
        [SerializeField] private string _nextLevelName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LoadNextLevel();
            }
        }

        private void LoadNextLevel()
        {
            if (!string.IsNullOrEmpty(_nextLevelName))
            {
                SceneManager.LoadScene(_nextLevelName);
            }
            else
            {
                Debug.LogWarning("Next Level Name not set in LevelChanger script!");
            }
        }
    }
}