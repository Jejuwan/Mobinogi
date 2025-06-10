using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public static UIController Instance;
    public List<SkillButtonUI> SkillButtons;

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
}
