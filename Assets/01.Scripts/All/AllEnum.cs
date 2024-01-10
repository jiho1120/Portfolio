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

    public enum SkillName // ���� ���ڸ� �����ϸ� ���߿� ���� ���� ������ ���� �ٸ��� ���嵵 �̻��ϰԳ���
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
