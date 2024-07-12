using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllEnum;

public class SkillManager : Singleton<SkillManager>
{
    // 스킬 오브젝트들을 관리하기위해 만듬
    Dictionary<ObjectType, Dictionary<SkillName, Skill>> skillDict = new Dictionary<ObjectType, Dictionary<SkillName, Skill>>();

    private void Start()
    {
        
    }

    public void InitSkillDicts()
    {
        skillDict[ObjectType.Player] = new Dictionary<SkillName, Skill>();
        skillDict[ObjectType.Boss] = new Dictionary<SkillName, Skill>();
        for (int i = 0; i < (int)SkillName.End; i++)
        {
            SkillName skillName = (SkillName)i;
            Skill skillPrefab = ResourceManager.Instance.GetPrefab(DictName.SkillDict, skillName.ToString()).GetComponent<Skill>();


            Skill skill = GetInstantiatedSkill(skillPrefab, GameManager.Instance.player.skillPos);

            SetUpInstantiatedSkill(skill, "PlayerSkill", GameManager.Instance.player.EnemyLayerMask);
            AddInstantiatedSkillToDict(skill, ObjectType.Player, skillName);

            skill = GetInstantiatedSkill(skillPrefab, GameManager.Instance.boss.skillPos);

            if (skillName != SkillName.Gravity)
            {
                SetUpInstantiatedSkill(skill, "BossSkill", GameManager.Instance.boss.EnemyLayerMask);
                AddInstantiatedSkillToDict(skill, ObjectType.Boss, skillName);
            }
        }
    }


    public Skill GetInstantiatedSkill(Skill skillPrefab, Transform skillPosition)
    {
        Skill skillInstance = Instantiate(skillPrefab, skillPosition);
        return skillInstance;
    }

    private void SetUpInstantiatedSkill(Skill skill, string layerName, int layer)
    {
        skill.gameObject.layer = LayerMask.NameToLayer(layerName);
        skill.SetEnemyLayer(layer);
        skill.gameObject.SetActive(false);
    }

    public void AddInstantiatedSkillToDict(Skill skill, ObjectType objType, SkillName skillName)
    {
        skillDict[objType][skillName] = skill;
    }
    

    /// <summary>
    /// 스킬 데이터 바뀔 때마다 실행
    /// </summary>
    public void UpdateSkillData(HumanCharacter caster)
    {
        ObjectType objectType = GetCasterType(caster);
        if (objectType == ObjectType.End)
        {
            Debug.LogError("Invalid caster type");
            return;
        }

        var skillDataDictionary = GetSkillDataDictionary(objectType);

        foreach (var skill in skillDict[objectType])
        {
            skill.Value.SetSkillData(skillDataDictionary[skill.Key]);
        }
    }
    private Dictionary<SkillName, SkillData> GetSkillDataDictionary(ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.Player:
                return DataManager.Instance.gameData.playerData.skillDict;
            case ObjectType.Boss:
                return DataManager.Instance.gameData.bossData.skillDict;
            default:
                return null;
        }
    }

    public Skill GetSkill(HumanCharacter caster, SkillName skillName)
    {
        ObjectType objectType = GetCasterType(caster);
        if (skillDict.TryGetValue(objectType, out var skills) && skills.TryGetValue(skillName, out var skill))
        {
            return skill;
        }

        Debug.LogError($"Skill {skillName} not found for {objectType}");
        return null;
    }

    public Skill GetPlayerSkill(SkillName skillName)
    {
       return skillDict[ObjectType.Player][skillName];
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

    ObjectType GetCasterType(HumanCharacter caster)
    {
        if (caster is Player) return ObjectType.Player;
        if (caster is Boss) return ObjectType.Boss;
        return ObjectType.End;
    }


    public void UseSkill(HumanCharacter caster, SkillName skillName)
    {
        ActiveSkill skill = GetSkill(caster, skillName)?.GetComponent<ActiveSkill>();
        if (skill == null || !skill.CheckUsableSkill(caster))
        {
            Debug.Log("Skill is not usable");
            return;
        }
        if (caster is Player player)
        {
            if (skillName == SkillName.Gravity)
            {
                player.SetUltimate(0);
                UIManager.Instance.uIPlayer.SetUltimateUI();
            }
            else
            {
                foreach (var skillSlot in UIManager.Instance.uIPlayer.uiSkillSlots)
                {
                    if (skillSlot.skillName == skillName)
                    {
                        skillSlot.SetUseSkillTime();
                        caster.SetMp(caster.Stat.mp - skill.skilldata.mana);
                        break;
                    }
                }
            }
        }
        else
        {
            caster.SetMp(caster.Stat.mp - skill.skilldata.mana);
        }
        skill.Activate();
        StartCoroutine(ResetSkillAvailability(skill));
    }

    IEnumerator ResetSkillAvailability(ActiveSkill skill)
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
        caster.StopPassiveCor();
    }

    private SkillName GetRandomPassiveSkillName()
    {
        float randomValue = Random.value;
        if (randomValue < 0.25f) return SkillName.Love;
        if (randomValue < 0.5f) return SkillName.Fire;
        if (randomValue < 0.75f) return SkillName.Wind;
        return SkillName.Heal;
    }
    public void DeactivateAllSkills(HumanCharacter caster)
    {
        ObjectType objectType = GetCasterType(caster);

        if (skillDict.TryGetValue(objectType, out var skills))
        {
            foreach (var skill in skills.Values)
            {
                skill.Deactivate();
            }
        }
    }
}
