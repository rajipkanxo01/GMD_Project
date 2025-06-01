using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private HealthBar healthBar;

        private EnemyAudioController EnemyAudioController;
        private int _currentHealth;

        private void Awake()
        {
            EnemyAudioController = GetComponent<EnemyAudioController>();
        }
        private void Start()
        {
            _currentHealth = maxHealth;
            healthBar.SetHealthLevel(_currentHealth);
        }
        
        public void DecreaseEnemyHealth(int damage)
        {
            _currentHealth -= damage;
            EnemyAudioController.PlayHurtSound();
            healthBar.SetHealthLevel(_currentHealth);

            if (_currentHealth <= 0)
            {
                EnemyAudioController.PlayDeathSound();
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}