using UnityEngine;

public class ChrController : MonoBehaviour
{
    protected Animator animator;
    protected CharacterController characterController;

    protected StateMachine stateMachine;

    public State IdleState { get; set; }
    public State MoveState { get; set; }
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
}
