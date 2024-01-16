using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public int PassiveCurrentNum;
    public Skill passiveSkill { get; private set; }
    //���߿� ��ų ��� ��� ���е� �ؾ���

    Dictionary<AllEnum.SkillName, Skill> nameDictObj = new Dictionary<AllEnum.SkillName, Skill>(); // ������ Ű�� ���������� �˾ƺ��� �������̿���
    Dictionary<AllEnum.SkillName, SOSkill> nameDictInfo = new Dictionary<AllEnum.SkillName, SOSkill>();
    public Dictionary<AllEnum.SkillName, Skill> skillDict { get; private set; } // ��ų ���鶧 �̰� ���

    public void Init()
    {
        skillDict = new Dictionary<AllEnum.SkillName, Skill>();
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
        PassiveCurrentNum = Random.Range((int)AllEnum.SkillName.Fire, (int)AllEnum.SkillName.End); // �ʹ� �ѹ� ���� �̰͵� ������
    }
    public void CallPassiveSkill()
    {
        PassiveCurrentNum++;
        if (PassiveCurrentNum >= (int)AllEnum.SkillName.End) // �ε��� �ѱ�� ó������ ����
        {
            PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
        }
        passiveSkill = skillDict[(AllEnum.SkillName)PassiveCurrentNum];
        passiveSkill.DoSkill();
    }
    
    public int EnumToInt(AllEnum.SkillName val)
    {
        switch (val)
        {
            case AllEnum.SkillName.AirSlash:
                return 1;
            case AllEnum.SkillName.AirCircle:
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
            case 1: return AllEnum.SkillName.AirSlash;
            case 2: return AllEnum.SkillName.AirCircle;
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
        if (skill.skillStat.inUse)
        {
            Debug.Log("�����");
            return;
        }
        else
        {
            if (GameManager.Instance.player.playerStat.mana >= skill.skillStat.mana)
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
                GameManager.Instance.player.playerStat.MinusMana(skill.skillStat.mana);
                UiManager.Instance.SetUseSKillCoolImg(skill.skillStat.index);
            }
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
                    skill.Init(skillInfo);
                    skill.skillStat.SetInUse(false);
                    //skill.SetInfo(skillInfo);

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

    public void SetSkillPos(Skill skill, Transform tr)
    {
        skill.transform.SetParent(tr);
    }
    public Skill SetSkillPos(Skill skill, Vector3 pos, Quaternion rot)
    {
        skill.transform.position = pos;
        skill.transform.rotation = rot;

        if (skill.skillStat.setParent)
        {
            skill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        return skill;
    }
    public Skill SetSkillPos(Skill skill, Vector3 pos)
    {
        skill.transform.position = pos;

        if (skill.skillStat.setParent)
        {
            skill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);
        }
        return skill;
    }

    //���� (�ʱ�ȭ�� ����ִ�)
    public void SetOffSkill(Skill skill)
    {
        skill.DoReset();
        skill.gameObject.SetActive(false);
    }




}
