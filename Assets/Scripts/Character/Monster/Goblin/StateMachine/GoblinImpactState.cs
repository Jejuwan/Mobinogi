using UnityEngine;

public class GoblinImpactState : State
{
    private readonly MonsterController monster;

    public GoblinImpactState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {

        if (monster.healthComponent.IsDead)
        {
            monster.SetState(monster.DeathState);
        }
        else
        {
            monster.SetAnimTrigger("Impact");
        }
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
