using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public enum SkillTag { ��Ÿ, ��Ÿ, ����, ����, �̵�, ���� }
    public AudioClip sound;
    public AudioClip rageSound;

    public abstract void Activate(GameObject user, GameObject target = null);
}
