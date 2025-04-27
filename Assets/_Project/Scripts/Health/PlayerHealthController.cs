using UnityEngine;

namespace _Project.Scripts.Health {
    public class PlayerHealthController : MonoBehaviour {

        //Creating single instance of PlayerHealthController
        public static PlayerHealthController instance;
        
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color normalColor, fadeColor;
        
        private int _currentHealth;
        private bool _isInvincible;
        private float _invincibilityCooldown;
        private PlayerController _player;

        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        void Awake() {
            instance = this;
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            _currentHealth = maxHealth;
            healthBar.SetMaxHealthLevel(maxHealth);
            _player = GetComponent<PlayerController>();
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
                spriteRenderer.color = normalColor;
                _isInvincible = false;
            }
        }

        public void TakeObstacleDamage(int amount)
        {
            if (_isInvincible) return;

            _isInvincible = true;
            _invincibilityCooldown = invincibleTime;
            spriteRenderer.color = fadeColor;
            
            //making player jump a little when hurt
            _player.PlayerHurtByObstacle();
            
            ApplyDamage(amount);
        }

        public void DecreaseHealth(int amount)
        {
            _player.PlayerHurtByEnemy();
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
            if (_currentHealth <= 0) {
                _player._isDead = true;
                _player.PlayerDie();
            }
        }
    }
}
