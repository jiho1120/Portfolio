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
        playerData.playerStat.name = NewUIManager.Instance.newPlayerName.text; // �Է��� �̸��� �����ؿ�
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
    // �̸�, ����, ����, �������� ����
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
    public int level; // �� �÷��� �ɷ�ġ �ø��� �Լ� ���鲨��
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
    public int index; // ������ȣ
    public AllEnum.NewSkillType newSkillType; // ��ų Ÿ��
    public AllEnum.SkillName skillName; // ��ų �̸�
    public Sprite icon; // �׸�
    public float effect; // ȿ�� �����̸� ���ݷ� ���̸� ���ϴ¾� ... 
    public float duration; // ��ų ���� �ð�
    public float cool; // ��Ÿ��
    public float mana; // �Ҹ� ����
    public bool setParent; // ��ų�� �÷��̾ ����ٴ���
    public bool inUse; //false�Ͻ� ��ų���� 

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

