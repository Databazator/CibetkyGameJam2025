using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetHealth(float healthPercent)
    {
        slider.value = healthPercent;
    }
}
