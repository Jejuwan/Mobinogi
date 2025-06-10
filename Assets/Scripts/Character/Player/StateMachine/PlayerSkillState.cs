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
        PlayerSkillController.Instance.castingIcon.gameObject.SetActive(false);
    }

    IEnumerator WaitUntilCasting()
    {
        // isCasting이 true가 될 때까지 기다림
        yield return new WaitUntil(() => PlayerSkillController.Instance.PlayerCastingReady);

        // isCasting이 true가 된 이후 실행할 코드
       // Start
    }
}
