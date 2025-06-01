using _Project.Scripts.Audio;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    public void PlayHurtSound()
    {
        AudioManager.Instance.PlayEnemyHurtSound();
    }

    public void PlayDeathSound()
    {
        AudioManager.Instance.PlayEnemyDeathSound();
    }

    public void PlayAttackSound()
    {
        AudioManager.Instance.PlayEnemyAttackSound();
    }
}
