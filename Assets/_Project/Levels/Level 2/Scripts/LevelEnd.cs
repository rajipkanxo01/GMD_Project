using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Levels.Level_2.Scripts
{
    public class LevelEnd : MonoBehaviour
    {
        private bool isFlagActive;
        [SerializeField] private Animator animator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && isFlagActive == false)
            {
                Debug.Log(other.name);
                animator.SetBool("isFlagActive", true);
                isFlagActive = true;
            }
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Level3");
        }
    }
}