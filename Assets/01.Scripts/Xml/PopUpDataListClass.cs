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

    public ItemGrade(string grade, string money, string color, string percentage)
    {
        this.grade = grade;
        this.money = int.Parse(money);
        this.color = color;
        this.percentage = int.Parse(percentage);
    }
}
public class PowerUpPlayer
{
    public string grade;
    public string statName;
    public float powerUpSize;

    public PowerUpPlayer(string statName, string grade,  string powerUpSize)
    {
        this.grade = grade;
        this.statName = statName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
   
    
}
public class PowerUpItem
{
    public string grade;
    public string itemName;
    public float powerUpSize;

    public PowerUpItem(string itemName, string grade,  string powerUpSize)
    {
        this.grade = grade;
        this.itemName = itemName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
}
public class PowerUpSkill
{
    public string grade;
    public string skillName;
    public float powerUpSize;

    public PowerUpSkill(string skillName, string grade,  string powerUpSize)
    {
        this.grade = grade;
        this.skillName = skillName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
}
