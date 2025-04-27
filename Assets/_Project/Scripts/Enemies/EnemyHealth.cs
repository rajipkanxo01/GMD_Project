using _Project.Scripts.Health;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private HealthBar healthBar;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
            healthBar.SetHealthLevel(_currentHealth);
        }
        
        public void DecreaseEnemyHealth(int damage)
        {
            _currentHealth -= damage;
            healthBar.SetHealthLevel(_currentHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}