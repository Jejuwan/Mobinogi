using UnityEngine;

public class GhostIdleState : State
{
    private readonly MonsterController monster;

    public GhostIdleState(MonsterController mst, StateMachine sm) : base(sm)
    {
        this.monster = mst;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
    }
}
