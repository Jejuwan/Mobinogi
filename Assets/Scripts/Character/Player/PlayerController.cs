using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : ChrController
{
    [SerializeField] private float moveSpeed;
    [SerializeField] public int atk;

    public static PlayerController Instance { get; private set; }
    public GameObject TargetMonster { get; set; }

    protected override void Awake()
    {
        base.Awake();

        if(Instance ==null)
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

        stateMachine.AddTransition(IdleState, MoveState, () => InputManager.instance.IsMoving);
        stateMachine.AddTransition(MoveState, IdleState, () => !InputManager.instance.IsMoving);
        stateMachine.AddTransition(IdleState, AttackState, () =>
        {
            var closeMonster = GetClosestMonster();
            if (closeMonster == null) return false;

            var chr = closeMonster.GetComponent<ChrController>();
            if (chr == null || chr.healthComponent.IsDead) return false;

            return Vector3.Distance(transform.position, closeMonster.transform.position) < 1.5f;
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

    }

    public GameObject GetClosestMonster()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject monster in MonsterPool.Instance.pool)
        {
            if (monster == null)
                continue;
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < minDistance)
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
        Vector3 camForward = CameraManager.instance.mainCam.transform.forward;
        Vector3 camRight = CameraManager.instance.mainCam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        //Vector3 moveVector = new Vector3(InputManager.instance.MoveInput.x, -10f/moveSpeed, InputManager.instance.MoveInput.y);
        Vector3 moveVector = camForward * InputManager.instance.MoveInput.y + camRight * InputManager.instance.MoveInput.x;
        characterController.Move(moveVector * moveSpeed* Time.deltaTime);

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
    }
}
