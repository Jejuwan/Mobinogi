using UnityEngine;

public class DamageResponder : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public AudioSource audioSource;
    public AudioClip hitSound;

    private HealthComponent health;

    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        if (health != null)
        {
            health.OnHealthChanged += OnDamaged;
            //health.OnDeath += OnDeath;
        }
    }

    private void OnDamaged(int current, int max)
    {
        if (hitEffect != null)
        {
            Vector3 randomOffset = Random.insideUnitSphere * .3f; // radius
            var effect = Instantiate(hitEffect, transform.position + Vector3.up * 1.5f + randomOffset, Quaternion.identity);
            effect.transform.localScale = Vector3.one * 0.1f;
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration + 0.5f);
        }

        AudioController.Instance.PlaySound(hitSound);
    }

    private void OnDeath(GameObject killer)
    {
        // 죽을 때 이펙트나 사운드 따로 처리하고 싶다면 여기에
    }
}
