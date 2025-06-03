using UnityEngine;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public string description;
    public SkillTag[] tags;

    public enum SkillTag { 연타, 강타, 방해, 생존, 이동, 보조 }

    public abstract void Activate(GameObject user, GameObject target = null);
}
