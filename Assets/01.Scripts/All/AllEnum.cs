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

    public enum SkillName // 직접 숫자를 지정하면 나중에 끝의 수가 갯수의 수랑 다르면 엔드도 이상하게나옴
    {
        Ground,
        AirSlash,
        AirCircle,
        Gravity,




        Wind,
        Love,
        Fire,
        Heal,

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

    public enum ItemType
    {
        HpPosion,
        MpPosion,
        ExpPosion,





        Head,
        Top,
        Gloves,
        Waepon,
        Belt,
        Bottom,
        Shoes,

        End
    }
}
