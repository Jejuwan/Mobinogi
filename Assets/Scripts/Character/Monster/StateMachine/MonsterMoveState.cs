using UnityEngine;

public class MonsterMoveState : State
{
    private readonly MonsterController monster;

    public MonsterMoveState(MonsterController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimBool("isWalking", true);
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
