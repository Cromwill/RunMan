public interface IPlayerComponent
{
    void Initialization(params Booster[] boosters);
    BoosterType BoosterType { get; }
    void UsedSkill(SkillData skill, int count);
}

