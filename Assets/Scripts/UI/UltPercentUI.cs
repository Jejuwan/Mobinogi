using TMPro;
using UnityEngine;

public class UltPercentUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ultPercent;
    [SerializeField] private CastingBarUI barUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(int val)
    {
        ultPercent.text = val.ToString();
        barUI.UpdateUI(val, 100);
    }
}
