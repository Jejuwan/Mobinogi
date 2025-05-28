using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : ChrController
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public PlayerInputHandler Input;

    public GameObject TargetMonster { get; set; }
    protected override void Start()
    {
        base.Start();

        IdleState = new PlayerIdleState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);
        AttackState = new PlayerAttackState(this, stateMachine);

        stateMachine.AddTransition(IdleState, MoveState, () => Input.IsMoving);
        stateMachine.AddTransition(MoveState, IdleState, () => !Input.IsMoving);
        stateMachine.AddTransition(IdleState, AttackState, () =>
        {
            var closeMonster = GetClosestMonster();
            return (Vector3.Distance(transform.position, closeMonster.transform.position) < 1.5f);
        });

       stateMachine.SetState(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public GameObject GetClosestMonster()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject monster in MonsterPool.Instance.pool)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = monster;
            }
        }
        TargetMonster = closest;
        return closest;
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

    public void OnAttackEnd()
    {
        stateMachine.SetState(IdleState);
    }
}
