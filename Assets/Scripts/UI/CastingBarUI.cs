using UnityEngine;
using UnityEngine.UI;

public class CastingBarUI : MonoBehaviour
{
    [SerializeField] private Slider castingSlider;
    [SerializeField] Image skillIcon;

    public void Start()
    {
      
    }

    public void Update()
    {

    }

    public void UpdateUI(float current, float max)
    {
        castingSlider.value = (float)current / max;
    }

    public void SetSkillIcon(Sprite sprite)
    {
        skillIcon.sprite = sprite;
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}