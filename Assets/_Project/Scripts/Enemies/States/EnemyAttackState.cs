using System;
using UnityEngine;

namespace _Project.Scripts.Enemies.States
{
    public class EnemyAttackState : IEnemyState
    {
        private static readonly int Running = Animator.StringToHash("running");
        private readonly Enemy _enemy;

        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log("Entered Attack State");

            // Stop movement animation
            _enemy.Animator.SetBool(Running, false);

            // Reset cooldown timer when entering attack state
            _enemy.EnemyCombat.ResetCooldown();

            // Immediately attack once
            _enemy.EnemyCombat.TryAttack();
        }

        public void Update()
        {
            if (!_enemy.EnemyDetection.PlayerInRange())
            {
                _enemy.EnemyStateMachine.ChangeState(new EnemyPatrolState(_enemy));
                return;
            }

            _enemy.EnemyCombat.TryAttack(); 
        }

        public void Exit()
        {
        }
    }
}