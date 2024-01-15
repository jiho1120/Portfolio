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

    public enum SkillName // 직접 숫자를 지정하면 나중에 끝의 수가 갯수의 수랑 다르면 엔드도 이상하게나옴
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
