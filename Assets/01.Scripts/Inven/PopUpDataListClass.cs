using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopUpDataListClass
{
    public List<ItemLating> latingList = new List<ItemLating>();
    public List<PowerUpPlayer> powerUpPlayerList = new List<PowerUpPlayer>();
    public List<PowerUpItem> powerUpItemList = new List<PowerUpItem>();
    public List<PowerUpSkill> powerUpSkillList = new List<PowerUpSkill>();

}
public class ItemLating
{
    public string lating;
    public int money;
    public string color;
    public int percentage;

    public ItemLating(string lating, int money, string color, int percentage)
    {
        this.lating = lating;
        this.money = money;
        this.color = color;
        this.percentage = percentage;
    }
}
public class PowerUpPlayer
{
    public string lating;
    public string ItemName;
    public float powerUpSize;

    public PowerUpPlayer(string lating, string itemName, float powerUpSize)
    {
        this.lating = lating;
        ItemName = itemName;
        this.powerUpSize = powerUpSize;
    }
}
public class PowerUpItem
{
    public string lating;
    public string itemName;
    public float powerUpSize;

    public PowerUpItem(string lating, string itemName, float powerUpSize)
    {
        this.lating = lating;
        this.itemName = itemName;
        this.powerUpSize = powerUpSize;
    }
}
public class PowerUpSkill
{
    public string lating;
    public string SkillName;
    public float powerUpSize;

    public PowerUpSkill(string lating, string skillName, float powerUpSize)
    {
        this.lating = lating;
        SkillName = skillName;
        this.powerUpSize = powerUpSize;
    }
}
