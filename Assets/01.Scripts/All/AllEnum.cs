public class AllEnum
{
    // enum이랑 프리팹 이름이랑 맞추기

    // 리소스 매니저 딕셔너리모음 키값
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

    public enum SkillName // 직접 숫자를 지정하면 나중에 끝의 수가 갯수의 수랑 다르면 엔드도 이상하게나옴
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
        Running, //실행중임
        Success, //성공함
        Failure //실패함
    }
    public enum StateEnum // 디버깅용
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
