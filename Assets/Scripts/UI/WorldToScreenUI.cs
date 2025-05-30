using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WorldToScreenUI : MonoBehaviour
{
    [SerializeField] private Transform target;          // 캐릭터 transform
    [SerializeField] private Vector3 offset;           // 머리 위 오프셋
    [SerializeField] private RectTransform uiElement;  // 체력바 UI 오브젝트
    [SerializeField] private Camera mainCamera;

    void OnEnable()
    {
        StartCoroutine(FollowTarget());
    }

    void OnDisable()
    {
        StopAllCoroutines(); // 혹은 해당 코루틴만 중단
    }

    IEnumerator FollowTarget()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame(); // 카메라 움직임 끝난 후 실행

            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            uiElement.position = screenPos;
        }
    }
}
