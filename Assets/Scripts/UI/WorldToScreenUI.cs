using UnityEngine;

public class WorldToScreenUI : MonoBehaviour
{
    [SerializeField] private Transform target;          // ĳ���� transform
    [SerializeField] private Vector3 offset;           // �Ӹ� �� ������
    [SerializeField] private RectTransform uiElement;  // ü�¹� UI ������Ʈ
    [SerializeField] private Camera mainCamera;

    void LateUpdate()
    {
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);
        uiElement.position = screenPos;
    }
}
