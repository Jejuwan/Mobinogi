using UnityEngine;

public class PlayerImpactState : State
{
    private readonly PlayerController player;

    public PlayerImpactState(PlayerController player, StateMachine sm) : base(sm)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.SetAnimTrigger("Impact");
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }
}
