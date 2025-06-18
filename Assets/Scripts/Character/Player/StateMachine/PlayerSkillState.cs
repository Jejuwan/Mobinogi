using System.Threading;
using System;
using System.Collections;
using UnityEngine;

public class PlayerSkillState : State
{
    private readonly PlayerController player;
    public PlayerSkillState(PlayerController player, StateMachine sm) : base(sm)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.SetAnimTrigger(PlayerSkillController.Instance.skillHandler.currentSkillName);
        PlayerSkillController.Instance.StartCasting();
        player.agent.isStopped = true;
    }

    public override void Tick(float deltaTime)
    {
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
        PlayerSkillController.Instance.castingIcon.gameObject.SetActive(false);
        player.agent.isStopped = false;
    }

    IEnumerator WaitUntilCasting()
    {
        // isCasting�� true�� �� ������ ��ٸ�
        yield return new WaitUntil(() => PlayerSkillController.Instance.PlayerCastingReady);

        // isCasting�� true�� �� ���� ������ �ڵ�
       // Start
    }
}
