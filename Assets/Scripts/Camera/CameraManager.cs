using UnityEngine;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{

    public static CameraManager instance;
    [SerializeField]public CinemachineCamera mainCam;
    [SerializeField]public CinemachineCamera ultimateCam;
    [SerializeField]public CinemachineOrbitalFollow orbit;
    public float ultimateCamDuration = 2f; // ī�޶� ��ȯ ���� �ð�

    public int originalCullingMask;
    public LayerMask maskWithoutMonster;
    private Coroutine camSwitchRoutine;

    private float cameraYLimitMin;
    private float cameraYLimitMax;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);

        cameraYLimitMin = 25f;
        cameraYLimitMax = 75f;
    }

    private void Update()
    {
  
    }

    public void TriggerUltimate()
    {
        if (camSwitchRoutine != null)
            StopCoroutine(camSwitchRoutine);

        camSwitchRoutine = StartCoroutine(SwitchToUltimateCam());
        PlayerController.Instance.animator.speed = 0.2f;
   
    }

    private IEnumerator SwitchToUltimateCam()
    {
        Vector3 offsetDir = Quaternion.AngleAxis(-45f, Vector3.up) * PlayerController.Instance.transform.forward;
        Vector3 camPos = PlayerController.Instance.transform.position + offsetDir * 2f + Vector3.up * 1f;
        Quaternion camRot = Quaternion.LookRotation(PlayerController.Instance.transform.position + Vector3.up - camPos);

        ultimateCam.transform.SetPositionAndRotation(camPos, camRot);
        // ī�޶� �켱���� ����
        ultimateCam.Priority = 20;
        mainCam.Priority = 10;
        // ���� ����ũ ����
        originalCullingMask = Camera.main.cullingMask;

        // ���� ���̾ ������ ����ũ ����
        Camera.main.cullingMask = maskWithoutMonster;

        // ���ϴ� ���� �ð���ŭ ���
        yield return new WaitForSeconds(ultimateCamDuration);

        PlayerController.Instance.animator.speed = 1f;

        // �ٽ� ���� ī�޶�� ��ȯ
        mainCam.Priority = 20;
        ultimateCam.Priority = 10;

        // ����ũ ����
        Camera.main.cullingMask = originalCullingMask;

        camSwitchRoutine = null;
    }

    public void SetOrbitAxis(Vector2 axis)
    {
        orbit.HorizontalAxis.Value += axis.x;
        orbit.VerticalAxis.Value -= axis.y;
        orbit.VerticalAxis.Value = Mathf.Clamp(orbit.VerticalAxis.Value, cameraYLimitMin, cameraYLimitMax);
    }

}
