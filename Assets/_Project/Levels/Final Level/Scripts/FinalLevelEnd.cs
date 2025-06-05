using UnityEngine;
using UnityEngine.SceneManagement;
using _Project.Scripts.Audio;
public class FinalLevelEnd : MonoBehaviour
{
    [SerializeField] private float delayBeforeMenu = 3f;

    public void HandleBossDefeat()
    {
        AudioManager.Instance.PlayLevelCompleteAudio();
        Invoke(nameof(GoToMainMenu), delayBeforeMenu);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}