using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GhostController : MonsterController
{
    [SerializeField] public GameObject Meteor;
    [SerializeField] public GameObject MiniMeteor;
    [SerializeField] public GameObject Crystal;
    [SerializeField] public ParticleSystem teleportEffectPrefab;
    public ParticleSystem teleportEffect;
    public List<AttackPattern> attackPatterns;

    [SerializeField] public Transform[] teleportPos;
    public int teleportIdx = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        IdleState = new GhostIdleState(this, stateMachine);
        MoveState = new GhostMoveState(this, stateMachine);
        AttackState = new GhostAttackState(this, stateMachine);
        ImpactState = new GhostImpactState(this, stateMachine);
        DeathState = new GhostDeathState(this, stateMachine);

        attackDist = 10f;
        detectDist = 20f;

        stateMachine.AddTransition(IdleState, MoveState, () =>
        {
            return Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= detectDist;
        });
        stateMachine.AddTransition(MoveState, AttackState, () =>
        {
            return Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= attackDist;
        });

        stateMachine.SetState(IdleState);
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnMeteor()
    {
        Instantiate(Meteor, PlayerController.Instance.transform.position,Quaternion.identity);
    }

    void OnMiniMeteor()
    {
        Instantiate(MiniMeteor, PlayerController.Instance.transform.position, Quaternion.identity);
    }

    void OnCrystal()
    {
        Vector3 direction = PlayerController.Instance.transform.position - transform.position;
        direction.y = 0f; // 수직 방향 무시 (XZ 평면 기준)
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion quat = new Quaternion(0.63132298f, 0.318482876f, -0.318482876f, 0.63132298f);
        Quaternion zRotation = Quaternion.Euler(0f, 0f, -angle -150f);
        quat *= zRotation;

        Instantiate(Crystal,transform.position, quat);
    }

    void OnTeleport()
    {
        StartCoroutine(TeleportAndWait());
    }

    IEnumerator TeleportAndWait()
    {
        teleportEffect = EffectManager.Instance.ShowEffect(teleportEffectPrefab, teleportEffect, transform.position, Quaternion.identity, 1f);
        yield return new WaitWhile(() => teleportEffect.isPlaying);
        teleportIdx = teleportIdx == 0 ? 1 : 0;
        transform.position = teleportPos[teleportIdx].position;
    }

}
