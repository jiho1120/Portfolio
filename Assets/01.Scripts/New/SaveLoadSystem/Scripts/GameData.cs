using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerData playerData = new PlayerData();
    public MonsterData monsterData = new MonsterData();
    public BossData bossData = new BossData();
    public List<ItemData> posionData = new List<ItemData>();
    public List<ItemData> equipmentData = new List<ItemData>();
    public List<SkillData> activeSkillData = new List<SkillData>();
    public List<SkillData> passiveSkillData = new List<SkillData>();

    public void SetGameData()
    {
        playerData.playerStat.name = NewUIManager.Instance.newPlayerName.text; // 입력한 이름을 복사해옴
        playerData.playerStat.SetStat(DataManager.Instance.SOPlayerStat);
        monsterData.monsterStat.SetStat(DataManager.Instance.SOMonsterStat);
        bossData.bossStat.SetStat(DataManager.Instance.SOBossStat);
        for (int i = 0; i < DataManager.Instance.Posion.Length; i++)
        {
            ItemData item = new ItemData();
            item.SetItemData(DataManager.Instance.Posion[i]);
            posionData.Add(item);
        }
        for (int i = 0; i < DataManager.Instance.Equipment.Length; i++)
        {
            ItemData item = new ItemData();
            item.SetItemData(DataManager.Instance.Equipment[i]);
            equipmentData.Add(item);
        }
        for (int i = 0; i < DataManager.Instance.activeSkill.Length; i++)
        {
            SkillData skill = new SkillData();
            skill.SetSkillData(DataManager.Instance.activeSkill[i]);
            activeSkillData.Add(skill);
        }
        for (int i = 0; i < DataManager.Instance.passiveSkill.Length; i++)
        {
            SkillData skill = new SkillData();
            skill.SetSkillData(DataManager.Instance.passiveSkill[i]);
            passiveSkillData.Add(skill);
        }
    }
}

[System.Serializable]
public class StatData
{
    // 이름, 레벨, 코인, 착용중인 무기
    public string name;
    public AllEnum.ObjectType objectType;
    public int level;
    public float hp;
    public float maxHp;
    public float attack;
    public float defense;
    public float critical;
    public float speed;
    public float experience;
    public int money;
    public float mp;
    public float maxMp;
    public float luck;
    public float maxExperience;
    public float ultimateGauge;
    public float maxUltimateGauge;

    public void SetStat(SOStat SO)
    {
        objectType = SO.objectType;
        level = SO.level;
        hp = SO.health;
        maxHp = SO.maxHealth;
        attack = SO.attack;
        defense = SO.defense;
        critical = SO.criticalChance;
        speed = SO.movementSpeed;
        experience = SO.experience;
        money = SO.money;
        mp = SO.mana;
        maxMp = SO.maxMana;
        luck = SO.luck;
        maxExperience = SO.maxExperience;
        ultimateGauge = SO.ultimateGauge;
        maxUltimateGauge = SO.ultimateGauge;
    }
}
[System.Serializable]
public class PlayerData
{
    public StatData playerStat = new StatData();
}

[System.Serializable]
public class MonsterData
{
    public StatData monsterStat = new StatData();
}

[System.Serializable]
public class BossData
{
    public StatData bossStat = new StatData();
}

[System.Serializable]
public class ItemData
{
    public int index;
    public int level; // 을 올려서 능력치 올리는 함수 만들꺼임
    public int count;
    public AllEnum.ItemType itemType;
    public Sprite icon;
    public float hp;
    public float mp;
    public float ultimateGauge;
    public float defense;
    public float maxHp;
    public float luck;
    public float attack;
    public float critical;
    public float maxMp;
    public float speed;

    public void SetItemData(SOItem SO)
    {
        index = SO.index;
        level = SO.level;
        count = SO.count;
        itemType = SO.itemType;
        icon = SO.icon;
        hp = SO.hp;
        mp = SO.mp;
        ultimateGauge = SO.ultimateGauge;
        defense = SO.defense;
        maxHp = SO.maxHp;
        luck = SO.luck;
        attack = SO.attack;
        critical = SO.critical;
        maxMp = SO.maxMp;
        speed = SO.speed;
    }
}

[System.Serializable]
public class SkillData
{
    public int index; // 고유번호
    public AllEnum.NewSkillType newSkillType; // 스킬 타입
    public AllEnum.SkillName skillName; // 스킬 이름
    public Sprite icon; // 그림
    public float effect; // 효과 공격이면 공격력 힐이면 힐하는양 ... 
    public float duration; // 스킬 지속 시간
    public float cool; // 쿨타임
    public float mana; // 소모 마나
    public bool setParent; // 스킬이 플레이어를 따라다닐지
    public bool inUse; //false일시 스킬나감 

    public void SetSkillData(NewSOSkill SO)
    {
        index = SO.index;
        newSkillType = SO.newSkillType;
        skillName = SO.skillName;
        icon = SO.icon;
        effect = SO.effect;
        duration = SO.duration;
        cool = SO.cool;
        mana = SO.mana;
        setParent = SO.setParent;
        inUse = SO.inUse;
    }
}

