using UnityEngine;

public class GoblinController : MonsterController
{
    protected override void Awake()
    {
        base.Awake();
        IdleState = new MonsterIdleState(this, stateMachine);
        MoveState = new MonsterMoveState(this, stateMachine);
        AttackState = new MonsterAttackState(this, stateMachine);
        ImpactState = new MonsterImpactState(this, stateMachine);
        DeathState = new MonsterDeathState(this, stateMachine);

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
}
