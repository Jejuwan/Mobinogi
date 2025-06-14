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

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

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
                    lastPosition = currentPos;
                    isDragging = true;
                }
                else
                {
                    Vector2 delta = (currentPos - lastPosition) * sensitivity;
                    OnDragEvent?.Invoke(delta);
                    lastPosition = currentPos;
                }
            }
            else
            {
                if (!isDragging)
                {
                    isDragging = true;
                    joystick.transform.position = currentPos;
                }
                else
                {
                    float horizontal = joystick.Horizontal;
                    float vertical = joystick.Vertical;
                    MoveInput = new Vector2(horizontal, vertical).normalized;

                    //Debug.Log(currentPos);
                    Vector2 delta = (currentPos - lastPosition) * sensitivity;
                    joystick.moveJoystick(currentPos);    
                }
            }
        }
        else
        {
            isDragging = false;
        }
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        pointerEventData = eventData;
    }

}
