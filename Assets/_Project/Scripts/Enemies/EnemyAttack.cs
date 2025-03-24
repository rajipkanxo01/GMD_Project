using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        private enum State
        {
            Patrol,
            Attack
        }

        [Header("Attack Settings")]
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float enemyAttackRange = 1.5f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float colliderDistance = 0.5f;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private LayerMask playerLayer;

        [Header("Patrol Settings")] 
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float pointReachedThreshold = 0.1f;
        [SerializeField] private float waitTimeAtPoint = 1f;

        private State _currentState = State.Patrol;
        private Animator _animator;
        private float _cooldownTimer = Mathf.Infinity;

        private int _currentPatrolIndex;
        private bool _waiting;

        private static readonly int Attack1 = Animator.StringToHash("attack1");
        private static readonly int Run = Animator.StringToHash("running");
        private static readonly int Idle = Animator.StringToHash("Goblin_Idle");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Update cooldown timer
            _cooldownTimer += Time.deltaTime;

            bool playerDetected = PlayerInRange();
            UpdateState(playerDetected);

            switch (_currentState)
            {
                case State.Patrol:
                    HandlePatrol();
                    break;
                case State.Attack:
                    HandleAttack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateState(bool playerDetected)
        {
            // Only switch states if not waiting (prevents bugs mid-wait)
            if (_waiting) return;

            _currentState = playerDetected ? State.Attack : State.Patrol;
        }

        private void HandlePatrol()
        {
            if (patrolPoints.Length < 2) return;

            // Avoid moving during wait period
            if (_waiting) return;

            Transform target = patrolPoints[_currentPatrolIndex];
            transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

            // Start run animation if not already running
            if (!_animator.GetBool(Run))
                _animator.SetBool(Run, true);

            // Check if reached patrol point
            if (Vector2.Distance(transform.position, target.position) < pointReachedThreshold)
            {
                _animator.SetBool(Run, false);
                StartCoroutine(WaitAtPatrolPoint());
            }
        }

        private IEnumerator WaitAtPatrolPoint()
        {
            _waiting = true;

            yield return new WaitForSeconds(waitTimeAtPoint);

            // Flip direction after wait
            _currentPatrolIndex = _currentPatrolIndex == 0 ? 1 : 0;
            FlipSprite(patrolPoints[_currentPatrolIndex]);

            _animator.SetBool(Run, true);
            _waiting = false;
        }

        private void HandleAttack()
        {
            // Ensure we are not running
            if (_animator.GetBool(Run))
            {
                _animator.SetBool(Run, false);
                return;
            }

            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Goblin_Idle") && _cooldownTimer >= attackCooldown)
            {
                _cooldownTimer = 0f;
                _animator.SetTrigger(Attack1);
            }
        }

        private void FlipSprite(Transform target)
        {
            Vector3 scale = transform.localScale;
            scale.x = target.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        private bool PlayerInRange()
        {
            Vector3 origin = boxCollider.bounds.center + transform.right * enemyAttackRange * transform.localScale.x * colliderDistance;
            Vector3 size = new Vector3(boxCollider.bounds.size.x * enemyAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

            RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.left, 0, playerLayer);
            
            return hit.collider;
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null) return;

            Gizmos.color = Color.red;
            Vector3 origin = boxCollider.bounds.center + transform.right * enemyAttackRange * transform.localScale.x * colliderDistance;
            Vector3 size = new Vector3(boxCollider.bounds.size.x * enemyAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

            Gizmos.DrawWireCube(origin, size);
        }

        // Called from animation event
        private void DamagePlayer()
        {
            if (PlayerInRange())
            {
                Debug.Log("Player damaged: " + attackDamage);
            }
        }
    }
}
