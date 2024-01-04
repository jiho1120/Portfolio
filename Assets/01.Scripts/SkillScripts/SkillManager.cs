using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    //밖에서 ㅁㅁ스킬 만들어줘(어느위치에 어느 타이밍에 어떤 각도 ~~ 만들어줘~)

    //그냥 만들어줌. 
    

    Dictionary<AllEnum.SkillName, Skill> nameDictObj = new Dictionary<AllEnum.SkillName, Skill>(); // 네임을 키로 쓰는이유는 알아보기 직관적이여서
    Dictionary<AllEnum.SkillName, SOSkill> nameDictInfo = new Dictionary<AllEnum.SkillName, SOSkill>();


    private void Start()
    {

        GameObject[] objectAll = Resources.LoadAll<GameObject>("모든 스킬프리팹 들어있는 폴더 경로");

        Skill skilltmp;
        foreach (var item in objectAll)
        {
            skilltmp = item.GetComponent<Skill>();
            AllEnum.SkillName name = IntToEnum(skilltmp.Index);

            if (nameDictObj.ContainsKey(name) == false)
            {
                nameDictObj.Add(name, skilltmp);
            }
        }
    }

    public int EnumToInt(AllEnum.SkillName val)
    {
        switch (val)
        {
            case AllEnum.SkillName.Ground:
                return 1;
            case AllEnum.SkillName.AirSlash:
                return 2;
            case AllEnum.SkillName.AirCircle:
                return 3;
            case AllEnum.SkillName.Gravity:
                return 4;
            case AllEnum.SkillName.Fire:
                return 101;
            case AllEnum.SkillName.Heal:
                return 101;
            case AllEnum.SkillName.Love:
                return 101;
            case AllEnum.SkillName.Wind:
                return 101;
            default:
                return 0;
        }
    }

    public AllEnum.SkillName IntToEnum(int val)
    {
        if (val == 1)
        {
            return AllEnum.SkillName.Ground;

        }
        else if (val == 2)
        {
            return AllEnum.SkillName.AirSlash;
        }
        else if (val == 3)
        {
            return AllEnum.SkillName.AirCircle;
        }
        else if (val == 4)
        {
            return AllEnum.SkillName.Gravity;
        }
        else if (val == 4)
        {
            return AllEnum.SkillName.Gravity;
        }
        else if (val == 101)
        {
            return AllEnum.SkillName.Fire;
        }
        else if (val == 102)
        {
            return AllEnum.SkillName.Heal;
        }
        else if (val == 103)
        {
            return AllEnum.SkillName.Love;
        }
        else if (val == 104)
        {
            return AllEnum.SkillName.Wind;
        }
        else
        {
            return AllEnum.SkillName.End;
        }
    }

    public Skill GetSkill(AllEnum.SkillName skillname, GameObject UseCharacter) //UseCharacter(플레이어의 것인지 적의것인지...)
    {
        //이전에 json이든 xml이든 어딘가 스킬 게임오브젝트와 내용물을 매치한 매치테이블이 있으면
        //    그매치테이블에서 정보 받아와서~ 
        nameDictObj[skillname].SetInfo(nameDictInfo[skillname]);
        return nameDictObj[skillname];
    }


}
