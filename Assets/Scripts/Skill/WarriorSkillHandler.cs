using UnityEngine;

public class WarriorSkillHandler : MonoBehaviour, IJobSkillHandler
{
    [SerializeField] private GameObject user;
    private RageComponent rage;
    public string currentSkillName { get; set; }

    private void Awake()
    {
        rage = GetComponent<RageComponent>();
    }

    public void TryUseSkill(SkillBase skill)
    {
        if (skill.skillName == "연속 베기" && !rage.IsMax)
        {
            Debug.Log("[Warrior] 투지가 부족하여 스킬 사용 불가");
            return;
        }

        currentSkillName = skill.skillName;
        skill.Activate(user);
        rage.ConsumeRage(10); // 예시로 스킬 사용 시 투지 감소 처리

    }
}
