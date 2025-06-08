using System.Threading;
using UnityEngine;

public class PlayerAttackState : State
{
    private readonly PlayerController player;

    public PlayerAttackState(PlayerController player, StateMachine sm) : base(sm)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.SetAnimTrigger("Attack");
        PlayerSkillController.Instance.currentActiveSkill = null;
    }

    public override void Tick(float deltaTime)
    {
        ChrController controller = player.TargetMonster.GetComponent<ChrController>();
        if (controller == null ||  controller.healthComponent.IsDead)
            return;

        if (player.TargetMonster != null)
        {
            Vector3 direction = (player.TargetMonster.transform.position - player.transform.position).normalized;

            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Quaternion rollRot = Quaternion.AngleAxis(30f, Vector3.up);
                lookRotation *= rollRot;

                player.transform.rotation = Quaternion.Slerp(
                    player.transform.rotation,
                    lookRotation,
                    10f * Time.deltaTime
                );
            }
        }
    }
    public override void Exit()
    {
        player.OnDisableCollider(0);
    }
}
