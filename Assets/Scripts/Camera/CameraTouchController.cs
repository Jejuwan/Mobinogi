using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Windows;

public class CameraTouchController : MonoBehaviour
{
    public Transform cameraPivot; // 회전용 빈 오브젝트 (Camera의 Follow 타겟)
    public float sensitivityX = 0.2f;
    public float minYaw = -180f;
    public float maxYaw = 180f;

    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        if (cameraPivot == null)
            Debug.LogError("Camera Pivot not assigned.");

        yaw = cameraPivot.eulerAngles.y;

        InputManager.instance.OnDragEvent += RotateCamPivot;
    }

    void Update()
    {
       
    }
    private void RotateCamPivot(Vector2 deltaPos)
    {
        yaw += deltaPos.x * 1f;
        pitch -= deltaPos.y * 1f;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0, 5f, -5f);
        //cameraPivot.position = PlayerController.Instance.transform.position + offset;
        //cameraPivot.rotation = rotation;

        CameraManager.instance.SetOrbitAxis(deltaPos);
    }
}

