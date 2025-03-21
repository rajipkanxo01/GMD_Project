using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float enemyAttackRange;
        [SerializeField] private int attackDamage;
        [SerializeField] private float colliderDistance;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private LayerMask playerLayer;

        private float _cooldownTimer = Mathf.Infinity;
        private Animator _animator;

        private static readonly int Attack1 = Animator.StringToHash("attack1");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;

            if (PlayerInRange())
            {
                if (_cooldownTimer >= attackCooldown)
                {
                    _cooldownTimer = 0;
                    _animator.SetTrigger(Attack1);
                }
            }
        }

        private bool PlayerInRange()
        {
            RaycastHit2D hit = 
                Physics2D.BoxCast(boxCollider.bounds.center + transform.right * enemyAttackRange * transform.localScale.x * colliderDistance,
                    new Vector3(boxCollider.bounds.size.x * enemyAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                    0, Vector2.left, 0, playerLayer);

            if (hit.collider)
            {
                Debug.LogWarning("Hit");
            }
            else
            {
                Debug.Log("Not Hit!");
            }
            
            return hit.collider;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * enemyAttackRange * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * enemyAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }

        private void DamagePlayer()
        {
            if (PlayerInRange())
            {
                // Damage player
            }
        }
    }
}