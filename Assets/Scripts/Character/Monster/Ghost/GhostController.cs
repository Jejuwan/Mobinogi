using UnityEngine;

public class GhostController : MonsterController
{
    public State HellHoleState { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        IdleState = new GhostIdleState(this, stateMachine);
        MoveState = new GhostMoveState(this, stateMachine);
        HellHoleState = new GhostHellHoleState(this, stateMachine);

        attackDist = 10f;
        detectDist = 20f;

        stateMachine.AddTransition(IdleState, MoveState, () =>
        {
            return Vector3.Distance(transform.position, PlayerController.Instance.transform.position) <= detectDist;
        });
        stateMachine.AddTransition(MoveState, HellHoleState, () =>
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
