using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ActiveSkill")]
public class ActiveSkill : SkillBase
{

    public float cooldown;
    public bool casting;
    public float castingTime;
    public int baseDamage;

    public override void Activate(GameObject user, GameObject target = null)
    {   
        PlayerController controller = user.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.SetState(controller.SkillState);
        }
    }
}