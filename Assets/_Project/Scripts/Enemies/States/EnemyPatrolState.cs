using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Enemies.States
{
    public class EnemyPatrolState : IEnemyState
    {
        private static readonly int Running = Animator.StringToHash("running");
        private readonly Enemy _enemy;
        private bool _waiting;

        public EnemyPatrolState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animator.SetBool(Running, true);
        }

        public void Update()
        {
            if (_enemy.EnemyDetection.PlayerInRange() && _enemy.EnemyCombat != null)
            {
                _enemy.EnemyStateMachine.ChangeState(new EnemyAttackState(_enemy));
                return;
            }

            if (_waiting) return;

            Transform target = _enemy.CurrentPatrolPoint;
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, target.position, _enemy.PatrolSpeed * Time.deltaTime);

            if (Vector2.Distance(_enemy.transform.position, target.position) < _enemy.PointReachedThreshold)
            {
                _enemy.Animator.SetBool(Running, false);
                _enemy.StartCoroutine(WaitAtPoint());
            }
        }

        public void Exit() {}

        private IEnumerator WaitAtPoint()
        {
            _waiting = true;
            yield return new WaitForSeconds(_enemy.WaitTimeAtPoint);
            _enemy.SwitchPatrolPoint();
            _enemy.Animator.SetBool(Running, true);
            _waiting = false;
        }
    }
}