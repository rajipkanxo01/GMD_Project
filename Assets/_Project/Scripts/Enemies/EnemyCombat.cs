using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private int damage = 10;
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
            if (_detection.PlayerInRange())
            {
                Debug.Log("Player damaged: " + damage);
                // TODO: Apply actual damage to player
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