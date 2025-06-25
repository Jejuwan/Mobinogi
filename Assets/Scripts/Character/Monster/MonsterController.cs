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
    [SerializeField] public TextMeshProUGUI nameTag;
    [SerializeField] public HealthBarUI healthBarUI;

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

        nearOpponent = false;

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
        agent.SetDestination(PlayerController.Instance.transform.position);

        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;
        Vector3 velocity = direction * agent.speed;

        LookController(PlayerController.Instance.transform, 10f);
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
        DamagePopupPool.instance.ShowDamage(transform.position, damage, Camera.main);
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
