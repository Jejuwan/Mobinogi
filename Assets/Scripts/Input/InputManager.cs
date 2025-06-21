using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour,IPointerDownHandler
{

    public static InputManager instance;
    private PlayerInputActions inputActions;
    private Vector2 lastPosition;
    public float sensitivity = 2f;
    public bool isDragging { get; set;}

    public delegate void OnDrag(Vector2 delta);
    public event OnDrag OnDragEvent;

    private InputAction pointAction;
    private InputAction pressAction;

    [SerializeField] private FixedJoystick joystick;
    public Vector2 MoveInput { get; private set; }
    public bool IsMoving => MoveInput.sqrMagnitude > 0.01f;
    private PointerEventData pointerEventData; 

    public enum InputMode { Move, View}
    public InputMode currentInputMode;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        isDragging = false;
        inputActions = new PlayerInputActions();
        pointAction = inputActions.TouchControls.Point;
        pressAction = inputActions.TouchControls.Press;
    }
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        Vector2 currentPos = pointAction.ReadValue<Vector2>();
        bool isPressed = pressAction.ReadValue<float>() > 0.5f;

        if (isPressed)
        {
            float screenHeight = Screen.height;
            if (currentPos.y > screenHeight * 0.5f)
            {
                if (!isDragging)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    lastPosition = currentPos;
                    isDragging = true;
                    currentInputMode = InputMode.View;
                }
                else
                {
                    if (currentInputMode == InputMode.View)
                    {
                        Vector2 delta = (currentPos - lastPosition) * sensitivity;
                        OnDragEvent?.Invoke(delta);
                        lastPosition = currentPos;
                    }
                }
            }
            else
            {
                if (!isDragging)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    isDragging = true;
                    joystick.transform.position = currentPos;
                    joystick.gameObject.SetActive(true);
                    currentInputMode = InputMode.Move;
                }
                else
                {
                    if (currentInputMode == InputMode.Move)
                    {
                        float horizontal = joystick.Horizontal;
                        float vertical = joystick.Vertical;
                        MoveInput = new Vector2(horizontal, vertical).normalized;
                        joystick.moveJoystick(currentPos);
                    }
                }
            }
        }
        else
        {
            isDragging = false;
            MoveInput = Vector2.zero;
            joystick.stopJoystick();
            joystick.gameObject.SetActive(false);
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        pointerEventData = eventData;
    }

}
