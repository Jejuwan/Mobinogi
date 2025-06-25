using UnityEngine;

public class GoblinController : MonsterController
{
    protected override void Awake()
    {
        base.Awake();
        IdleState = new GoblinIdleState(this, stateMachine);
        MoveState = new GoblinMoveState(this, stateMachine);
        AttackState = new GoblinAttackState(this, stateMachine);
        ImpactState = new GoblinImpactState(this, stateMachine);
        DeathState = new GoblinDeathState(this, stateMachine);

        attackDist = 1.5f;
        detectDist = 10f;

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
