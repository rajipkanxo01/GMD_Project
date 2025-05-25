using UnityEngine;

namespace _Project.Scripts.Audio
{
    public class PickupAudioController : MonoBehaviour
    {
        public void PlayPickupSound()
        {
            AudioManager.Instance.PlayHealthPickupAudio();
        }
    }
}