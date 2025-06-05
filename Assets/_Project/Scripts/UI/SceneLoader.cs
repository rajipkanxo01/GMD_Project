using System.Collections;
using _Project.Scripts.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Image loadingBarFill;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(int sceneId)
        {
            StartCoroutine(LoadSceneAsync(sceneId));
        }

        IEnumerator LoadSceneAsync(int sceneId)
        {
            loadingScreen.SetActive(true);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
            
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingBarFill.fillAmount = progress;
                yield return null;
            }
            
            PlayLevelMusic(sceneId);
            loadingScreen.SetActive(false);
        }
        
        private void PlayLevelMusic(int sceneId)
        {
            switch (sceneId)
            {
                case 1:
                    AudioManager.Instance.PlayLevelAudio(LevelType.Level1);
                    break;
                case 2:
                    AudioManager.Instance.PlayLevelAudio(LevelType.Level2);
                    break;
                case 3:
                    AudioManager.Instance.PlayLevelAudio(LevelType.Boss);
                    break;
                default:
                    Debug.LogWarning("No music assigned for this scene.");
                    break;
            }
        }

    }
}