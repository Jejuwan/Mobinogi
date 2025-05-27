using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : ChrController
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public PlayerInputHandler Input;

    protected override void Start()
    {
        base.Start();

        IdleState = new PlayerIdleState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);

        stateMachine.AddTransition(IdleState, MoveState, () => Input.IsMoving);
        stateMachine.AddTransition(MoveState, IdleState, () => !Input.IsMoving);

        stateMachine.SetState(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Move()
    {
        Vector3 moveVector = new Vector3(Input.MoveInput.x, -10f/moveSpeed, Input.MoveInput.y);
        characterController.Move(moveVector * moveSpeed* Time.deltaTime);

        Vector3 rotateVector = moveVector;
        rotateVector.y = 0f;

        if (Input.MoveInput.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(rotateVector);
        }
    }
}
