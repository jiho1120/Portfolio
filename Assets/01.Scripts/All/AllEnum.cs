public class AllEnum
{
    public enum ObjectType
    {
        Monster,
        Boss,
        Player,

        End
    }
    public enum MonsterType
    {
        Normal,
        Explosion,

        End
    }
    public enum SkillType
    {
        PassiveSkill,
        ActiveSkill,

        End
    }
    public enum PassiveSkillType
    {
        Damage,
        Heal,
        Buff,
        DeBuff,

        End
    }

    public enum PassiveSkillName
    {
        Wind,
        Love,
        Fire,
        Heal,

        End
    }
    public enum ActiveSkillName
    {
        Ground,
        AirSlash,
        AirCircle,
        Gravity,

        End
    }
    
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Hit,
        Die,

        End
    }
}
