using UnityEngine;

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

        if (!InputManager.instance.IsMoving)
        {
            stateMachine.SetState(player.IdleState);
        }
    }
    public override void Exit()
    {
    }
}
