using _Project.Scripts.Audio;
using UnityEngine;

public class BossAudioController : MonoBehaviour
{
    public void PlayHurtSound()
    {
        AudioManager.Instance.PlayBossHurtSound();
    }

    public void PlayDeathSound()
    {
        AudioManager.Instance.PlayBossDeathSound();
    }

    public void PlayAttackSound()
    {
        AudioManager.Instance.PlayBossAttackSound();
    }
}
