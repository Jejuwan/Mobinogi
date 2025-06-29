using UnityEngine;

public class GhostDeathState : State
{
    private readonly MonsterController monster;

    public GhostDeathState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimTrigger("Death");
        if (PlayerController.Instance.TargetMonster == monster)
        {
            PlayerController.Instance.TargetMonster = null;
        }
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
