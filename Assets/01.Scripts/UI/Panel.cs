using System;
using UnityEngine;
using static AllEnum;

public class Panel : MonoBehaviour
{
    [SerializeField]
    public PanelType setType { get; private set; }
    string objName;
    float effect;
    public int SellMoney { get; private set; }
    public void SetPanelType(PanelType type)
    {
        setType = type;
    }
    public void SetSelldata(string _name, float _effect, int money)
    {
        objName = _name;
        effect = _effect;
        SellMoney = money;
    }
    public void ApplyValue()
    {
        if (setType == PanelType.PlayerPower)
        {
            ApplyPlayerPower();
        }
        else if (setType == PanelType.ItemPower)
        {
            ApplyItemPower();
        }
        else if (setType == PanelType.SkillPower)
        {
            ApplySkillPower();
        }
        else
        {
            Debug.Log("없는 것인데");
        }
    }

    void ApplyPlayerPower()
    {
        if (Enum.TryParse(objName, true, out StatList statList))
        {
            DataManager.Instance.gameData.playerData.playerStat.
            AddStatData(statList, effect);
            GameManager.Instance.player.AddStatData(statList, effect);
        }
        else
        {
            Debug.LogError($"Enum 파싱에 실패했습니다. 유효하지 않은 PlyerStat: ");
        }
    }
    void ApplyItemPower()
    {
        if (Enum.TryParse(objName, true, out ItemList itemList))
        {
            DataManager.Instance.gameData.invenDatas.AddEquipmentStat(itemList, effect);
            GameManager.Instance.player.AddEquipmentStat(itemList, effect);

        }
    }


    void ApplySkillPower()
    {
        if (Enum.TryParse(objName, true, out SkillName skillName))
        {
            DataManager.Instance.gameData.playerData.skillDict[skillName].effect += effect;
            SkillManager.Instance.GetPlayerSkill(skillName).SetSkillData(DataManager.Instance.gameData.playerData.skillDict[skillName]);

        }
        else
        {
            Debug.LogError($"Enum 파싱에 실패했습니다. 유효하지 않은 SkillName");
        }
    }


}
