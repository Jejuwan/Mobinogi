using UnityEngine;

public class GoblinMoveState : State
{
    private readonly MonsterController monster;

    public GoblinMoveState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimBool("isWalking", true);
        if (monster.nameTag != null)
            monster.nameTag.enabled = true;
        if (monster.healthBarUI != null)
            monster.healthBarUI.gameObject.SetActive(true);
    }

    public override void Tick(float deltaTime)
    {
        monster.Move();
    }
    public override void Exit()
    {
        monster.Stop();
        monster.SetAnimBool("isWalking", false);
    }
}
