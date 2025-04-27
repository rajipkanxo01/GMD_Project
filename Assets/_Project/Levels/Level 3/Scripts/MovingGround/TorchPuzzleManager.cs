using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Levels.Level_3.Scripts.MovingGround
{
    public class TorchPuzzleManager : MonoBehaviour
    {
        [SerializeField] private TorchController[] torches;
        [SerializeField] private PlatformMover platform;

        private void Start()
        {
            foreach (var torch in torches)
            {
                torch.OnTorchStateChanged += CheckTorches;
            }
        }

        private void CheckTorches(TorchController changedTorch)
        {
            foreach (var torch in torches)
            {
                if (!torch.IsLit)
                {
                    platform.DeactivatePlatform();
                    return;
                }
            }

            // All torches are lit, move the platforms
            platform.ActivatePlatform();
        }
    }
}