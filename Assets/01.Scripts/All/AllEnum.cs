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

    public enum ItemListType
    {
        Inventory,
        PlayerUI,

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
