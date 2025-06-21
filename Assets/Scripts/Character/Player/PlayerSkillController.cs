using System;
using System.Collections;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] public CastingBarUI castingIcon;
    [SerializeField] public GameObject castingBar;
    [SerializeField] public WarriorSkillHandler skillHandler;

    public static PlayerSkillController Instance;
    public ActiveSkill currentActiveSkill { get; set; }
    public int ultPercent { get; set; }
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

        PlayerCastingReady = false;
        castingIcon.gameObject.SetActive(false);
        ultPercent = 0;
    }

    private void Start()
    {
        UIController.Instance.UpdateUltPercent(ultPercent);
    }

    void CancelCast()
    {

    }

    public void TryUseSkill(ActiveSkill skill)
    {
        castingIcon.gameObject.SetActive(true);
        castingIcon.SetSkillIcon(skill.icon);

        skill.raged = skillHandler.rage.IsMax;

        if (!skill.ult)
        {

            if (!skill.raged)
                castingBar.SetActive(skill.casting);
            else
                castingBar.SetActive(false);

            currentActiveSkill = skill;
            skillHandler.TryUseSkill(skill);
        }
        else
        {
            if(ultPercent >= 100)
            {
                currentActiveSkill = skill;
                skillHandler.TryUseSkill(skill);
                PlayerController.Instance.immortal = true;
            }
        }
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

        if (skill.casting && !skill.raged)
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

    public void OnBladeSmash()
    {
        skillHandler.BladeSmash();
    }

    public void OnShieldBash()
    {
        skillHandler.ShieldBash();
    }

    public void OnKick()
    {
        skillHandler.Kick();
    }

    public void OnTaunt()
    {
        skillHandler.Taunt();
    }

    public void OnStartUltimate()
    {
        skillHandler.StartUltimate();
    }

    public void OnBladeImpact()
    {
        skillHandler.BladeImpact();
        skillHandler.rage.GainRage(skillHandler.rage.maxRage);
    }

    public void OnAddRage(int val)
    {
        skillHandler.rage.GainRage(val);
    }
}