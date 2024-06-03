using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XMLAccess : MonoBehaviour
{
    public string[] dataList = new string[4] { "grade", "PowerUpPlayer", "PowerUpItem", "PowerUpSkill" };
    public List<ItemGrade> gradeList = new List<ItemGrade>();
    public List<PowerUpPlayer> powerUpPlayerList = new List<PowerUpPlayer>();
    public List<PowerUpItem> powerUpItemList = new List<PowerUpItem>();
    public List<PowerUpSkill> powerUpSkillList = new List<PowerUpSkill>();

    public void Init()
    {
        for (int i = 0; i < dataList.Length; i++)
        {
            // XML 파일 로드
            TextAsset xmlAsset = Resources.Load<TextAsset>($"PopUpData/{dataList[i]}");
            if (xmlAsset != null)
            {
                string xmlData = xmlAsset.text;
                ParseXml(i, xmlData);
            }
            else
            {
                Debug.LogError("PowerUpSkills.xml not found in Resources folder.");
            }
        }
    }

    private void ParseXml(int i, string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);
        XmlNodeList statNodes = xmlDoc.SelectNodes("//Stat");
        string tmp = "";
        List<string> list = new List<string>();

        foreach (XmlNode statNode in statNodes)
        {
            string name = statNode.Attributes["Name"].Value;
            string grade = statNode.Attributes["Rating"].Value;
            string powerUpSize = statNode.Attributes["Value"].Value;
            if (i == 0)
            {
                string exName = tmp;
                if (exName != name) // xml에 end라는 이름 추가 어쩔수없음(아니면 레전드가 안나옴) 안쓸려면 아예 따로만들어야함
                {
                    ItemGrade powerUpGrade = new ItemGrade();
                    if (list.Count != 0)
                    {
                        list.Add(exName);
                        powerUpGrade.AddMoney(list[0]);
                        powerUpGrade.AddColor(list[1]);
                        powerUpGrade.AddPercentage(list[2]);
                        powerUpGrade.AddGrade(list[3]);

                        list.Clear();
                        gradeList.Add(powerUpGrade);
                    }
                }
                list.Add(powerUpSize);
                tmp = name;
            }
            else if (i == 1)
            {
                PowerUpPlayer powerUpPlayer = new PowerUpPlayer(name, grade, powerUpSize);
                powerUpPlayerList.Add(powerUpPlayer);
            }
            else if (i == 2)
            {
                PowerUpItem powerUpItem = new PowerUpItem(name, grade, powerUpSize);
                powerUpItemList.Add(powerUpItem);
            }
            else if (i == 3)
            {
                PowerUpSkill powerUpSkill = new PowerUpSkill(name, grade, powerUpSize);
                powerUpSkillList.Add(powerUpSkill);
            }
        }

    }
    public ItemGrade Randomgrade()
    {
        if (gradeList != null)
        {
            int num = Random.Range(0, 101);
            int luck = num + (int)GameManager.Instance.player.Stat.luck; // 수 내림 적용
            int Range = gradeList[0].percentage;
            if (luck <= Range)
            {
                return gradeList[0];
            }
            else if (luck <= Range + gradeList[1].percentage)
            {
                return gradeList[1];
            }
            else if (luck <= Range + gradeList[1].percentage + gradeList[2].percentage)
            {
                return gradeList[2];
            }
            else { return gradeList[3]; }
        }
        else
        {
            Debug.Log("리스트가 없음");
            return null;
        }

    }
    public void ShowListInfo()
    {
        // 파싱된 데이터 사용
        foreach (ItemGrade skill in gradeList)
        {
            skill.ShowInfo();
        }
        // 파싱된 데이터 사용
        foreach (PowerUpPlayer skill in powerUpPlayerList)
        {
            skill.ShowInfo();
        }
        // 파싱된 데이터 사용
        foreach (PowerUpItem skill in powerUpItemList)
        {
            skill.ShowInfo();
        }
        // 파싱된 데이터 사용
        foreach (PowerUpSkill skill in powerUpSkillList)
        {
            skill.ShowInfo();
        }
    }
}