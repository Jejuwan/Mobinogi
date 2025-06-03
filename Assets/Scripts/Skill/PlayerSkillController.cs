using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public static PlayerSkillController Instance;
    public IJobSkillHandler skillHandler;

    private void Awake()
    {
        Instance = this;
        skillHandler = GetComponent<IJobSkillHandler>();
    }

    public void TryUseSkill(SkillBase skill)
    {
        skillHandler.TryUseSkill(skill);
    }
}