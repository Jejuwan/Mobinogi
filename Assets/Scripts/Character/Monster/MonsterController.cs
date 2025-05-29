using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : ChrController
{
    [SerializeField]public GameObject player;

    public float chaseRange = 10f;    
    public float stopDistance = 1.5f;
    public bool nearPlayer { get; set; }

    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        IdleState = new MonsterIdleState(this, stateMachine);
        MoveState = new MonsterMoveState(this, stateMachine);
        AttackState = new MonsterAttackState(this, stateMachine);
        ImpactState = new MonsterImpactState(this, stateMachine);

        stateMachine.AddTransition(IdleState, MoveState, () => !nearPlayer);
        stateMachine.AddTransition(MoveState, AttackState, () => nearPlayer);

        stateMachine.SetState(IdleState);
    }

    protected override void Start()
    {
        base.Start();

        MonsterPool.Instance.pool.Enqueue(this.gameObject);
    }

    protected override void Update()
    {
        base.Update();

        ScanPlayer();
    }

    public void ScanPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= chaseRange && distance > stopDistance)
        {
            nearPlayer = false;
        }
        else if (distance <= stopDistance)
        {
            nearPlayer = true;
        }
    }

    public void Move()
    {
        if (player == null) return;

        agent.SetDestination(player.transform.position);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 velocity = direction * agent.speed;

        characterController.Move(velocity * Time.deltaTime);

        LookController(player.transform, 10f);
    }

    public void Stop()
    {
        agent.ResetPath();
    }

    public void OnSetMoveState()
    {
        stateMachine.SetState(MoveState);
    }
}
