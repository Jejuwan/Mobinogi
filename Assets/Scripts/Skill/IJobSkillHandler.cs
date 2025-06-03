public interface IJobSkillHandler
{
    public string currentSkillName { get; set; }
    void TryUseSkill(SkillBase skill);
}
