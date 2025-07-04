using UnityEngine;

public class PlayerIdleState : State
{
    private readonly PlayerController player;

    public PlayerIdleState(PlayerController player, StateMachine sm) : base(sm)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.SetAnimBool("isWalking", false);
        player.animator.Play("Idle");
    }

    public override void Tick(float deltaTime)
    {
        if (InputManager.instance.IsMoving)
        {
            stateMachine.SetState(player.MoveState);
        }
    }
}
