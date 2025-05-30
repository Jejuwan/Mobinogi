using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WorldToScreenUI : MonoBehaviour
{
    [SerializeField] private Transform target;          // ĳ���� transform
    [SerializeField] private Vector3 offset;           // �Ӹ� �� ������
    [SerializeField] private RectTransform uiElement;  // ü�¹� UI ������Ʈ
    [SerializeField] private Camera mainCamera;

    void OnEnable()
    {
        StartCoroutine(FollowTarget());
    }

    void OnDisable()
    {
        StopAllCoroutines(); // Ȥ�� �ش� �ڷ�ƾ�� �ߴ�
    }

    IEnumerator FollowTarget()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame(); // ī�޶� ������ ���� �� ����

            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            uiElement.position = screenPos;
        }
    }
}
