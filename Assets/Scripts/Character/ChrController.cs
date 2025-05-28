using UnityEngine;

public class ChrController : MonoBehaviour
{
    public Animator animator;
    protected CharacterController characterController;

    protected StateMachine stateMachine;

    public State IdleState { get; set; }
    public State MoveState { get; set; }
    public State AttackState { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
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
}

