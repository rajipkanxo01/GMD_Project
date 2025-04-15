using UnityEngine;

namespace _Project.Scripts.Health {
    public class PlayerHealthController : MonoBehaviour {

        //Creating single instance of PlayerHealthController
        public static PlayerHealthController instance;
        
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private HealthBar healthBar;
        
        private int _currentHealth;
        private bool _isInvincible;
        private float _invincibilityCooldown;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        void Awake() {
            instance = this;
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            _currentHealth = maxHealth;
            healthBar.SetMaxHealthLevel(maxHealth);
        }

        // Update is called once per frame
        void Update() {
            HandleInvincibility();
        }
        
        private void HandleInvincibility()
        {
            if (!_isInvincible) return;

            _invincibilityCooldown -= Time.deltaTime;
            if (_invincibilityCooldown <= 0)
            {
                _isInvincible = false;
            }
        }

        public void TakeObstacleDamage(int amount)
        {
            if (_isInvincible) return;

            _isInvincible = true;
            _invincibilityCooldown = invincibleTime;

            ApplyDamage(amount);
        }

        public void DecreaseHealth(int amount)
        {
            ApplyDamage(amount);
        }

        public void AddPlayerHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            healthBar.SetHealthLevel(_currentHealth);
        }

        private void ApplyDamage(int amount)
        {
            Debug.Log("Applying Damage " + amount);
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, maxHealth);
            healthBar.SetHealthLevel(_currentHealth);
        }
    }
}
