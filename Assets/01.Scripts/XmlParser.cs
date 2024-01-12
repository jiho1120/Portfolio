using System;
using System.Xml;
using UnityEngine;
public class XmlParser : MonoBehaviour
{
    void Start()
    {
        // XML ���� ���
        string xmlFilePath = "Assets/Resources/PopUpdata/YourData.xmlC";

        // XML ������ �о ������ �Ľ�
        ParseXmlFile(xmlFilePath);
    }

    void ParseXmlFile(string filePath)
    {
        try
        {
            // XmlDocument ����
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // ��� ���� �Ľ�
            XmlNodeList gradeNodes = xmlDoc.SelectNodes("/Root/GradeInfo");
            foreach (XmlNode gradeNode in gradeNodes)
            {
                string grade = gradeNode.SelectSingleNode("Grade").InnerText;
                int money = int.Parse(gradeNode.SelectSingleNode("Money").InnerText);
                string color = gradeNode.SelectSingleNode("Color").InnerText;

                Debug.Log($"Grade: {grade}, Money: {money}, Color: {color}");
            }

            // �ɷ�ġ ���� �Ľ�
            XmlNodeList statNodes = xmlDoc.SelectNodes("/Root/Stats/Stat");
            foreach (XmlNode statNode in statNodes)
            {
                string statName = statNode.Attributes["name"].Value;
                foreach (XmlNode gradeNode in statNode.ChildNodes)
                {
                    string grade = gradeNode.Name;
                    float value = float.Parse(gradeNode.InnerText);

                    Debug.Log($"Stat: {statName}, Grade: {grade}, Value: {value}");
                }
            }

            // ������ ���� �Ľ�
            XmlNodeList itemNodes = xmlDoc.SelectNodes("/Root/Items/Item");
            foreach (XmlNode itemNode in itemNodes)
            {
                string itemType = itemNode.Attributes["type"].Value;
                foreach (XmlNode gradeNode in itemNode.ChildNodes)
                {
                    string grade = gradeNode.Name;
                    float value = float.Parse(gradeNode.InnerText);

                    Debug.Log($"Item: {itemType}, Grade: {grade}, Value: {value}");
                }
            }

            // ��ų ���� �Ľ�
            XmlNodeList skillNodes = xmlDoc.SelectNodes("/Root/Skills/Skill");
            foreach (XmlNode skillNode in skillNodes)
            {
                string skillName = skillNode.Attributes["name"].Value;
                foreach (XmlNode gradeNode in skillNode.ChildNodes)
                {
                    string grade = gradeNode.Name;
                    float value = float.Parse(gradeNode.InnerText);

                    Debug.Log($"Skill: {skillName}, Grade: {grade}, Value: {value}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing XML file: {e.Message}");
        }
    }
}
