using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] public GameObject rageEffect;
    [SerializeField] private ActiveSkill skill; // ����� ��ų ������
    [SerializeField] private PlayerController playerController;

    public RectTransform rectTransform { get; set; }
    private Button button;
    private bool isCoolingDown = false;
    private float rageEffectSize = 500f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSkillButton);

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0f;
    }

    private void OnClickSkillButton()
    {
        if (isCoolingDown || skill == null)
            return;

        if (PlayerController.Instance.stateMachine.currentState != PlayerController.Instance.AttackState)
            return;

        // ��ų ����
        PlayerSkillController.Instance.TryUseSkill(skill);

        // ��ٿ� ����
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        isCoolingDown = true;

        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / cooldownTime);
            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = 1f - t;
            yield return null;
        }

        if (cooldownOverlay != null)
            cooldownOverlay.fillAmount = 0f;

        isCoolingDown = false;
    }
}
