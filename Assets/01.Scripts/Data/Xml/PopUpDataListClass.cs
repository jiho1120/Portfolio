using UnityEngine;
public class ItemGrade
{
    public string grade;
    public int money;
    public string color;
    public int percentage;

    public ItemGrade()
    {
    }
    public ItemGrade(string grade, string money, string color, string percentage)
    {
        this.grade = grade;
        this.money = int.Parse(money);
        this.color = color;
        this.percentage = int.Parse(percentage);
    }
    public void AddGrade(string grade)
    {
        this.grade = grade;

    }
    public void AddMoney(string money)
    {
        this.money = int.Parse(money);

    }
    public void AddColor(string color)
    {
        this.color = color;

    }
    public void AddPercentage(string percentage)
    {
        this.percentage = int.Parse(percentage);
    }


    public void ShowInfo()
    {
        Debug.Log($"grade: {grade}, money: {money}, color: {color},percentage: {percentage}");
    }
}
public class PowerUpPlayer
{
    public string grade;
    public string statName;
    public float powerUpSize;

    public PowerUpPlayer(string statName, string grade, string powerUpSize)
    {
        this.grade = grade;
        this.statName = statName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
    public void ShowInfo()
    {
        Debug.Log($"Skill: {statName}, Grade: {grade}, PowerUpSize: {powerUpSize}");
    }

}
public class PowerUpItem
{
    public string grade;
    public string itemName;
    public float powerUpSize;

    public PowerUpItem(string itemName, string grade, string powerUpSize)
    {
        this.grade = grade;
        this.itemName = itemName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
    public void ShowInfo()
    {
        Debug.Log($"Skill: {itemName}, Grade: {grade}, PowerUpSize: {powerUpSize}");
    }
}
public class PowerUpSkill
{
    public string grade;
    public string skillName;
    public float powerUpSize;

    public PowerUpSkill(string skillName, string grade, string powerUpSize)
    {
        this.grade = grade;
        this.skillName = skillName;
        this.powerUpSize = float.Parse(powerUpSize);
    }
    public void ShowInfo()
    {
        Debug.Log($"Skill: {skillName}, Grade: {grade}, PowerUpSize: {powerUpSize}");
    }
}
