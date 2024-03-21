using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStat
{
    public int index { get; private set; }

    public int level { get; private set; }
    public int count { get; private set; }
    public AllEnum.ItemType itemType { get; private set; }

    public Sprite icon { get; private set; }
    public float health { get; private set; }
    public float mana { get; private set; }
    public float ultimateGauge { get; private set; }
    // 운 최체 치 공 최마 방 스피드
    public float luck { get; private set; }
    public float maxHealth { get; private set; }
    public float critical { get; private set; }
    public float attack { get; private set; }
    public float maxMana { get; private set; }
    public float defence { get; private set; }
    public float speed { get; private set; }



    public ItemStat(SOItem SOItem)
    {
        index = SOItem.index;
        level = SOItem.level;
        count = SOItem.count;
        icon = SOItem.icon;
        itemType = SOItem.itemType;
        health = SOItem.hp;
        mana = SOItem.mp;
        ultimateGauge = SOItem.ultimateGauge;
        luck = SOItem.luck;
        maxHealth = SOItem.maxHp;
        critical = SOItem.critical;
        attack = SOItem.attack;
        maxMana = SOItem.maxMp;
        defence = SOItem.defense;
        speed = SOItem.speed;
    }
    public void AddStat(AllEnum.ItemType propertyName, float effect)
    {
        switch (propertyName)
        {
            case AllEnum.ItemType.Head:
                luck += effect;
                break;
            case AllEnum.ItemType.Top:
                maxHealth += effect;
                break;
            case AllEnum.ItemType.Gloves:
                critical += effect;
                break;
            case AllEnum.ItemType.Weapon:
                attack += effect;
                break;
            case AllEnum.ItemType.Belt:
                maxMana += effect;
                break;
            case AllEnum.ItemType.Bottom:
                defence += effect;
                break;
            case AllEnum.ItemType.Shoes:
                speed += effect;
                break;
            default:
                Debug.LogError("Invalid property name: " + propertyName);
                break;
        }
    }
    
    public void AddLevel(int effect)
    {
        level += effect;
    }
    public void AddCount(int effect)
    {
        count += effect;
    }
    public void DisplayStats()
    {
        Debug.Log("Index: " + index);
        Debug.Log("Level: " + level);
        Debug.Log("Count: " + count);
        Debug.Log("ItemType: " + itemType);
        Debug.Log("Icon: " + icon);
        Debug.Log("Health: " + health);
        Debug.Log("Mana: " + mana);
        Debug.Log("Ultimate Gauge: " + ultimateGauge);
        Debug.Log("Luck: " + luck);
        Debug.Log("Max Health: " + maxHealth);
        Debug.Log("Critical: " + critical);
        Debug.Log("Attack: " + attack);
        Debug.Log("Max Mana: " + maxMana);
        Debug.Log("Defence: " + defence);
        Debug.Log("Speed: " + speed);
    }
}
