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
        {
           PlayerEffectController.Instance.ShowEffect(PlayerEffectController.Instance.rageEffectPrefab, PlayerEffectController.Instance.rageEffect,
            PlayerController.Instance.transform.position, Quaternion.identity, .7f);
            UIController.Instance.ShowRageUI();
        }
    }

    public void ConsumeRage()
    {
        PlayerEffectController.Instance.DestroyEffect(PlayerEffectController.Instance.rageEffect);
        UIController.Instance.DestroyRageUI();
        PlayerSkillController.Instance.currentActiveSkill.raged = false;
        currentRage = 0;
        OnRageChanged?.Invoke(currentRage, maxRage);
    }
}