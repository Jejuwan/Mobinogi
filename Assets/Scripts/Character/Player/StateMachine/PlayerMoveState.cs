using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveState : State
{
    private readonly PlayerController player;

    public PlayerMoveState(PlayerController player, StateMachine sm) : base(sm)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.SetAnimBool("isWalking", true);
    }

    public override void Tick(float deltaTime)
    {
        player.Move();
    }
    public override void Exit()
    {
        player.agent.isStopped = true;
        player.agent.ResetPath();
    }
}
