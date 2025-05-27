using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : ChrController
{
    [SerializeField]public Transform player;

    public float chaseRange = 10f;    
    public float stopDistance = 1.5f;
    public bool IsMoving { get; set; }

    private NavMeshAgent agent;


    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        IdleState = new MonsterIdleState(this, stateMachine);
        MoveState = new MonsterMoveState(this, stateMachine);

        stateMachine.AddTransition(IdleState, MoveState, () => IsMoving);
        stateMachine.AddTransition(MoveState, IdleState, () => !IsMoving);

        stateMachine.SetState(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        ScanPlayer();
    }

    public void ScanPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange && distance > stopDistance)
        {
            IsMoving = true;
        }
        else if (distance <= stopDistance)
        {
            IsMoving = false;
        }
    }

    public void Move()
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 velocity = direction * agent.speed;

        characterController.Move(velocity * Time.deltaTime);

        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                10f * Time.deltaTime
            );
        }
    }

    public void Stop()
    {
        agent.ResetPath();
    }
}
