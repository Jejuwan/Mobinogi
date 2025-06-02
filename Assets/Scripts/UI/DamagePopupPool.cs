using System.Collections.Generic;
using UnityEngine;

public class DamagePopupPool : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private int poolSize = 20;

    private readonly Queue<DamagePopup> pool = new Queue<DamagePopup>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(popupPrefab, uiCanvas.transform);
            go.SetActive(false);
            pool.Enqueue(go.GetComponent<DamagePopup>());
        }
    }

    public void ShowDamage(Vector3 worldPos, int damage, Camera cam)
    {
        DamagePopup popup = pool.Count > 0 ? pool.Dequeue() : CreateNew();
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos + Vector3.up * 1.5f);
        popup.transform.position = screenPos;
        popup.Show(damage);
        pool.Enqueue(popup); // Update에서 꺼질 때 다시 비활성화됨
    }

    private DamagePopup CreateNew()
    {
        GameObject go = Instantiate(popupPrefab, uiCanvas.transform);
        go.SetActive(false);
        return go.GetComponent<DamagePopup>();
    }
}
