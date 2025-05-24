using UnityEngine;

namespace _Project.Scripts.Audio {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }

        [Header("Menu")]
        [SerializeField] AudioSource mainMenuAudio;

        [Header("SFX")]
        [SerializeField] AudioSource enemyHurtAudio;
        [SerializeField] AudioSource playerHurtAudio;
        [SerializeField] AudioSource playerDeathAudio;
        [SerializeField] AudioSource rockEnemyHitAudio;
        [SerializeField] AudioSource playerJumpAudio;
        [SerializeField] AudioSource collectHealthAudio;
        [SerializeField] AudioSource levelCompleteAudio;
        
        [Header("Level")]
        [SerializeField] AudioSource[] levelAudios;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);  
            }
            else if (Instance != this)           
            {
                Destroy(gameObject);
                return;
            }
        }

        public void StopOtherAudio() {
            mainMenuAudio.Stop();
            enemyHurtAudio.Stop();
            playerHurtAudio.Stop();
            playerDeathAudio.Stop();
            rockEnemyHitAudio.Stop();
            playerJumpAudio.Stop();
            collectHealthAudio.Stop();
            levelCompleteAudio.Stop();

            foreach (AudioSource audio in levelAudios) {
                audio.Stop();
            }
        }
        
        public void PlayMenuAudio() {
            StopOtherAudio();
            mainMenuAudio.Play();
        }

        public void StopMenuAudio()
        {
            mainMenuAudio.Stop();
        }

        public void PlayEnemyHurtAudio() {
            enemyHurtAudio.Play();
        }
    }
}
