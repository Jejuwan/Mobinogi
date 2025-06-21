using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public int maxHealth = 300;
    public int currentHealth { get; set; }
    public bool IsDead;

    public event Action<int, int> OnHealthDamaged;
    public event Action<int, int> OnHealthHealed;
    public event Action OnDeath;

    private void Awake()
    {
        IsDead = false;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        OnHealthHealed += (currentHealth, maxHealth) =>
        {
            EffectManager eff = EffectManager.Instance;
            eff.ShowAndDestroyEffect(eff.healPrefab, eff.healEffect, PlayerController.Instance.transform.position, Quaternion.identity, .5f);
        };
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthDamaged?.Invoke(currentHealth, maxHealth);
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
        OnHealthHealed?.Invoke(currentHealth, maxHealth);
    }
}
