using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] public ParticleSystem rageEffectPrefab;
    [SerializeField] public ParticleSystem swordTrailPrefab;
    [SerializeField] public ParticleSystem swordTrailRagePrefab;
    [SerializeField] public ParticleSystem shieldBashPrefab;
    [SerializeField] public ParticleSystem shieldBashRagePrefab;
    [SerializeField] public ParticleSystem kickPrefab;
    [SerializeField] public ParticleSystem kickRagePrefab;
    [SerializeField] public ParticleSystem TauntPrefab;
    [SerializeField] public ParticleSystem TauntRagePrefab;

    public static PlayerEffectController Instance;
    public ParticleSystem rageEffect;
    public ParticleSystem swordTrailEffect;
    public ParticleSystem swordTrailRageEffect;
    public ParticleSystem shieldBashEffect;
    public ParticleSystem shieldBashRageEffect;
    public ParticleSystem kickEffect;
    public ParticleSystem kickRageEffect;
    public ParticleSystem tauntEffect;
    public ParticleSystem tauntRageEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowEffect(ParticleSystem inPs, ParticleSystem outPs, Vector3 pos, Quaternion quat, float localSize)
    {
        if (inPs != null)
        {
            outPs = Instantiate(inPs, pos, quat);
            outPs.transform.localScale = Vector3.one * localSize;
            outPs.Play();
        }
    }

    public void DestroyEffect(ParticleSystem outPos)
    {
        if (outPos != null)
        {
            Destroy(outPos.gameObject);
        }
    }

    public void ShowAndDestroyEffect(ParticleSystem inPs, ParticleSystem outPs, Vector3 pos, Quaternion quat, float localSize)
    {
        if (inPs != null)
        {
            outPs = Instantiate(inPs, pos, quat);
            outPs.transform.localScale = Vector3.one * localSize;
            outPs.Play();
            Destroy(outPs.gameObject, outPs.main.duration + 0.5f);
        }
    }
}