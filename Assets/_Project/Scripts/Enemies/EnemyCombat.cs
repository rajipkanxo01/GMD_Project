using System;
using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private int damage = 5;
        [SerializeField] private float attackCooldown = 2f;

        private float _cooldownTimer = Mathf.Infinity;
        private EnemyDetection _detection;
        private Animator _animator;

        private static readonly int Attack1 = Animator.StringToHash("attack1");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _detection = GetComponent<EnemyDetection>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
        }

        public void TryAttack()
        {
            if (!IsCooldownComplete()) return;
            _animator.SetTrigger(Attack1);
            ResetCooldown();
        }


        public void DamagePlayer()
        {
            Vector3 origin = _detection.GetBoxOrigin();
            Vector3 size = _detection.GetBoxSize();

            Collider2D hit = Physics2D.OverlapBox(origin, size, 0, _detection.PlayerLayer);

            if (hit != null)
            {
                var healthController = hit.GetComponent<PlayerHealthController>();
                if (healthController != null)
                {
                    healthController.DecreaseHealth(damage);
                    Debug.Log("Player damaged: " + damage);
                }
            }
        }
        
        public void ResetCooldown()
        {
            _cooldownTimer = 0f;
        }

        public bool IsCooldownComplete()
        {
            return _cooldownTimer >= attackCooldown;
        }
        
        public void ChangeToIdle()
        {
            _animator.SetTrigger("idle");
        }

    }
}