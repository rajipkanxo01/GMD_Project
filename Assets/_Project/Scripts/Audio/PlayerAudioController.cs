using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class PlayerAudioController : MonoBehaviour
    {
        public void PlayJumpSound()
        {
            AudioManager.Instance.PlayPlayerJumpAudio();
        }

        public void PlayHurtSound()
        {
            AudioManager.Instance.PlayPlayerHurtAudio();
        }

        public void PlayDeathSound()
        {
            AudioManager.Instance.PlayPlayerDeathAudio();
        }

        public void PlayHealthCollectSound()
        {
            AudioManager.Instance.PlayCollectHealthAudio();
        }

        public void PlayAttackSound()
        {
            AudioManager.Instance.PlayPlayerAttackSound();
        }
    }
}