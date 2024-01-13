using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopUpDataListClass
{
    public List<ItemGrade> GradeList = new List<ItemGrade>();
    public List<PowerUpPlayer> powerUpPlayerList = new List<PowerUpPlayer>();
    public List<PowerUpItem> powerUpItemList = new List<PowerUpItem>();
    public List<PowerUpSkill> powerUpSkillList = new List<PowerUpSkill>();

}
public class ItemGrade
{
    public string grade;
    public int money;
    public string color;
    public int percentage;

    public ItemGrade(string grade, int money, string color, int percentage)
    {
        this.grade = grade;
        this.money = money;
        this.color = color;
        this.percentage = percentage;
    }
}
public class PowerUpPlayer
{
    public string grade;
    public string statName;
    public float powerUpSize;

    public PowerUpPlayer(string grade, string statName, float powerUpSize)
    {
        this.grade = grade;
        this.statName = statName;
        this.powerUpSize = powerUpSize;
    }
}
public class PowerUpItem
{
    public string grade;
    public string itemName;
    public float powerUpSize;

    public PowerUpItem(string grade, string itemName, float powerUpSize)
    {
        this.grade = grade;
        this.itemName = itemName;
        this.powerUpSize = powerUpSize;
    }
}
public class PowerUpSkill
{
    public string grade;
    public string SkillName;
    public float powerUpSize;

    public PowerUpSkill(string grade, string skillName, float powerUpSize)
    {
        this.grade = grade;
        SkillName = skillName;
        this.powerUpSize = powerUpSize;
    }
}
