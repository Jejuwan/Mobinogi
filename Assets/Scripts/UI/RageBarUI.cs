using UnityEngine;
using UnityEngine.UI;

public class RageBarUI : MonoBehaviour
{
    [SerializeField] private Slider rageSlider;
    [SerializeField] private Slider easeRageSlider;
    [SerializeField] private float lerpSpeed = 0.05f;

    public void Start()
    {
       var warriorSkillHandler = PlayerSkillController.Instance.skillHandler as WarriorSkillHandler;
        if(warriorSkillHandler != null )
        {
            Bind(warriorSkillHandler.rage);
        }
    }

    public void Update()
    {
        if(rageSlider.value != easeRageSlider.value)
        {
            easeRageSlider.value = Mathf.Lerp(easeRageSlider.value, rageSlider.value, lerpSpeed);
        }
    }

    public void Bind(RageComponent rage)
    {
        rage.OnRageChanged += UpdateUI;
        UpdateUI(rage.currentRage, rage.maxRage);
    }

    private void UpdateUI(int current, int max)
    {
        rageSlider.value = (float)current / max;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}