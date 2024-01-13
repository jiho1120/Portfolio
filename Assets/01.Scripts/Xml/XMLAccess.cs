using OpenCover.Framework.Model;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLAccess : MonoBehaviour
{
    private List<PowerUpSkill> powerUpSkills;

    void Start()
    {
        powerUpSkills = new List<PowerUpSkill>();

        // XML 파일 로드
        TextAsset xmlAsset = Resources.Load<TextAsset>("PopUpData/PowerUpSkill");
        if (xmlAsset != null)
        {
            string xmlData = xmlAsset.text;
            ParseXml(xmlData);
        }
        else
        {
            Debug.LogError("PowerUpSkills.xml not found in Resources folder.");
        }
    }

    private void ParseXml(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);

        XmlNodeList statNodes = xmlDoc.SelectNodes("//Stat");

        foreach (XmlNode statNode in statNodes)
        {
            string skillName = statNode.Attributes["Name"].Value;
            string grade = statNode.Attributes["Rating"].Value;
            string powerUpSize = statNode.Attributes["Value"].Value;

            PowerUpSkill powerUpSkill = new PowerUpSkill(grade, skillName, powerUpSize);
            powerUpSkills.Add(powerUpSkill);
        }

        // 파싱된 데이터 사용
        foreach (PowerUpSkill skill in powerUpSkills)
        {
            Debug.Log($"Skill: {skill.skillName}, Grade: {skill.grade}, PowerUpSize: {skill.powerUpSize}");
        }
    }
}