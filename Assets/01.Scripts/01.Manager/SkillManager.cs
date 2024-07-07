using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class SkillManager : Singleton<SkillManager>
{
    // 스킬 오브젝트들을 관리하기위해 만듬
    Dictionary<ObjectType, Dictionary<SkillName, Skill>> SkillDict = new Dictionary<ObjectType, Dictionary<SkillName, Skill>>();

    protected override void Awake()
    {
        base.Awake();
        SkillDict[ObjectType.Player] = new Dictionary<SkillName, Skill>();
        SkillDict[ObjectType.Boss] = new Dictionary<SkillName, Skill>();
    }
    public void Init()
    {
        Transform skillTr;
        Skill skillInstance;
        for (int i = 0; i < (int)SkillName.End; i++)
        {
            SkillName skillName = (SkillName)i;

            //Prefab과 인스턴스 구분: Init 메서드에서 Instantiate를 사용하여 스킬 인스턴스를 생성하고 이를 SkillDict에 저장. 이렇게 하면 SkillDict에 저장된 스킬과 실제 사용하는 스킬 인스턴스가 동일하게 됩니다.
            Skill skillPrefab = ResourceManager.Instance.GetPrefab(DictName.SkillDict, skillName.ToString()).GetComponent<Skill>();

            skillTr = GameManager.Instance.player.skillPos;
            skillInstance = Instantiate(skillPrefab, skillTr);
            skillInstance.gameObject.layer = LayerMask.NameToLayer("PlayerSkill");
            skillInstance.gameObject.SetActive(false);
            SkillDict[ObjectType.Player][skillName] = skillInstance;

            if (skillName != SkillName.Gravity)
            {
                // Boss용 코드
                skillTr = GameManager.Instance.boss.skillPos;
                skillInstance = Instantiate(skillPrefab, skillTr);
                skillInstance.gameObject.layer = LayerMask.NameToLayer("BossSkill");
                skillInstance.gameObject.SetActive(false);
                SkillDict[ObjectType.Boss][skillName] = skillInstance;
            }
        }
    }

    /// <summary>
    /// 데이터 설정은 게임 시작하고 플레이 하면 한번 넣어주기
    /// </summary>
    public void SetSkillData(ObjectType objectType)
    {
        int layerMask;
        Dictionary<SkillName, Skill> dict = SkillDict[objectType];

        var skillDataDict = GetSkillDataDict(objectType);
        if (objectType == ObjectType.Player)
        {
            layerMask = GameManager.Instance.player.EnemyLayerMask;
        }
        else
        {
            layerMask = GameManager.Instance.boss.EnemyLayerMask;
        }


        foreach (var item in dict)
        {
            if (item.Key == SkillName.Gravity && layerMask == GameManager.Instance.boss.EnemyLayerMask)
            {
                continue;
            }
            item.Value.Init(skillDataDict[item.Key], layerMask);
        }
    }
    private Dictionary<SkillName, SkillData> GetSkillDataDict(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.Player:
                return DataManager.Instance.gameData.playerData.skillDict;
            case ObjectType.Boss:
                return DataManager.Instance.gameData.bossData.skillDict;
            default:
                return new Dictionary<SkillName, SkillData>();
        }
    }

    public int ChangeNameToIndex(SkillName name)
    {
        switch (name)
        {
            case SkillName.AirSlash: return 1;
            case SkillName.AirCircle: return 2;
            case SkillName.Ground: return 3;
            case SkillName.Gravity: return 4;
            case SkillName.Fire: return 101;
            case SkillName.Heal: return 102;
            case SkillName.Love: return 103;
            case SkillName.Wind: return 104;
            case SkillName.End: return 0;
            default:
                Debug.LogError($"Invalid SkillName: {name}");
                return -1;
        }
    }
    ObjectType GetCaster(HumanCharacter caster)
    {
        ObjectType objType;

        if (caster is Player)
        {
            objType = ObjectType.Player;
        }
        else if (caster is Boss)
        {
            objType = ObjectType.Boss;
        }
        else
        {
            objType = ObjectType.End;
        }
        return objType;
    }
    public Skill GetSkill(HumanCharacter caster, SkillName skillName)
    {
        ObjectType objType = GetCaster(caster);
        if (SkillDict.TryGetValue(objType, out var skills) && skills.TryGetValue(skillName, out var skill))
        {
            return skill;
        }

        Debug.LogError($"Skill {skillName} not found for {objType}");
        return null;
    }

    public void UseSkill(HumanCharacter caster, SkillName skillName)
    {
        ActiveSkill skill = GetSkill(caster, skillName)?.GetComponent<ActiveSkill>();
        if (skill.CheckUsableSkill(caster))
        {
            if (caster is Player)
            {
                for (int i = 0; i < UIManager.Instance.uIPlayer.uiSkillSlots.Length; i++)
                {
                    if (UIManager.Instance.uIPlayer.uiSkillSlots[i].skillName == skillName)
                    {
                        UIManager.Instance.uIPlayer.uiSkillSlots[i].SetUseSKillTime();
                    }
                }

            }
            
            skill.Activate();
            StartCoroutine(SetIsAvailable(skill));
            caster.SetMp(caster.Stat.mp - skill.skilldata.mana);
        }
        else
        {
            Debug.Log(skill.IsAvailable + "가능한지 변수");
        }
    }

    IEnumerator SetIsAvailable(ActiveSkill skill)
    {
        yield return new WaitForSeconds(skill.skilldata.cool);
        skill.IsAvailable = true;
    }

    public IEnumerator StartPassiveCor(HumanCharacter caster)
    {
        while (GameManager.Instance.stageStart)
        {
            SkillName skillName = GetRandomPassiveSkillName();
            Skill skill = GetSkill(caster, skillName);
            skill.Activate();
            yield return new WaitForSeconds(skill.skilldata.duration);
            skill.Deactivate();

        }
        caster.SetPassiveCorNull();
    }

    public SkillName GetRandomPassiveSkillName()
    {
        SkillName skillName;
        if (Random.value < 0.25)
        {
            skillName = SkillName.Love;
        }
        else if (Random.value < 0.5)
        {
            skillName = SkillName.Fire;

        }
        else if (Random.value < 0.75)
        {
            skillName = SkillName.Wind;
        }
        else
        {
            skillName = SkillName.Heal;
        }

        return skillName;

    }

    public void AllSKillDeactive(HumanCharacter caster)
    {
        ObjectType objType = GetCaster(caster);

        SkillDict.TryGetValue(objType, out var skills);
        for (int i = 0; i < 10; i++)
        {
            if (skills.TryGetValue((SkillName)i, out var skill))
            {
                if (skill != null)
                {
                    skill.Deactivate();
                }
            }
        }
    }

}
