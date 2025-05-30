using UnityEngine;

public class WorldToScreenUI : MonoBehaviour
{
    [SerializeField] private Transform target;          // 캐릭터 transform
    [SerializeField] private Vector3 offset;           // 머리 위 오프셋
    [SerializeField] private RectTransform uiElement;  // 체력바 UI 오브젝트
    [SerializeField] private Camera mainCamera;

    void LateUpdate()
    {
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        uiElement.position = screenPos;
    }
}
