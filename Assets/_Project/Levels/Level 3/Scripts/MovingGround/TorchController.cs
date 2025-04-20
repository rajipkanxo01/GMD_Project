using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

namespace _Project.Levels.Level_3.Scripts.MovingGround
{
    public class TorchController : MonoBehaviour
    {
        [SerializeField] private GameObject torchLight;
        [SerializeField] private Light2D torchFlame;
        [SerializeField] private float cooldown = 1f;
        
        public event System.Action<TorchController> OnTorchStateChanged;

        private Animator _animator;
        public bool IsLit { get; private set; }
        private bool _isOnCooldown = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            torchFlame.intensity = 0;
            IsLit = false;
        }

        public void ActivateTorch()
        {
            if (IsLit || _isOnCooldown) return;

            IsLit = true;
            torchFlame.intensity = 2;
            _animator.SetBool("isLit", true);
            
            OnTorchStateChanged!.Invoke(this);

            StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            _isOnCooldown = true;

            float t = 0f;
            float startIntensity = torchFlame.intensity;

            while (t < cooldown)
            {
                t += Time.deltaTime;
                torchFlame.intensity = Mathf.Lerp(startIntensity, 0, t / cooldown);
                yield return null;
            }

            IsLit = false;
            _animator.SetBool("isLit", false);
            
            OnTorchStateChanged!.Invoke(this);
            
            _isOnCooldown = false;
        }
    }
}