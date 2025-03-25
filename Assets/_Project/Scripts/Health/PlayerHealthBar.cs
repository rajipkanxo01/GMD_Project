using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Health {
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image healthFill;
        
        [Header("Health")]
        [SerializeField] private float invincibleTime = 2f;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth;
        private bool isInvincible;
        private float cooldownTime;
        void Awake() {
            slider = GetComponent<Slider>();
        }
        
        void Start() {
            currentHealth = maxHealth;
            SetMaxHealthLevel(maxHealth);
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
            SetHealthLevel(currentHealth);
        }
        
        //Decrease health
        public void DecreasePlayerHealth(int amount) {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            Debug.Log(currentHealth);
            SetHealthLevel(currentHealth);
        }
        
        //Adds health
        public void AddPlayerHealth(int amount) {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            SetHealthLevel(currentHealth);
        }

        public void SetHealthLevel(int health) {
            slider.value = health;
            healthFill.fillAmount = health / 100f;
            healthFill.color = gradient.Evaluate(health / 100f);
        }

        public void SetMaxHealthLevel(int health) {
            slider.maxValue = health;
            slider.value = health;
            healthFill.fillAmount = health / 100f;
            healthFill.color = gradient.Evaluate(health / 100f);
        }
        
        public int Health => currentHealth;
        
        public int MaxHealth => maxHealth;
    }
}
