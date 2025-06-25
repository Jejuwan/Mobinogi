using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class HellFire : MonoBehaviour
{
    [SerializeField] public GameObject indicator;
    public Material indicatorMat;
    [SerializeField] public float readyDuration;    // 몇 초 동안 진행할지
    [SerializeField] public float duration;
    [SerializeField] public float fadeDuration;
    [SerializeField] public ParticleSystem skillEffectPrefab;
    public ParticleSystem skillEffect;
    [SerializeField] int dmg;
    [SerializeField] float hitCoolTime;
    private float currentHitCoolTime;
    private Collider hitCollider;

    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
        hitCollider.enabled = false;
        currentHitCoolTime = hitCoolTime;
    }
    private void Start()
    {
        StartCoroutine(ReadyAOE());
    }
    void Update()
    {
        currentHitCoolTime -= Time.deltaTime;
    }

    IEnumerator ReadyAOE()
    {
        float timer = 0f;

        while (timer < readyDuration/2f)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / readyDuration);
            indicatorMat.SetFloat("_Progress", progress);

            yield return null;
        }
        indicator.SetActive(false);
        skillEffect = EffectManager.Instance.ShowEffect(skillEffectPrefab, skillEffect, transform.position, Quaternion.identity, 1f);
        hitCollider.enabled = true;
        StartCoroutine(SkillEnd());
    }

    IEnumerator SkillEnd()
    {
        yield return new WaitForSeconds(4f);

        hitCollider.enabled = false;

        while (skillEffect.isPlaying)
            yield return null;

        Destroy(skillEffect.gameObject);
        Destroy(gameObject);
    }
    IEnumerator DeActivateCollider(float delay)
    {
        yield return new WaitForSeconds(delay);

        hitCollider.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && currentHitCoolTime < 0f)
        {
            PlayerController.Instance.healthComponent.TakeDamage(dmg);
            currentHitCoolTime = hitCoolTime;
        }
    }
}
