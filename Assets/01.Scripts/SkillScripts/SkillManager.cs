using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    //�ۿ��� ������ų �������(�����ġ�� ��� Ÿ�ֿ̹� � ���� ~~ �������~)

    //�׳� �������. 
    

    Dictionary<AllEnum.SkillName, Skill> nameDictObj = new Dictionary<AllEnum.SkillName, Skill>(); // ������ Ű�� ���������� �˾ƺ��� �������̿���
    Dictionary<AllEnum.SkillName, SOSkill> nameDictInfo = new Dictionary<AllEnum.SkillName, SOSkill>();


    private void Start()
    {

        GameObject[] objectAll = Resources.LoadAll<GameObject>("��� ��ų������ ����ִ� ���� ���");

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

    public Skill GetSkill(AllEnum.SkillName skillname, GameObject UseCharacter) //UseCharacter(�÷��̾��� ������ ���ǰ�����...)
    {
        //������ json�̵� xml�̵� ��� ��ų ���ӿ�����Ʈ�� ���빰�� ��ġ�� ��ġ���̺��� ������
        //    �׸�ġ���̺��� ���� �޾ƿͼ�~ 
        nameDictObj[skillname].SetInfo(nameDictInfo[skillname]);
        return nameDictObj[skillname];
    }


}
