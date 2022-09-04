using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Slider slider;
    private void Start()
    {
        slider.maxValue = health.MaxHp;
    }
    private void Update()
    {
        slider.value = health.Hp;
    }
}
