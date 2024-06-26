public class AllEnum
{
    // enum�̶� ������ �̸��̶� ���߱�

    // ���ҽ� �Ŵ��� ��ųʸ����� Ű��
    public enum DictName
    {
        MonsterDict,
        ItemDict,
        SkillDict,

        End
    }
    public enum ObjectType
    {
        Monster,
        Boss,
        Player,

        End
    }
    public enum MonsterType
    {
        NormalMonster,
        ExplosionMonster,

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
        AirSlash,
        AirCircle,
        Ground,
        Gravity,




        Fire,
        Heal,
        Love,
        Wind,

        End
    }

    public enum ItemType
    {
        Useable,
        Equipable,

        End
    }

    public enum ItemList
    {
        HpPotion,
        MpPotion,
        UltimatePotion,





        Head,
        Top,
        Gloves,
        Weapon,
        Belt,
        Bottom,
        Shoes,

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
