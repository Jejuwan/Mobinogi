using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill")]
public class ActiveSkill : SkillBase
{
    public float cooldown;

    public override void Activate(GameObject user, GameObject target = null)
    {
        var animator = user.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(skillName); // 애니메이션 트리거는 스킬명과 동일하게 세팅
        }
        
        PlayerController controller = user.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.SetState(controller.SkillState);
        }
        Debug.Log($"[Activate] {skillName} 스킬 발동");
    }
}