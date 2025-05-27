using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private float moveSpeed;

    private CharacterController characterController;
    private Animator animator;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    { 
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>(); 

        playerInputActions = new PlayerInputActions();
        //playerInputActions.Player.Enable();
    }

    private void Update()
    {
        Vector3 moveVector = new Vector3(joystick.Horizontal * moveSpeed, -9.8f, joystick.Vertical* moveSpeed);
        characterController.Move(moveVector * Time.deltaTime);

        Vector3 rotateVector = moveVector;
        rotateVector.y = 0f;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rotateVector);
            animator.SetBool("isWalking", true);
        }
        else
            animator.SetBool("isWalking", false);
    }
}
