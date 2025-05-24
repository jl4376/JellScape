using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    public void UpdateHealthBar(float health, float maxHealth)
    {
        if (slider != null)
        {
            slider.value = health / maxHealth;
        }
    }
}
