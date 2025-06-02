using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    private CanvasGroup canvasGroup;
    private float duration = 2f;
    private float floatSpeed = 100f;
    private float fadeStartRatio = 0.7f;
    private float elapsed;
    private bool isActive;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(int damage)
    {
        damageText.text = damage.ToString();
        canvasGroup.alpha = 1f;
        elapsed = 0f;
        isActive = true;

        float range = 20f; // ÇÈ¼¿ ´ÜÀ§
        Vector2 randomOffset = new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        transform.position += (Vector3)randomOffset;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isActive) return;

        elapsed += Time.deltaTime;
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        float fadeStartTime = duration * fadeStartRatio;

        if (elapsed > fadeStartTime)
        {
            float fadeElapsed = elapsed - fadeStartTime;
            float fadeDuration = duration - fadeStartTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeElapsed / fadeDuration);
        }

        if (elapsed >= duration)
        {
            isActive = false;
            gameObject.SetActive(false);
        }
    }
}
