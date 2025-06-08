using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHealth { get; set; } = 10000;
    public int currentHealth { get; set; }
    public bool IsDead;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        IsDead = currentHealth <= 0;

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
