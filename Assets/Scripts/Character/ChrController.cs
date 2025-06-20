using UnityEngine;

public class ChrController : MonoBehaviour
{
    [SerializeField] public GameObject[] attackColliders;
    [SerializeField] protected Camera mainCamera;

    public Animator animator;
    protected CharacterController characterController;

    public StateMachine stateMachine;
    public HealthComponent healthComponent;
    public State IdleState { get; set; }
    public State MoveState { get; set; }
    public State AttackState { get; set; }
    public State ImpactState { get; set; }
    public State DeathState { get; set; }
    public State SkillState { get; set; }

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeath += () =>
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
        };
        stateMachine = new StateMachine();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        stateMachine.Tick(Time.deltaTime);
        characterController.Move(new Vector3(0, -9.8f*Time.deltaTime, 0));
    }

    public void SetAnimBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void SetAnimTrigger(string name)
    {
        animator.SetTrigger(name);
    }

    public void LookController(Transform target, float speed)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                speed * Time.deltaTime
            );
        }
    }

    public void SetState(State state)
    {
        stateMachine.SetState(state);
    }

    public void OnEnableCollider(int idx)
    {
        Collider[] colliders = attackColliders[idx].GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }
    public void OnDisableCollider(int idx)
    {
        Collider[] colliders = attackColliders[idx].GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }
}

