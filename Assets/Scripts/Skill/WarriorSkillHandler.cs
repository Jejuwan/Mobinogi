using UnityEngine;

public class WarriorSkillHandler : MonoBehaviour, IJobSkillHandler
{
    [SerializeField] private GameObject user;
    public RageComponent rage;
    public string currentSkillName { get; set; }

    private void Awake()
    {
        rage = GetComponent<RageComponent>();
    }

    public void TryUseSkill(SkillBase skill)
    {
        if (skill.skillName == "BladeSmash")
        {
            if (rage.IsMax)
            {

            }
            else
            {

            }
        }
        currentSkillName = skill.skillName;
        skill.Activate(user);
        rage.GainRage(10);
    }
}
