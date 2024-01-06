using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    //나중에 스킬 쏘는 사람 구분도 해야함

    Dictionary<AllEnum.SkillName, Skill> nameDictObj = new Dictionary<AllEnum.SkillName, Skill>(); // 네임을 키로 쓰는이유는 알아보기 직관적이여서
    Dictionary<AllEnum.SkillName, SOSkill> nameDictInfo = new Dictionary<AllEnum.SkillName, SOSkill>();
    Dictionary<AllEnum.SkillName, Skill> skillDict = new Dictionary<AllEnum.SkillName, Skill>(); // 스킬 만들때 이거 사용

    private void Start()
    {

        //foreach (var item in skillDict)
        //{
        //    Debug.Log($"{item.Key}는 {item.Value}");
        //}
    }
    public void Init()
    {
        GameObject[] objectAll = ResourceManager.Instance.objectAll;
        SOSkill[] skillDataAll = ResourceManager.Instance.skillDataAll;
        //PrintResourceInfo(objectAll, "GameObject");
        //PrintResourceInfo(skillDataAll, "SOSkillData");
        Skill skilltmp;
        foreach (var item in objectAll)
        {

            skilltmp = Instantiate(item).GetComponent<Skill>();
            AllEnum.SkillName name = IntToEnum(skilltmp.Index);

            if (!nameDictObj.ContainsKey(name))
            {
                nameDictObj.Add(name, skilltmp);
                skilltmp.gameObject.SetActive(false);
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
    //private void PrintResourceInfo<T>(T[] resources, string resourceName)
    //{
    //    Debug.Log($"--- {resourceName} Resources Info ---");

    //    for (int i = 0; i < resources.Length; i++)
    //    {
    //        Debug.Log($"{resourceName} {i + 1}: {resources[i]}");
    //    }
    //}
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
        switch (val)
        {
            case 1: return AllEnum.SkillName.AirCircle;
            case 2: return AllEnum.SkillName.AirSlash;
            case 3: return AllEnum.SkillName.Ground;
            case 4: return AllEnum.SkillName.Gravity;
            case 101: return AllEnum.SkillName.Fire;
            case 102: return AllEnum.SkillName.Heal;
            case 103: return AllEnum.SkillName.Love;
            case 104: return AllEnum.SkillName.Wind;
            default: return AllEnum.SkillName.End;
        }
    }
    public void UseSKill(AllEnum.SkillName name)
    {
        Skill skill = GetSKillFromDict(name);
        if (skill.orgInfo.inUse)
        {
            Debug.Log("사용중");
            return;
        }
        else
        {
            Vector3 pos = GameManager.Instance.player.transform.position;
            Quaternion rot = GameManager.Instance.player.transform.GetChild(0).rotation;
            if (name == AllEnum.SkillName.Gravity)
            {
                Vector3 spawnOffset = rot * new Vector3(0, 0.5f, 1) * 10f;
                rot = Quaternion.Euler(skill.transform.rotation.eulerAngles);
                pos = pos + spawnOffset;
            }
            skill = SetSkillPos(skill, pos, rot);
            skill.gameObject.SetActive(true);
            skill.DoSkill();
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
                    skill.orgInfo.inUse = false;
                    skill.SetInfo(skillInfo);
                }
                skillDict.Add(skillName, skill);
            }
            else
            {
                Debug.LogError($"Skill not found: {skillName}");
            }
        }
    }
    public Skill GetSKillFromDict(AllEnum.SkillName skillName)
    {
        Skill skill = skillDict[skillName];
        return skill;
    }

    public Skill SetSkillPos(Skill skill, Vector3 pos, Quaternion rot)
    {
        skill.transform.position = pos;
        skill.transform.rotation = rot;

        if (skill.orgInfo.setParent)
        {
            skill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        return skill;
    }

    //끄기 (초기화를 담고있는)
    public void SetOffSkill(Skill skill)
    {
        skill.DoReset();
        skill.gameObject.SetActive(false);
    }




}
