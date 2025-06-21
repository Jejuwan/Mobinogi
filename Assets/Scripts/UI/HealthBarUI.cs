using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private ChrController chrController;
    [SerializeField] private float lerpSpeed = 0.05f;

    public void Start()
    {
        if(chrController!=null)
            Bind(chrController.healthComponent);
    }

    public void Update()
    {
        if(healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }

    public void Bind(HealthComponent health)
    {
        health.OnHealthDamaged += UpdateUI;
        health.OnHealthHealed += UpdateUI;
        health.OnDeath += Destroy;
        UpdateUI(health.currentHealth, health.maxHealth);
    }

    private void UpdateUI(int current, int max)
    {
        healthSlider.value = (float)current / max;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}