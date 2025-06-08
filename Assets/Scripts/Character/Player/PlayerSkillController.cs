using System;
using System.Collections;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] public CastingBarUI castingIcon;
    [SerializeField] public GameObject castingBar;
    public static PlayerSkillController Instance;
    public WarriorSkillHandler skillHandler;
    public ActiveSkill currentActiveSkill { get; set; }

    public bool PlayerCastingReady { get; set; } 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        skillHandler = GetComponent<WarriorSkillHandler>();
        PlayerCastingReady = false;
        castingIcon.gameObject.SetActive(false);
    }

    void CancelCast()
    {

    }

    public void TryUseSkill(ActiveSkill skill)
    {
        castingIcon.gameObject.SetActive(true);
        castingIcon.SetSkillIcon(skill.icon);
        castingBar.SetActive(skill.casting);

        currentActiveSkill = skill;
        skillHandler.TryUseSkill(skill);
    }

    public void StartCasting()
    {
        StartCoroutine(WaitUntilCasting(currentActiveSkill));
    }

    IEnumerator WaitUntilCasting(ActiveSkill skill)
    {
        // isCasting이 true가 될 때까지 기다림
        yield return new WaitUntil(() => PlayerCastingReady);

        PlayerController.Instance.animator.speed = 0f;
        // isCasting이 true가 된 이후 실행할 코드
        StartCoroutine(CastRoutine(skill, PlayerController.Instance.gameObject, CancelCast));
        PlayerCastingReady = false;
    }

    private IEnumerator CastRoutine(ActiveSkill skill, GameObject caster, Action onCancelled)
    {
        //ShowCastUI(skill.icon, skill.castTime);

        if (skill.casting)
        {
            PlayerController.Instance.animator.speed = 0f;
            float elapsed = 0f;
            while (elapsed < skill.castingTime)
            {
                //// 취소 조건 검사 (ex: 피격, 이동 등)
                //if (Input.GetKeyDown(KeyCode.Escape))
                //{
                //    //CancelCast();
                //    onCancelled?.Invoke();
                //    yield break;
                //}

                elapsed += Time.deltaTime;
                castingIcon.UpdateUI(elapsed, skill.castingTime);
                yield return null;
            }
        }
        //HideCastUI();
        PlayerController.Instance.animator.speed = 1f;
  
    }
}