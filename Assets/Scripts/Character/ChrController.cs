using UnityEngine;

public class ChrController : MonoBehaviour
{
    [SerializeField] public GameObject Weapon;

    public Animator animator;
    protected CharacterController characterController;

    protected StateMachine stateMachine;
    public HealthComponent healthComponent;
    public State IdleState { get; set; }
    public State MoveState { get; set; }
    public State AttackState { get; set; }
    public State ImpactState { get; set; }

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthComponent = GetComponent<HealthComponent>(); 

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

    public void OnEnableCollider(int val)
    {
        bool enabled = val != 0 ? true : false;
        Weapon.GetComponent<Collider>().enabled = enabled;
    }
}

