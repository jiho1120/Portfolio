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
    public enum States
    {
        Idle,
        Walk,
        Attack,
        Hit,
        Die,

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




        Fire,
        Heal,
        Love,
        Wind,

        End
    }

    public enum ItemType
    {
        HpPosion,
        MpPosion,
        UltimatePosion,





        Head,
        Top,
        Gloves,
        Weapon,
        Belt,
        Bottom,
        Shoes,

        End
    }
    public enum PlyerStat
    {
        maxHealth,
        attack,
        defense,
        criticalChance,
        movementSpeed,
        experience,
        maxMana,
        luck,
        maxUltimateGauge,

        End
    }

    public enum ItemListType
    {
        Inventory,
        PlayerUI,

        End
    }
}
