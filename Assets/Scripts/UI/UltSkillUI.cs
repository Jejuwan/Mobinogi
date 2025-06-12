using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UltSkillUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ultPercent;
    [SerializeField] private CastingBarUI barUI;
    [SerializeField] private Image ultImg;
    [SerializeField] private Material grayScaleMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetGrayscale(true);
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

    // UI를 흑백으로 전환
    public void SetGrayscale(bool enabled)
    {
        Material mat = enabled ? grayScaleMat : null;
        ultImg.material = mat;
    }
}
