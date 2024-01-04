using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    //�ۿ��� ������ų �������(�����ġ�� ��� Ÿ�ֿ̹� � ���� ~~ �������~)

    //�׳� �������. 
    //���߿� ��ų ��� ��� ���е� �ؾ���

    Dictionary<AllEnum.SkillName, Skill> nameDictObj = new Dictionary<AllEnum.SkillName, Skill>(); // ������ Ű�� ���������� �˾ƺ��� �������̿���
    Dictionary<AllEnum.SkillName, SOSkill> nameDictInfo = new Dictionary<AllEnum.SkillName, SOSkill>();
    public Dictionary<AllEnum.SkillName, Skill> perfectSkillDict = new Dictionary<AllEnum.SkillName, Skill>(); // ��ų ���鶧 �̰� ���


    private void Start()
    {
        
        //foreach (var item in perfectSkillDict)
        //{
        //    Debug.Log($"{item.Key}�� {item.Value}");
        //}
    }
    public void SetSkillData()
    {
        GameObject[] objectAll = ResourceManager.Instance.objectAll;
        SOSkill[] skillDataAll = ResourceManager.Instance.skillDataAll;
        PrintResourceInfo(objectAll, "GameObject");
        PrintResourceInfo(skillDataAll, "SOSkillData");
        Skill skilltmp;
        foreach (var item in objectAll)
        {
            skilltmp = item.GetComponent<Skill>();
            AllEnum.SkillName name = IntToEnum(skilltmp.Index);

            if (!nameDictObj.ContainsKey(name))
            {
                nameDictObj.Add(name, skilltmp);
            }
        }
        for (int i = 0; i < skillDataAll.Length; i++)
        {
            AllEnum.SkillName skillName = IntToEnum(skillDataAll[i].index);

            if (!nameDictInfo.ContainsKey(skillName))
            {
                nameDictInfo.Add(skillName, skillDataAll[i]);
            }
        }
        SetAllSkill();
    }
    private void PrintResourceInfo<T>(T[] resources, string resourceName)
    {
        Debug.Log($"--- {resourceName} Resources Info ---");

        for (int i = 0; i < resources.Length; i++)
        {
            Debug.Log($"{resourceName} {i + 1}: {resources[i]}");
        }
    }
    public int EnumToInt(AllEnum.SkillName val)
    {
        switch (val)
        {
            case AllEnum.SkillName.AirCircle:
                return 1;
            case AllEnum.SkillName.AirSlash:
                return 2;
            case AllEnum.SkillName.Ground:
                return 3;
            case AllEnum.SkillName.Gravity:
                return 4;
            case AllEnum.SkillName.Fire:
                return 101;
            case AllEnum.SkillName.Heal:
                return 102;
            case AllEnum.SkillName.Love:
                return 103;
            case AllEnum.SkillName.Wind:
                return 104;
            default:
                return 0;
        }
    }

    public AllEnum.SkillName IntToEnum(int val)
    {
        if (val == 1)
        {
            return AllEnum.SkillName.AirCircle;

        }
        else if (val == 2)
        {
            return AllEnum.SkillName.AirSlash;
        }
        else if (val == 3)
        {
            return AllEnum.SkillName.Ground;
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

    public void SetAllSkill()
    {
        for (int i = 0; i < (int)AllEnum.SkillName.End; i++)
        {
            AllEnum.SkillName skillName = (AllEnum.SkillName)i;
            if (nameDictObj.TryGetValue(skillName, out Skill skill))
            {
                if (nameDictInfo.TryGetValue(skillName, out SOSkill skillInfo))
                {
                    skill.SetInfo(skillInfo);
                }
                perfectSkillDict.Add(skillName, skill);
            }
            else
            {
                Debug.LogError($"Skill not found: {skillName}");
            }
        }
    }

    public void SetSkillPos(Skill skill, Vector3 pos)
    {
        skill.transform.position = pos;
        skill.transform.rotation = GameManager.Instance.player.transform.GetChild(0).rotation;
        if (skill.orgInfo.setParent)
        {
            skill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
    }
}
