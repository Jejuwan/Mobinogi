using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    public Vector2 MoveInput { get; private set; }

    private void Update()
    {
        // 이동 입력 (WASD나 조이스틱)
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        MoveInput = new Vector2(horizontal, vertical).normalized;

    }

    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
}
