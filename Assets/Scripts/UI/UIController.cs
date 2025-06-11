using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public List<SkillButtonUI> SkillButtons;
    [SerializeField] public UltPercentUI ultPercent;

    public static UIController Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRageUI()
    {
        foreach(SkillButtonUI button in SkillButtons)
        {
            button.rageEffect.SetActive(true);
        }
    }

    public void DestroyRageUI()
    {
        foreach(SkillButtonUI button in SkillButtons)
        {
            button.rageEffect.SetActive(false);
        }
    }

    public void UpdateUltPercent(int val)
    {
        ultPercent.UpdateUI(val);
    }
}
