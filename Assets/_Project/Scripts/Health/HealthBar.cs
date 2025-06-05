using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Health {
    public class HealthBar : MonoBehaviour
    {
        [Header("Health Bar Settings")]
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image healthFill;
        
        void Awake() {
            slider = GetComponent<Slider>();
        }
        
        public void SetHealthLevel(int health) {
            Debug.Log("Setting health level: " + health);
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
    }
}
