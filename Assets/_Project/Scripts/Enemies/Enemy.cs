using System;
using _Project.Scripts.Enemies.States;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        // Internal components
        internal EnemyStateMachine EnemyStateMachine { get; private set; }
        internal EnemyHealth EnemyHealth { get; private set; }
        internal EnemyDetection EnemyDetection { get; private set; }
        internal EnemyCombat EnemyCombat { get; private set; }
        internal Animator Animator { get; private set; }

        [Header("Patrol Settings")] [SerializeField]
        private Transform[] patrolPoints;

        [SerializeField] private float patrolSpeed = 2f;
        [SerializeField] private float pointReachedThreshold = 0.1f;
        [SerializeField] private float waitTimeAtPoint = 1f;

        // Exposed public read-only properties
        public Transform[] PatrolPoints => patrolPoints;
        public float PatrolSpeed => patrolSpeed;
        public float PointReachedThreshold => pointReachedThreshold;
        public float WaitTimeAtPoint => waitTimeAtPoint;
        public Transform CurrentPatrolPoint => patrolPoints[_currentPatrolIndex];

        private int _currentPatrolIndex;

        private void Awake()
        {
            // Cache required components
            EnemyStateMachine = new EnemyStateMachine();
            EnemyHealth = GetComponent<EnemyHealth>();
            EnemyDetection = GetComponent<EnemyDetection>();
            EnemyCombat = GetComponent<EnemyCombat>();
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EnemyStateMachine.ChangeState(new EnemyPatrolState(this));
        }

        private void Update()
        {
            EnemyStateMachine.Update();
        }

        public void SwitchPatrolPoint()
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
            FlipSprite(patrolPoints[_currentPatrolIndex]);
        }

        private void FlipSprite(Transform target)
        {
            Vector3 scale = transform.localScale;
            scale.x = target.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}