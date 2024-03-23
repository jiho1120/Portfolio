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
        DeActivate,

        End
    }
    
    public enum NewSkillType
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

    public enum ItemListType
    {
        Inventory,
        PlayerUI,

        End
    }

    public enum NodeState
    {
        Running, //��������
        Success, //������
        Failure //������
    }
    public enum StateEnum // ������
    {
        DIe,
        Stun,
        Skill,
        BasicAttack,
        Run,
        Walk,
        Idle,

        End
    }
}
