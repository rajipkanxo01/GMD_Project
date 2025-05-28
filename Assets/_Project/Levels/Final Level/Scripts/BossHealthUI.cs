using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHealth(int max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void UpdateHealth(int current)
    {
        slider.value = current;
    }

    private void Awake()
    {
        slider = GetComponent<Slider>(); // fallback if not assigned
    }
}
