using System;
using UnityEngine;

public class RageComponent : MonoBehaviour
{
   public int currentRage { get; set; }
    public int maxRage = 100;

    public event Action<int, int> OnRageChanged;

    public bool IsMax => currentRage >= maxRage;

    private void Awake()
    {
        currentRage = 0;
    }
    public void GainRage(int amount)
    {
        currentRage = Mathf.Min(maxRage, currentRage + amount);
        OnRageChanged?.Invoke(currentRage, maxRage);

        if (IsMax)
            PlayerEffectController.instance.ShowRageEffect();
    }

    public void ConsumeRage(int amount)
    {
        if (IsMax)
            PlayerEffectController.instance.DestroyRageEffect();

        currentRage = 0;
        OnRageChanged?.Invoke(currentRage, maxRage);
    }
}