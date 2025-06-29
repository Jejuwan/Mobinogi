using UnityEngine;

public class GhostImpactState : State
{
    private readonly MonsterController monster;

    public GhostImpactState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {

        if (monster.healthComponent.IsDead)
        {
            monster.SetState(monster.DeathState);
        }
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
