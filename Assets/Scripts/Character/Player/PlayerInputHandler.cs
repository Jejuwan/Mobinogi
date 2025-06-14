using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    public Vector2 MoveInput { get; private set; }

    private void Update()
    {
        // �̵� �Է� (WASD�� ���̽�ƽ)
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        MoveInput = new Vector2(horizontal, vertical).normalized;

    }

    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
}
