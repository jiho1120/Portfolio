using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllEnum;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SkillManager : Singleton<SkillManager>
{
    // 스킬 오브젝트들을 관리하기위해 만듬
    Dictionary<ObjectType, Dictionary<SkillName, Skill>> skillDict = new Dictionary<ObjectType, Dictionary<SkillName, Skill>>();

    private void Start()
    {
        skillDict[ObjectType.Player] = new Dictionary<SkillName, Skill>();
        skillDict[ObjectType.Boss] = new Dictionary<SkillName, Skill>();
    }

    public void InstanceSkill()
    {
        for (int i = 0; i < (int)SkillName.End; i++)
        {
            SkillName skillName = (SkillName)i;
            Skill skillPrefab = ResourceManager.Instance.GetPrefab(DictName.SkillDict, skillName.ToString()).GetComponent<Skill>();

            CreateAndInitializeSkillInstance(ObjectType.Player, skillName, skillPrefab, GameManager.Instance.player.skillPos, "PlayerSkill", GameManager.Instance.player.EnemyLayerMask);

            if (skillName != SkillName.Gravity)
            {
                CreateAndInitializeSkillInstance(ObjectType.Boss, skillName, skillPrefab, GameManager.Instance.boss.skillPos, "BossSkill", GameManager.Instance.boss.EnemyLayerMask);
            }

        }
    }

    private void CreateAndInitializeSkillInstance(ObjectType objectType, SkillName skillName, Skill skillPrefab, Transform skillPosition, string layerName, int layer)
    {
        CreateSkillInstance(objectType, skillName, skillPrefab, skillPosition, layerName);
        skillDict[objectType][skillName].SetEnemyLayer(layer);
    }

    private void CreateSkillInstance(ObjectType objectType, SkillName skillName, Skill skillPrefab, Transform skillPosition, string layerName)
    {
        Skill skillInstance = Instantiate(skillPrefab, skillPosition);
        skillInstance.gameObject.layer = LayerMask.NameToLayer(layerName);
        skillInstance.gameObject.SetActive(false);
        skillDict[objectType][skillName] = skillInstance;
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
    //private SkillData GetSkillData(ObjectType objectType, SkillName skillName)
    //{
    //    return GetSkillDataDictionary(objectType)[skillName];
    //}
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
            foreach (var skillSlot in UIManager.Instance.uIPlayer.uiSkillSlots)
            {
                if (skillSlot.skillName == skillName)
                {
                    skillSlot.SetUseSkillTime();
                    break;
                }
            }
        }

        skill.Activate();
        StartCoroutine(ResetSkillAvailability(skill));
        caster.SetMp(caster.Stat.mp - skill.skilldata.mana);
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
        caster.SetPassiveCorNull();
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
