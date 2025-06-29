using UnityEngine;

[System.Serializable]
public class AttackPattern
{
    public string name;
    public float minDistance;
    public float maxDistance;
    public float cooldown;
    public string animTrigger;

    private float lastUsedTime = -Mathf.Infinity;

    public bool IsAvailable(Transform boss, Transform player)
    {
        float dist = Vector3.Distance(boss.position, player.position);
        return dist >= minDistance && dist <= maxDistance && Time.time >= lastUsedTime + cooldown;
    }

    public void Use()
    {
        lastUsedTime = Time.time;
    }
}
