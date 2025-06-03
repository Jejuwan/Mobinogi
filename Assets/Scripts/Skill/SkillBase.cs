using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public string description;
    public SkillTag[] tags;

    public enum SkillTag { ��Ÿ, ��Ÿ, ����, ����, �̵�, ���� }

    public abstract void Activate(GameObject user, GameObject target = null);
}
