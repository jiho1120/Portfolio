using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int killGoal = 10;
    public float gameRound = 1;
    public float gameStage = 1;
    public PlayerData playerData = new PlayerData();
    public MonsterData monsterData = new MonsterData();
    public BossData bossData = new BossData();
    public List<SkillData> activeSkillData = new List<SkillData>();
    public List<SkillData> passiveSkillData = new List<SkillData>();
    public InvenData invenDatas = new InvenData();


    public void SetGameData()
    {
        playerData.playerStat.name = UIManager.Instance.newPlayerName.text; // �Է��� �̸��� �����ؿ�
        playerData.playerStat.SetStat(DataManager.Instance.SOPlayerStat);
        monsterData.monsterStat.SetStat(DataManager.Instance.SOMonsterStat);
        bossData.bossStat.SetStat(DataManager.Instance.SOBossStat);
        for (int i = (int)AllEnum.ItemList.Head; i < (int)AllEnum.ItemList.End; i++)
        {
            ItemData item = new ItemData();
            invenDatas.EquipItemDatas.Add((AllEnum.ItemList)i, item);
        }
        //for (int i = 0; i < DataManager.Instance.activeSkill.Length; i++)
        //{
        //    SkillData skill = new SkillData();
        //    skill.SetSkillData(DataManager.Instance.activeSkill[i]);
        //    activeSkillData.Add(skill);
        //}
        //for (int i = 0; i < DataManager.Instance.passiveSkill.Length; i++)
        //{
        //    SkillData skill = new SkillData();
        //    skill.SetSkillData(DataManager.Instance.passiveSkill[i]);
        //    passiveSkillData.Add(skill);
        //}
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
    public void PrintStatData()
    {
        System.Reflection.FieldInfo[] fields = typeof(StatData).GetFields();
        foreach (var field in fields)
        {
            Debug.Log(field.Name + ": " + field.GetValue(this));
        }
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
    public int index = -1;
    public int level = 0; // �� �÷��� �ɷ�ġ �ø��� �Լ� ���鲨��
    public int count = 0;
    public AllEnum.ItemType itemType = AllEnum.ItemType.End;
    public AllEnum.ItemList itemList = AllEnum.ItemList.End;

    [JsonConverter(typeof(SpriteConverter))]
    public Sprite icon;

    public float hp = 0;
    public float mp = 0;
    public float ultimateGauge = 0;
    public float defense = 0;
    public float maxHp = 0;
    public float luck = 0;
    public float attack = 0;
    public float critical = 0;
    public float maxMp = 0;
    public float speed = 0;

    public ItemData()
    {
        index = -1;
        level = 0;
        count = 0;
        itemType = AllEnum.ItemType.End;
        itemList = AllEnum.ItemList.End;
        icon = null;
        hp = 0;
        mp = 0;
        ultimateGauge = 0;
        defense = 0;
        maxHp = 0;
        luck = 0;
        attack = 0;
        critical = 0;
        maxMp = 0;
        speed = 0;
    }

    public void SetItemData(SOItem SO)
    {
        index = SO.index;
        level = SO.level;
        count = SO.count;
        itemType = SO.itemType;
        itemList = SO.itemList;
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

[System.Serializable]
public class InvenData
{
    public List<ItemData> invenItemDatas = new List<ItemData>();
    public Dictionary<AllEnum.ItemList, ItemData> EquipItemDatas = new Dictionary<AllEnum.ItemList, ItemData>();

    public ItemData GetItemDataForIndex(int index)
    {
        foreach (ItemData item in invenItemDatas)
        {
            if (item.index == index)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowDIc()
    {
        foreach (var item in EquipItemDatas)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value);
        }
    }
}



