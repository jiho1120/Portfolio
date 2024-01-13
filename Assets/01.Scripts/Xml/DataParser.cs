using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class DataParser : MonoBehaviour
{
    public string[] dataList = new string[4] { "grade", "PowerUpItem", "PowerUpPlayer", "PowerUpSkill" };
    public PopUpDataListClass ParseXmlData(string xmlData)
    {
        PopUpDataListClass dataList = new PopUpDataListClass();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);

        // ItemGrade µ¥ÀÌÅÍ ÆÄ½Ì
        XmlNodeList gradeNodes = xmlDoc.SelectNodes("//GradeList/Grade");
        foreach (XmlNode gradeNode in gradeNodes)
        {
            string grade = gradeNode.Attributes["grade"].Value;
            string money = gradeNode.Attributes["money"].Value;
            string color = gradeNode.Attributes["color"].Value;
            string percentage = gradeNode.Attributes["percentage"].Value;

            ItemGrade itemGrade = new ItemGrade(grade, money, color, percentage);
            dataList.GradeList.Add(itemGrade);
        }

        // PowerUpPlayer µ¥ÀÌÅÍ ÆÄ½Ì
        XmlNodeList playerNodes = xmlDoc.SelectNodes("//powerUpPlayerList/PowerUpPlayer");
        foreach (XmlNode playerNode in playerNodes)
        {
            string statName = playerNode.Attributes["statName"].Value;
            string grade = playerNode.Attributes["grade"].Value;
            string powerUpSize = playerNode.Attributes["powerUpSize"].Value;

            PowerUpPlayer powerUpPlayer = new PowerUpPlayer(statName, grade, powerUpSize);
            dataList.powerUpPlayerList.Add(powerUpPlayer);
        }

        // PowerUpItem µ¥ÀÌÅÍ ÆÄ½Ì
        XmlNodeList itemNodes = xmlDoc.SelectNodes("//powerUpItemList/PowerUpItem");
        foreach (XmlNode itemNode in itemNodes)
        {
            string itemName = itemNode.Attributes["itemName"].Value;
            string grade = itemNode.Attributes["grade"].Value;
            string powerUpSize = itemNode.Attributes["powerUpSize"].Value;

            PowerUpItem powerUpItem = new PowerUpItem(itemName, grade, powerUpSize);
            dataList.powerUpItemList.Add(powerUpItem);
        }

        // PowerUpSkill µ¥ÀÌÅÍ ÆÄ½Ì
        XmlNodeList skillNodes = xmlDoc.SelectNodes("//powerUpSkillList/PowerUpSkill");
        foreach (XmlNode skillNode in skillNodes)
        {
            string skillName = skillNode.Attributes["skillName"].Value;
            string grade = skillNode.Attributes["grade"].Value;
            string powerUpSize = skillNode.Attributes["powerUpSize"].Value;

            PowerUpSkill powerUpSkill = new PowerUpSkill(skillName, grade, powerUpSize);
            dataList.powerUpSkillList.Add(powerUpSkill);
        }

        return dataList;
    }

    void Start()
    {
            TextAsset xmlAsset = Resources.Load<TextAsset>("PopUpData");
            if (xmlAsset != null)
            {
                string xmlData = xmlAsset.text;
                PopUpDataListClass parsedData = ParseXmlData(xmlData);

                // ÆÄ½ÌµÈ µ¥ÀÌÅÍ »ç¿ë
                foreach (ItemGrade grade in parsedData.GradeList)
                {
                    Debug.Log($"Grade: {grade.grade}, Money: {grade.money}, Color: {grade.color}, Percentage: {grade.percentage}");
                }

                foreach (PowerUpPlayer player in parsedData.powerUpPlayerList)
                {
                    Debug.Log($"Player StatName: {player.statName}, Grade: {player.grade}, PowerUpSize: {player.powerUpSize}");
                }

                foreach (PowerUpItem item in parsedData.powerUpItemList)
                {
                    Debug.Log($"Item Name: {item.itemName}, Grade: {item.grade}, PowerUpSize: {item.powerUpSize}");
                }

                foreach (PowerUpSkill skill in parsedData.powerUpSkillList)
                {
                    Debug.Log($"Skill Name: {skill.skillName}, Grade: {skill.grade}, PowerUpSize: {skill.powerUpSize}");
                }
            }
    }
}
