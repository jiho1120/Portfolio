using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>, ReInitialize
{
    public Skill passiveSkill { get; private set; }
    public Dictionary<AllEnum.SkillName, Skill> skillDict { get; private set; } // ��ų ���鶧 �̰� ���
    public Dictionary<AllEnum.SkillName, Skill> bossSkillDict { get; private set; }
    public Transform playerSKillPool;
    public Transform bossSKillPool;
    
    public void Init()
    {
        skillDict = new Dictionary<AllEnum.SkillName, Skill>();
        bossSkillDict = new Dictionary<AllEnum.SkillName, Skill>();
        Skill skilltmp;

        foreach (var item in ResourceManager.Instance.objectAll)
        {
            skilltmp = Instantiate(item, playerSKillPool).GetComponent<Skill>();
            skilltmp.gameObject.layer = LayerMask.NameToLayer("PlayerSkill");
            skilltmp.Init(ResourceManager.Instance.GetSkillData(skilltmp.Index));
            skilltmp.skillStat.SetInUse(false);
            skilltmp.gameObject.SetActive(false);
            skilltmp.isPlayer = true;
            skillDict.Add(IntToEnum(skilltmp.Index), skilltmp);
            if (skilltmp.skillStat.skillName != AllEnum.SkillName.Gravity)
            {
                skilltmp = Instantiate(item, bossSKillPool).GetComponent<Skill>();
                skilltmp.gameObject.layer = LayerMask.NameToLayer("BossSkill");
                skilltmp.Init(ResourceManager.Instance.GetSkillData(skilltmp.Index));
                skilltmp.skillStat.SetInUse(false);
                skilltmp.gameObject.SetActive(false);
                skilltmp.isPlayer = false;
                bossSkillDict.Add(IntToEnum(skilltmp.Index), skilltmp);
            }
        }
    }
    public void ReInit()
    {
        foreach(var item in skillDict)
        {
            item.Value.Init(item.Value.orgInfo);
        }
        foreach (var item in bossSkillDict)
        {
            item.Value.Init(item.Value.orgInfo);
        }
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    
    

    public PassiveSkill CallPassiveSkill(bool isPlayer)
    {
        if (isPlayer)
        {
            GameManager.Instance.player.PassiveCurrentNum++;
            if (GameManager.Instance.player.PassiveCurrentNum >= (int)AllEnum.SkillName.End)
            {
                GameManager.Instance.player.PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
            }

            passiveSkill = skillDict[(AllEnum.SkillName)GameManager.Instance.player.PassiveCurrentNum];
        }
        else
        {
            GameManager.Instance.boss.PassiveCurrentNum++;
            if (GameManager.Instance.boss.PassiveCurrentNum >= (int)AllEnum.SkillName.End)
            {
                GameManager.Instance.boss.PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
            }

            passiveSkill = bossSkillDict[(AllEnum.SkillName)GameManager.Instance.boss.PassiveCurrentNum];
        }

        passiveSkill.DoSkill(isPlayer);
        return passiveSkill.GetComponent<PassiveSkill>();
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
    public void UseActiveSKill(AllEnum.SkillName name, bool isPlayer)
    {
        Skill skill = GetSKillFromDict(name, isPlayer);
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        if (skill.skillStat.inUse)
        {
            Debug.Log("사용중");
            return;
        }
        else
        {
            if (isPlayer)
            {
                if (GameManager.Instance.player.Mp >= skill.skillStat.mana)
                {
                    pos = GameManager.Instance.player.transform.position;
                    rot = GameManager.Instance.player.transform.GetChild(0).rotation;
                    if (name == AllEnum.SkillName.Gravity)
                    {
                        Vector3 spawnOffset = rot * new Vector3(0, 0.5f, 1) * 10f;
                        rot = Quaternion.Euler(skill.transform.rotation.eulerAngles);
                        pos = pos + spawnOffset;
                    }
                    skill = SetSkillPos(skill, pos, rot, isPlayer);
                }
            }
            else
            {
                if (GameManager.Instance.boss.bossStat.mana >= skill.skillStat.mana)
                {
                    pos = GameManager.Instance.boss.transform.position;
                    rot = GameManager.Instance.boss.transform.GetChild(0).rotation;
                }
                skill = SetSkillPos(skill, pos, rot, isPlayer);
            }
            skill.gameObject.SetActive(true);
            skill.DoSkill(skill.isPlayer);
        }
    }
    public bool UseableSkill(AllEnum.SkillName name, bool isPlayer)
    {
        Skill skill = GetSKillFromDict(name, isPlayer);
        if (skill.skillStat.inUse)
        {
            Debug.Log("사용중");
            return false;
        }
        else if (GameManager.Instance.boss.bossStat.mana < skill.skillStat.mana)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Skill GetSKillFromDict(AllEnum.SkillName skillName, bool isPlayer = true)
    {
        Skill skill;
        if (isPlayer)
        {
            if (skillDict.ContainsKey(skillName))
            {
                skill = skillDict[skillName];
            }
            else
            {
                throw new System.Exception($"�÷��̾��� {skillName} ��ų�̾���");
            }
        }
        else
        {
            if (bossSkillDict.ContainsKey(skillName))
            {
                skill = bossSkillDict[skillName];
            }
            else
            {
                throw new System.Exception($"������ {skillName} ��ų�̾���");
            }
        }
        return skill;
    }

    public Skill SetSkillPos(Skill skill, Vector3 pos, Quaternion rot, bool isPlayer)
    {
        skill.transform.position = pos;
        skill.transform.rotation = rot;

        if (skill.skillStat.setParent)
        {
            if (isPlayer)
            {
                skill.transform.SetParent(GameManager.Instance.player.transform.GetChild(0), true);

            }
            else
            {
                skill.transform.SetParent(GameManager.Instance.boss.transform.GetChild(0), true);
            }
        }
        return skill;
    }
    public void SetSkillInUse(float cool, Skill skill)
    {
        StartCoroutine(SetSkillInUseTimer(cool, skill));
    }

    IEnumerator SetSkillInUseTimer(float cool, Skill skill)
    {
        yield return new WaitForSeconds(cool);
        skill.skillStat.SetInUse(false);
    }

   
}
