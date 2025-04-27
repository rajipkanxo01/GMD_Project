using System;
using _Project.Scripts.Enemies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int Attack1 = Animator.StringToHash("attack1");
        private static readonly int RunningAttack = Animator.StringToHash("runAttack");
        
        public float attackRange = 0.5f;
        public LayerMask enemyLayer;
        public Transform attackPoint;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

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
            Debug.Log("First Enemy took damage!........................");
            
            // Find all enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(10);
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



