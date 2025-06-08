using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterController : ChrController
{
    [SerializeField] private DamagePopupPool popupPool;
    [SerializeField]public GameObject player;
    [SerializeField] private float dissolveDuration = 2f;

    public float chaseRange = 10f;    
    public float stopDistance = 1.5f;
    public bool nearPlayer { get; set; }

    private NavMeshAgent agent;
    private Material material;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;

        material = GetComponentInChildren<SkinnedMeshRenderer>().material;

        IdleState = new MonsterIdleState(this, stateMachine);
        MoveState = new MonsterMoveState(this, stateMachine);
        AttackState = new MonsterAttackState(this, stateMachine);
        ImpactState = new MonsterImpactState(this, stateMachine);
        DeathState = new MonsterDeathState(this, stateMachine);

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

    public void OnDeath()
    {
        StartCoroutine(DissolveCoroutine());
    }

    public void OnDamaged(int damage)
    {
        popupPool.ShowDamage(transform.position, damage, mainCamera);
    }
    private IEnumerator DissolveCoroutine()
    {
        float time = 0f;

        while (time < dissolveDuration)
        {
            float t = time / dissolveDuration;
            material.SetFloat("_Dissolve", t);
            time += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_Dissolve", 1f);
    }
}
