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
       
        monster.healthComponent.TakeDamage(30);
        if(monster.healthComponent.IsDead)
        {
            monster.SetState(monster.DeathState);
        }
        else
        {
            monster.SetAnimTrigger("Impact");
        }
        monster.OnDamaged(30);
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
