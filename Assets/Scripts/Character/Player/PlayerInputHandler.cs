using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    public Vector2 MoveInput { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }

    private void Update()
    {
        // �̵� �Է� (WASD�� ���̽�ƽ)
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        MoveInput = new Vector2(horizontal, vertical).normalized;

        //// ���� �Է� (���콺 ���� or Ű����)
        //IsAttacking = Input.GetButtonDown("Fire1");

        //// ���� �Է�
        //IsJumping = Input.GetButtonDown("Jump");
    }

    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
}
