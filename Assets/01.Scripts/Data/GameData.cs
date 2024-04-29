using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public class GameData
{
    public int killGoal = 10;
    public float gameRound = 1;
    public float gameStage = 1;
    public PlayerData playerData = new PlayerData();
    public MonsterData monsterData = new MonsterData();
    public BossData bossData = new BossData();
    public InvenData invenDatas = new InvenData();


    public void SetGameData()
    {
        playerData.name = UIManager.Instance.newPlayerName.text; // 입력한 이름을 복사해옴
        playerData.playerStat.SetStat(DataManager.Instance.SOPlayerStat);
        monsterData.monsterStat.SetStat(DataManager.Instance.SOMonsterStat);
        bossData.bossStat.SetStat(DataManager.Instance.SOBossStat);
        for (int i = 0; i < (int)AllEnum.ItemList.End; i++)
        {
            ItemData item = new ItemData();
            if (i < (int)AllEnum.ItemList.Head)
            {
                invenDatas.PosionItemDatas.Add((AllEnum.ItemList)i, item);
            }
            else if (i >= (int)AllEnum.ItemList.Head)
            {
                invenDatas.EquipItemDatas.Add((AllEnum.ItemList)i, item);
            }
            
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
    // 이름, 레벨, 코인, 착용중인 무기
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

    public StatData()
    {
    }
    public StatData(StatData SO)
    {
        objectType = SO.objectType;
        level = SO.level;
        hp = SO.hp;
        maxHp = SO.maxHp;
        attack = SO.attack;
        defense = SO.defense;
        critical = SO.critical;
        speed = SO.speed;
        experience = SO.experience;
        money = SO.money;
        mp = SO.mp;
        maxMp = SO.maxMp;
        luck = SO.luck;
        maxExperience = SO.maxExperience;
        ultimateGauge = SO.ultimateGauge;
        maxUltimateGauge = SO.ultimateGauge;
    }

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
    public string name;
    public StatData playerStat = new StatData();
    public List<SkillData> activeSkill = new List<SkillData>();
    public List<SkillData> passiveSkill = new List<SkillData>();
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
    public List<SkillData> activeSkill = new List<SkillData>();
    public List<SkillData> passiveSkill = new List<SkillData>();
}

[System.Serializable]
public class ItemData
{
    public int index = -1;
    public int level = 0; // 을 올려서 능력치 올리는 함수 만들꺼임
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
    public ItemData(ItemData Item)
    {
        index = Item.index;
        level = Item.level;
        count = Item.count;
        itemType = Item.itemType;
        itemList = Item.itemList;
        icon = Item.icon;
        hp = Item.hp;
        mp = Item.mp;
        ultimateGauge = Item.ultimateGauge;
        defense = Item.defense;
        maxHp = Item.maxHp;
        luck = Item.luck;
        attack = Item.attack;
        critical = Item.critical;
        maxMp = Item.maxMp;
        speed = Item.speed;
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
    public void SetItemData(ItemData SO)
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
    public ItemData GetRandomItemData()
    {
        int randamId;
        ItemData item = new ItemData();
        randamId = Random.Range(0, (int)AllEnum.ItemList.End);
        item.SetItemData(DataManager.Instance.soItem[randamId]);
        return item;
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

[System.Serializable]
public class InvenData
{
    public List<ItemData> invenItemDatas = new List<ItemData>();
    public Dictionary<AllEnum.ItemList, ItemData> EquipItemDatas = new Dictionary<AllEnum.ItemList, ItemData>();
    public Dictionary<AllEnum.ItemList, ItemData> PosionItemDatas = new Dictionary<AllEnum.ItemList, ItemData>();

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
        foreach (KeyValuePair<AllEnum.ItemList, ItemData> pair in EquipItemDatas)
        {
            Debug.Log("Item List: " + pair.Key);
            ItemData itemData = pair.Value;

            foreach (System.Reflection.FieldInfo fieldInfo in typeof(ItemData).GetFields())
            {
                Debug.Log(fieldInfo.Name + ": " + fieldInfo.GetValue(itemData));
            }
        }
    }
}



