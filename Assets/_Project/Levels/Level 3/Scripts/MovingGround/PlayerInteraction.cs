using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Levels.Level_3.Scripts.MovingGround
{
    public class PlayerInteraction : MonoBehaviour
    {
        private TorchController _currentTorch;

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && _currentTorch != null)
            {
                _currentTorch.ActivateTorch();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out TorchController torch))
            {
                _currentTorch = torch;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out TorchController torch) && _currentTorch == torch)
            {
                _currentTorch = null;
            }
        }
    }
}