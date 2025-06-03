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
            animator.SetTrigger(skillName); // �ִϸ��̼� Ʈ���Ŵ� ��ų��� �����ϰ� ����
        }
        
        PlayerController controller = user.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.SetState(controller.SkillState);
        }
        Debug.Log($"[Activate] {skillName} ��ų �ߵ�");
    }
}