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
            PlayerSkillController.Instance.ultPercent = Mathf.Min(PlayerSkillController.Instance.ultPercent, 100);
            UIController.Instance.UpdateUltPercent(PlayerSkillController.Instance.ultPercent);
        }
        else
        {
            PlayerSkillController.Instance.ultPercent = 0;
            UIController.Instance.UpdateUltPercent(PlayerSkillController.Instance.ultPercent);
        }
    }
    public void BladeSmash()
    {
        if (rage.IsMax)
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.swordTrailRagePrefab, PlayerEffectController.Instance.swordTrailRageEffect,
                PlayerController.Instance.transform.position + Vector3.up, Quaternion.Euler(90f, 0, 0), 2f);
        }
        else
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.swordTrailPrefab, PlayerEffectController.Instance.swordTrailEffect,
                PlayerController.Instance.transform.position + Vector3.up, Quaternion.Euler(90f, 0, 0), 1f);
        }
        UseSkill();
    }

    public void ShieldBash()
    {
        if (rage.IsMax)
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.shieldBashRagePrefab, PlayerEffectController.Instance.shieldBashRageEffect,
               PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        else
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.shieldBashPrefab, PlayerEffectController.Instance.shieldBashEffect,
               PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        UseSkill();
    }

    public void Kick()
    {
        if (rage.IsMax)
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.kickRagePrefab, PlayerEffectController.Instance.kickRageEffect,
                PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);

        }
        else
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.kickPrefab, PlayerEffectController.Instance.kickEffect,
                PlayerController.Instance.transform.position + Vector3.up + PlayerController.Instance.transform.forward, Quaternion.identity, 1f);
        }
        UseSkill();
    }

    public void Taunt()
    {
        if (rage.IsMax)
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.TauntRagePrefab, PlayerEffectController.Instance.tauntRageEffect,
               PlayerController.Instance.transform.position + Vector3.up, Quaternion.identity, 1f);
        }
        else
        {
            PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.TauntPrefab, PlayerEffectController.Instance.tauntEffect,
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
        PlayerEffectController.Instance.ShowAndDestroyEffect(PlayerEffectController.Instance.bladeImpactPrefab, PlayerEffectController.Instance.bladeImpactEffect,
              PlayerController.Instance.transform.position + vec * 2f, Quaternion.identity, 3f);
        AudioController.Instance.PlaySound(PlayerSkillController.Instance.currentActiveSkill.sound);

    }
}
