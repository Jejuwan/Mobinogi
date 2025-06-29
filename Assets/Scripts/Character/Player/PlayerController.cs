using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PlayerController : ChrController
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public int atk;
    public static PlayerController Instance { get; private set; }
    public MonsterController TargetMonster { get; set; }

    public bool autoMode { get; set; }
    public bool immortal { get; set; }

    [SerializeField] public GameObject[] navigationPoint;
    public int navPointIdx { get; set; } = 0;

    protected override void Awake()
    {
        base.Awake();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        IdleState = new PlayerIdleState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);
        AttackState = new PlayerAttackState(this, stateMachine);
        ImpactState = new PlayerImpactState(this, stateMachine);
        SkillState = new PlayerSkillState(this, stateMachine);

        autoMode = false;
        nearOpponent = false;
        immortal = false;

        attackDist = 1.5f;
        detectDist = 10f;

        stateMachine.AddTransition(IdleState, MoveState, () =>
        {

              return InputManager.instance.IsMoving;

        });
        stateMachine.AddTransition(IdleState, AttackState, () =>
        {
            var closeMonster = GetClosestMonster();
            if (!closeMonster) return false;

            float dist = Vector3.Distance(transform.position, closeMonster.transform.position);

            nearOpponent = dist < detectDist;

            return dist < attackDist;
        });
        stateMachine.AddTransition(MoveState, IdleState, () =>
        {
            if (!autoMode)
                return !InputManager.instance.IsMoving;
            else
            {
                if (!InputManager.instance.IsMoving)
                {
                    {
                        var closeMonster = GetClosestMonster();
                        if (!closeMonster) return false;

                        float dist = Vector3.Distance(transform.position, closeMonster.transform.position);

                        nearOpponent = dist < detectDist;

                        return dist < attackDist;
                    }
                }
                return false;
            }
        });

        stateMachine.SetState(IdleState);
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log(stateMachine.currentState);
    }

    public MonsterController GetClosestMonster()
    {
        MonsterController closest = null;
        float minDistance = float.MaxValue;

        foreach (MonsterController monster in MonsterPool.Instance.pool)
        {
            if (monster == null)
                continue;
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < minDistance && !monster.healthComponent.IsDead)
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
        if (!autoMode)
        {
            ForceMove();
        }
        else
        {
            if(InputManager.instance.IsMoving)
            {
                ForceMove();
            }
            else
            {
                if (TargetMonster != null)
                {
                    agent.SetDestination(TargetMonster.transform.position);
                    Vector3 next = agent.nextPosition;
                    Vector3 dir = next - transform.position;
                }

                if (!agent.pathPending && agent.remainingDistance > 0.1f)
                {
                    Vector3 dir = agent.steeringTarget - transform.position;
                    dir.y = 0f; // Y축 고정 (수직 기울어짐 방지)

                    if (dir.sqrMagnitude > 0.001f)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                    }
                }
            }
        }
    }

    void ForceMove()
    {
        Vector3 camForward = CameraManager.instance.mainCam.transform.forward;
        Vector3 camRight = CameraManager.instance.mainCam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveVector = camForward * InputManager.instance.MoveInput.y + camRight * InputManager.instance.MoveInput.x;
        agent.SetDestination(transform.position + moveVector);

        Vector3 rotateVector = moveVector;
        rotateVector.y = 0f;

        if (InputManager.instance.MoveInput.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(rotateVector);
        }
    }

    public void SetPlayerCastingReady(int boolParam)
    {
        bool enabled = boolParam != 0 ? true : false;
        PlayerSkillController.Instance.PlayerCastingReady = enabled;
    }

    public void SetIdleState()
    {
        stateMachine.SetState(IdleState);
        immortal = false;
    }
}
