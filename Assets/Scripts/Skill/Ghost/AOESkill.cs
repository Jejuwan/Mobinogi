using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AOESkill : MonoBehaviour
{
    [SerializeField] public GameObject indicator;
    public Material indicatorMat;
    [SerializeField] public float readyDuration;    // 몇 초 동안 진행할지
    [SerializeField] public float duration;
    [SerializeField] public ParticleSystem skillEffectPrefab;
    public ParticleSystem skillEffect;
    [SerializeField] Vector3 effectPosOffset;
    [SerializeField] Vector3 effectRotOffset;
    [SerializeField] float effectScale;
    [SerializeField] int dmg;
    [SerializeField] float hitCoolTime;
    [SerializeField] float startValue;

    public float currentHitCoolTime;
    public Collider hitCollider;

    protected virtual void Awake()
    {
        hitCollider = GetComponent<Collider>();
        hitCollider.enabled = false;
    }
    protected virtual void Start()
    {
        StartCoroutine(ReadyAOE());
    }
    protected virtual void Update()
    {
        currentHitCoolTime -= Time.deltaTime;
    }

    IEnumerator ReadyAOE()
    {
        float timer = startValue*(readyDuration/2f);

        while (timer < (readyDuration/2f))
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / (readyDuration/2f));
            indicatorMat.SetFloat("_Progress", progress);

            yield return null;
        }
        indicator.SetActive(false);

        // 위치는 기준 오브젝트 기준 + 오프셋
        Vector3 effectPos = transform.position + effectPosOffset;

        // 회전: 원래 z축 회전만 유지하고 거기에 offset 추가
        Vector3 rotation = transform.eulerAngles + effectRotOffset;
        Quaternion effectRot = Quaternion.Euler(rotation);

        // 이펙트 생성
        skillEffect = EffectManager.Instance.ShowEffect(skillEffectPrefab, skillEffect, effectPos, effectRot, effectScale);
        hitCollider.enabled = true;
        StartCoroutine(SkillEnd());
    }

    IEnumerator SkillEnd()
    {
        yield return new WaitForSeconds(duration);

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
