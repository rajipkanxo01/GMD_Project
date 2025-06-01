using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Audio
{
    public enum LevelType
    {
        Level1,
        Level2,
        Boss
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Menu")] 
        [SerializeField] private AudioSource mainMenuAudio;

        [Header("Misc")] 
        [SerializeField] private AudioSource levelCompleteAudio;
        
        [Header("Player Audio")] 
        [SerializeField] private AudioSource playerAttackAudio;
        [SerializeField] private AudioSource playerJumpAudio;
        [SerializeField] private AudioSource playerHurtAudio;
        [SerializeField] private AudioSource playerDeathAudio;

        [Header("Enemy Audio")]
        [SerializeField] private AudioSource enemyAttackAudio;
        [SerializeField] private AudioSource enemyHurtAudio;
        [SerializeField] private AudioSource enemyDeathAudio;

        [Header("Boss Audio")]
        [SerializeField] private AudioSource bossAttackAudio;
        [SerializeField] private AudioSource bossHurtAudio;
        [SerializeField] private AudioSource bossDeathAudio;

        [Header("Health Pickup")] 
        [SerializeField] private AudioSource collectHealthAudio;

        [Header("Level Music")] 
        [SerializeField] private AudioSource level1Audio;
        [SerializeField] private AudioSource level2Audio;
        [SerializeField] private AudioSource bossAudio;

        private Dictionary<LevelType, AudioSource> _levelAudioMap;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeLevelAudioMap();
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        private void InitializeLevelAudioMap()
        {
            _levelAudioMap = new Dictionary<LevelType, AudioSource>
            {
                { LevelType.Level1, level1Audio },
                { LevelType.Level2, level2Audio },
                { LevelType.Boss, bossAudio }
            };
        }

        public void StopOtherAudio()
        {
            mainMenuAudio.Stop();

            // Stop misc audio if it's playing
            if (levelCompleteAudio.isPlaying) levelCompleteAudio.Stop();

            // Stop health pickup audio if it's playing
            if (collectHealthAudio.isPlaying) collectHealthAudio.Stop();

            // Stop level audio if it's playing
            if (level1Audio.isPlaying) level1Audio.Stop();
            if (level2Audio.isPlaying) level2Audio.Stop();
            if (bossAudio.isPlaying) bossAudio.Stop();

            // Stop player attack audio if it's playing
            if (playerAttackAudio.isPlaying) playerAttackAudio.Stop();
            if (playerJumpAudio.isPlaying) playerJumpAudio.Stop();
            if (playerHurtAudio.isPlaying) playerHurtAudio.Stop();
            if (playerDeathAudio.isPlaying) playerDeathAudio.Stop();

            //Stop enemy audio if it is playing
            if (enemyAttackAudio.isPlaying) enemyAttackAudio.Stop();
            if (enemyHurtAudio.isPlaying) enemyHurtAudio.Stop();
            if (enemyDeathAudio.isPlaying) enemyDeathAudio.Stop();

            // Stop boss audio if it is playing
            //Stop enemy audio if it is playing
            if (bossAttackAudio.isPlaying) bossAttackAudio.Stop();
            if (bossHurtAudio.isPlaying) bossHurtAudio.Stop();
            if (bossDeathAudio.isPlaying) bossDeathAudio.Stop();


            foreach (var source in _levelAudioMap.Values)
            {
                source.Stop();
            }
        }

        public void PlayMenuAudio()
        {
            StopOtherAudio();
            mainMenuAudio.Play();
        }

        public void StopMenuAudio()
        {
            mainMenuAudio.Stop();
        }


        // Misc Audio Methods
        public void PlayLevelCompleteAudio()
        {
            levelCompleteAudio.Play();
        }


        // Level Audio Methods
        public void PlayLevelAudio(LevelType levelType)
        {
            StopOtherAudio();
            if (_levelAudioMap.TryGetValue(levelType, out AudioSource levelAudio))
            {
                levelAudio.Play();
            }
            else
            {
                Debug.LogWarning($"No audio mapped for {levelType}");
            }
        }

        // Health Pickup Methods
        public void PlayHealthPickupAudio()
        {
            collectHealthAudio.Play();
        }

        // Player Audio Methods
        public void PlayPlayerJumpAudio()
        {
            playerJumpAudio.Play();
        }

        public void PlayPlayerHurtAudio()
        {
            playerHurtAudio.Play();
        }

        public void PlayPlayerDeathAudio()
        {
            playerDeathAudio.Play();
        }

        public void PlayCollectHealthAudio()
        {
            collectHealthAudio.Play();
        }

        public void PlayPlayerAttackSound()
        {
            playerAttackAudio.Play();
        }

        // Enemy Audio Methods

        public void PlayEnemyAttackSound()
        {
            enemyAttackAudio.Play();
        }

        public void PlayEnemyHurtSound()
        {
            enemyHurtAudio.Play();
        }

        public void PlayEnemyDeathSound()
        {
            enemyDeathAudio.Play();
        }

        public void PlayBossAttackSound()
        {
            bossAttackAudio.Play();
        }

        public void PlayBossHurtSound()
        {
            bossHurtAudio.Play();
        }

        public void PlayBossDeathSound()
        {
            bossDeathAudio.Play();
        }

    }
}