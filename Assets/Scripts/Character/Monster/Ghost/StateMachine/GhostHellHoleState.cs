using UnityEngine;

public class GhostHellHoleState : State
{
    private readonly GhostController monster;

    public GhostHellHoleState(GhostController monster, StateMachine sm) : base(sm)
    {
        this.monster = monster;
    }

    public override void Enter()
    {
        monster.SetAnimTrigger("HellHole");
    }

    public override void Tick(float deltaTime)
    {
        
    }
    public override void Exit()
    {
    }
}
