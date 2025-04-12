using _Project.Scripts.Enemies.States;

namespace _Project.Scripts.Enemies
{
    public class EnemyStateMachine
    {
        private IEnemyState _enemyCurrentState;

        public void ChangeState(IEnemyState newState)
        {
            _enemyCurrentState?.Exit();
            _enemyCurrentState = newState;
            _enemyCurrentState.Enter();
        }
        
        public void Update()
        {
            _enemyCurrentState?.Update();
        }
    }
}