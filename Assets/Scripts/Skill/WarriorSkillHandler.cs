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
        if (skill.skillName == "���� ����" && !rage.IsMax)
        {
            Debug.Log("[Warrior] ������ �����Ͽ� ��ų ��� �Ұ�");
            return;
        }

        currentSkillName = skill.skillName;
        skill.Activate(user);
        rage.ConsumeRage(10); // ���÷� ��ų ��� �� ���� ���� ó��

    }
}
