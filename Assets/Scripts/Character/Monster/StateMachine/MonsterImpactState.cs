using UnityEngine;

public class MonsterImpactState : State
{
    private readonly MonsterController monster;

    public MonsterImpactState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimTrigger("Impact");
        monster.healthComponent.TakeDamage(10);
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
