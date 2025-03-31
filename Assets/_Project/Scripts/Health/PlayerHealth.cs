using UnityEngine;

namespace _Project.Scripts.Health {
    public class PlayerHealth : MonoBehaviour {
        [Header("Health")]
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        [SerializeField] private PlayerHealthBar playerHealthBar;
        
        private bool isInvincible;
        private float cooldownTime;
        void Start() {
            currentHealth = maxHealth;
            if (playerHealthBar == null) {
                playerHealthBar = FindObjectOfType<PlayerHealthBar>();
            }
            playerHealthBar.SetMaxHealthLevel(maxHealth);
        }
            
        void Update() {
            if (isInvincible) {
                cooldownTime -= Time.deltaTime;
                if (cooldownTime <= 0) {
                    isInvincible = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                Debug.Log("Q");
                DecreasePlayerHealth(10);
            }
        }
            
        //Decreases health. Also has cooldown time in scenarios where damage zone can continuously decrease player's health
        public void TakeObstacleDamage(int amount) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            cooldownTime = invincibleTime;
                
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            playerHealthBar.SetHealthLevel(currentHealth);
        }

        //Decrease health
        public void DecreasePlayerHealth(int amount) {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            Debug.Log(currentHealth);
            playerHealthBar.SetHealthLevel(currentHealth);
        }
            
        //Adds health
        public void AddPlayerHealth(int amount) {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            playerHealthBar.SetHealthLevel(currentHealth);
        }
        
        public int Health => currentHealth;

        public int MaxHealth => maxHealth;
    }
}
