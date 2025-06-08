using UnityEngine;
using System.Collections.Generic;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] public ParticleSystem rageEffectPrefab;
    [SerializeField] public ParticleSystem swordTrailPrefab;
    [SerializeField] public ParticleSystem ragedSwordTrailPrefab;

    public static PlayerEffectController instance;
    private ParticleSystem rageEffect;
    private ParticleSystem swordTrailEffect;
    private ParticleSystem ragedSwordTrailEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowRageEffect()
    {
        if (rageEffectPrefab != null)
        {
            rageEffect = Instantiate(rageEffectPrefab, transform.position, Quaternion.identity);
            rageEffect.transform.localScale = Vector3.one * 0.7f;
            rageEffect.Play();
        }
    }

    public void DestroyRageEffect()
    {
        if (rageEffect != null)
        {
            Destroy(rageEffect.gameObject);
        }
    }

    public void ShowSwordTrailEffect()
    {
        if (swordTrailPrefab != null && ragedSwordTrailPrefab != null)
        {
            if (!PlayerSkillController.Instance.skillHandler.rage.IsMax)
            {
                swordTrailEffect = Instantiate(swordTrailPrefab, transform.position + Vector3.up * 1f, Quaternion.Euler(90f, 0, 0));
                swordTrailEffect.transform.localScale = Vector3.one * 1f;
                swordTrailEffect.Play();
                Destroy(swordTrailEffect.gameObject, swordTrailEffect.main.duration + 0.5f);
            }
            else
            {
                ragedSwordTrailEffect = Instantiate(ragedSwordTrailPrefab, transform.position + Vector3.up * 1f, Quaternion.Euler(90f, 0, 0));
                ragedSwordTrailEffect.transform.localScale = Vector3.one * 2f;
                ragedSwordTrailEffect.Play();
                Destroy(ragedSwordTrailEffect.gameObject, ragedSwordTrailEffect.main.duration + 0.5f);
            }
        }
    }
}