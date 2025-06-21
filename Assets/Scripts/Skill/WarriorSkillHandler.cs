using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WarriorSkillHandler : MonoBehaviour, IJobSkillHandler
{
    [SerializeField] private GameObject user;
    public RageComponent rage;
    public string currentSkillName { get; set; }

    private void Awake()
    {
        rage = GetComponent<RageComponent>();
    }

    public void TryUseSkill(SkillBase skill)
    {
        currentSkillName = skill.skillName;
        skill.Activate(user);
    }

    private void UseSkill()
    {
        if (rage.IsMax)
        {
            rage.ConsumeRage();
            AudioController.Instance.PlaySound(PlayerSkillController.Instance.currentActiveSkill.rageSound);
        }
        else
        {
            rage.GainRage(PlayerSkillController.Instance.currentActiveSkill.gainRage);
            AudioController.Instance.PlaySound(PlayerSkillController.Instance.currentActiveSkill.sound);
        }
        if(!PlayerSkillController.Instance.currentActiveSkill.ult)
        {
            PlayerSkillController.Instance.ultPercent += PlayerSkillController.Instance.currentActiveSkill.gainUlt;
            if(PlayerSkillController.Instance.ultPercent >= 100)
                UIController.Instance.UpdateGrayScale(false);
            PlayerSkillController.Instance.ultPercent = Mathf.Min(PlayerSkillController.Instance.ultPercent, 100);
            UIController.Instance.UpdateUltPercent(PlayerSkillController.Instance.ultPercent);
        }
    }
    public void BladeSmash()
    {
        if (rage.IsMax)
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.swordTrailRagePrefab, EffectManager.Instance.swordTrailRageEffect,
                PlayerController.Instance.transform.position + Vector3.up, Quaternion.Euler(90f, 0, 0), 2f);
        }
        else
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.swordTrailPrefab, EffectManager.Instance.swordTrailEffect,
                PlayerController.Instance.transform.position + Vector3.up, Quaternion.Euler(90f, 0, 0), 1f);
        }
        UseSkill();
    }

    public void ShieldBash()
    {
        if (rage.IsMax)
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.shieldBashRagePrefab, EffectManager.Instance.shieldBashRageEffect,
               PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        else
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.shieldBashPrefab, EffectManager.Instance.shieldBashEffect,
               PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        UseSkill();
    }

    public void Kick()
    {
        if (rage.IsMax)
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.kickRagePrefab, EffectManager.Instance.kickRageEffect,
                PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);

        }
        else
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.kickPrefab, EffectManager.Instance.kickEffect,
                PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        UseSkill();
    }

    public void Taunt()
    {
        if (rage.IsMax)
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.TauntRagePrefab, EffectManager.Instance.tauntRageEffect,
               PlayerController.Instance.transform.position + Vector3.up, Quaternion.identity, 1f);
        }
        else
        {
            EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.TauntPrefab, EffectManager.Instance.tauntEffect,
               PlayerController.Instance.transform.position + Vector3.up, Quaternion.identity, 1f);
        }
        UseSkill();
    }

    public void StartUltimate()
    {
        CameraManager.instance.TriggerUltimate();
    }

    public void BladeImpact()
    {
        Quaternion rotation = Quaternion.AngleAxis(-30f, Vector3.up);
        Vector3 vec = rotation * PlayerController.Instance.transform.forward;
        EffectManager.Instance.ShowAndDestroyEffect(EffectManager.Instance.bladeImpactPrefab, EffectManager.Instance.bladeImpactEffect,
              PlayerController.Instance.transform.position + vec * 2f, Quaternion.identity, 3f);
        AudioController.Instance.PlaySound(PlayerSkillController.Instance.currentActiveSkill.sound);
       
        PlayerSkillController.Instance.ultPercent = 0;
        UIController.Instance.UpdateUltPercent(PlayerSkillController.Instance.ultPercent);
        UIController.Instance.UpdateGrayScale(true);
    }
}
