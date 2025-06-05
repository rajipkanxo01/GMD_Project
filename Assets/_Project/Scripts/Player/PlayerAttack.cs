using System;
using _Project.Scripts.Audio;
using _Project.Scripts.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public float attackRange = 0.5f;

        public LayerMask enemyLayer;
        public Transform attackPoint;

        [SerializeField] private int attackDamage = 10;

        private Animator _animator;
        private PlayerAudioController _playerAudioController;
        private static readonly int Attack1 = Animator.StringToHash("attack1");
        private static readonly int RunningAttack = Animator.StringToHash("runAttack");

        private void Awake()
        {
            _playerAudioController = GetComponent<PlayerAudioController>();
            _animator = GetComponent<Animator>();
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                _playerAudioController.PlayAttackSound();

                if (stateInfo.IsName("Running") || _animator.GetBool("move"))
                {
                    _animator.SetTrigger(RunningAttack);
                    Debug.Log("Player performed a running attack!");
                }
                else
                {
                    _animator.SetTrigger(Attack1);
                    Debug.Log("Player performed a normal attack!");
                }
            }
        }

        // This function will be called by the Animation Event!
        public void DamageEnemy()
        {
            Debug.Log("Attempting to damage enemy...");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                if (enemyCollider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(attackDamage);
                    Debug.Log($"Damaged Enemy: {enemy.name}");
                }
                else if (enemyCollider.TryGetComponent(out BossController boss))
                {
                    boss.TakeDamage(attackDamage);
                    Debug.Log($"Damaged Boss: {boss.name}");
                }
            }
        }

        // To visualize attack hitbox in the editor
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}