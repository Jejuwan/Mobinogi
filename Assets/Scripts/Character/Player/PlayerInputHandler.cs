using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;

    public Vector2 MoveInput { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsJumping { get; private set; }

    private void Update()
    {
        // 이동 입력 (WASD나 조이스틱)
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        MoveInput = new Vector2(horizontal, vertical).normalized;

        //// 공격 입력 (마우스 왼쪽 or 키보드)
        //IsAttacking = Input.GetButtonDown("Fire1");

        //// 점프 입력
        //IsJumping = Input.GetButtonDown("Jump");
    }

    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
}
