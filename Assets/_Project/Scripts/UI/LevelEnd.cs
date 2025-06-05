using _Project.Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI
{
    public class LevelEnd : MonoBehaviour
    {
        private bool isFlagActive;
        [SerializeField] private Animator animator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && isFlagActive == false)
            {
                AudioManager.Instance.PlayLevelCompleteAudio();
                animator.SetBool("isFlagActive", true);
                isFlagActive = true;
            }
        }

        public void LoadNextLevel()
        {
            SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}