using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public Skill passiveSkill { get; private set; }
    //하나로 설정후 주인을 지정해주기
    public Dictionary<AllEnum.SkillName, Skill> skillDict { get; private set; } // 스킬 만들때 이거 사용
    public Dictionary<AllEnum.SkillName, Skill> bossSkillDict { get; private set; }


    public void Init()
    {
        skillDict = new Dictionary<AllEnum.SkillName, Skill>();
        bossSkillDict = new Dictionary<AllEnum.SkillName, Skill>();

        Skill skilltmp;

        foreach (var item in ResourceManager.Instance.objectAll)
        {
            skilltmp = Instantiate(item).GetComponent<Skill>();
            skilltmp.Init(ResourceManager.Instance.GetSkillData(skilltmp.Index));
            skilltmp.skillStat.SetInUse(false);
            skilltmp.gameObject.SetActive(false);
            skilltmp.isPlayer = true;
            skillDict.Add(IntToEnum(skilltmp.Index), skilltmp);

            skilltmp = Instantiate(item).GetComponent<Skill>();
            skilltmp.Init(ResourceManager.Instance.GetSkillData(skilltmp.Index));
            skilltmp.skillStat.SetInUse(false);
            skilltmp.gameObject.SetActive(false);
            skilltmp.isPlayer = false;
            bossSkillDict.Add(IntToEnum(skilltmp.Index), skilltmp);
        }
       
    }


    public void CallPassiveSkill(bool isPlayer)
    {
        if (isPlayer)
        {
            GameManager.instance.player.PassiveCurrentNum++;
            if (GameManager.instance.player.PassiveCurrentNum >= (int)AllEnum.SkillName.End) // 인덱스 넘기면 처음부터 시작
            {
                GameManager.instance.player.PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
            }

            passiveSkill = skillDict[(AllEnum.SkillName)GameManager.instance.player.PassiveCurrentNum];
        }
        else
        {
            GameManager.instance.boss.PassiveCurrentNum++;
            if (GameManager.instance.boss.PassiveCurrentNum >= (int)AllEnum.SkillName.End) // 인덱스 넘기면 처음부터 시작
            {
                GameManager.instance.boss.PassiveCurrentNum = (int)AllEnum.SkillName.Fire;
            }

            passiveSkill = bossSkillDict[(AllEnum.SkillName)GameManager.instance.boss.PassiveCurrentNum];
        }
        
        passiveSkill.DoSkill(isPlayer);
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
    public void UseSKill(AllEnum.SkillName name, bool isPlayer)
    {
        Skill skill = GetSKillFromDict(name, isPlayer);
        if (skill.skillStat.inUse)
        {
            Debug.Log("사용중");
            return;
        }
        else
        {
            if (GameManager.Instance.player.Mp >= skill.skillStat.mana)
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
                skill.DoSkill(skill.isPlayer);
            }
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
                throw new System.Exception( $"플레이어의 {skillName} 스킬이없음");
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
                throw new System.Exception($"보스의 {skillName} 스킬이없음");
            }
        }
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

    //끄기 (초기화를 담고있는)
    public void SetOffSkill(Skill skill)
    {
        skill.DoReset();
        skill.gameObject.SetActive(false);
    }


}
