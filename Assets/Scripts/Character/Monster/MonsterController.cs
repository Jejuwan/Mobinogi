using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using TMPro;

public class MonsterController : ChrController
{
    [SerializeField] private DamagePopupPool popupPool;
    [SerializeField] public TextMeshProUGUI nameTag;
    [SerializeField] public HealthBarUI healthBarUI;
    [SerializeField] public GameObject player;
    [SerializeField] private float dissolveDuration = 2f;

    private Material material;

    protected override void Awake()
    {
        base.Awake();

        healthComponent.OnDeath += () =>
        {
            nameTag.enabled = false;
        };
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;

        IdleState = new MonsterIdleState(this, stateMachine);
        MoveState = new MonsterMoveState(this, stateMachine);
        AttackState = new MonsterAttackState(this, stateMachine);
        ImpactState = new MonsterImpactState(this, stateMachine);
        DeathState = new MonsterDeathState(this, stateMachine);

        nearOpponent = false;

        attackDist = 1.5f;
        detectDist = 10f;

        stateMachine.AddTransition(IdleState, MoveState, () =>
        {
            return Vector3.Distance(transform.position, player.transform.position) <= detectDist;
        });
        stateMachine.AddTransition(MoveState, AttackState, () =>
        {
            return Vector3.Distance(transform.position, player.transform.position) <= attackDist;
        });

        stateMachine.SetState(IdleState);
    }

    protected override void Start()
    {
        base.Start();

        MonsterPool.Instance.pool.Enqueue(this);

        nameTag.enabled = false;
        healthBarUI.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        if (healthComponent.IsDead)
            stateMachine.SetState(DeathState);
     
    }

    public void Move()
    {
        if (player == null) return;

        agent.SetDestination(player.transform.position);

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 velocity = direction * agent.speed;

        //characterController.Move(velocity * Time.deltaTime);

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
