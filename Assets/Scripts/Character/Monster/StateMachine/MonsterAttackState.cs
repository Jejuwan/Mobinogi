using UnityEngine;

public class MonsterAttackState : State
{
    private readonly MonsterController monster;

    public MonsterAttackState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimTrigger("Attack");
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
