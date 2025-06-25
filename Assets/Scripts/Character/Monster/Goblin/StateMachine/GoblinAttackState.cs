using UnityEngine;

public class GoblinAttackState : State
{
    private readonly MonsterController monster;

    public GoblinAttackState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimTrigger("Attack");
    }

    public override void Tick(float deltaTime)
    {
        monster.LookController(PlayerController.Instance.transform, 10f);
    }
    public override void Exit()
    {

    }
}
