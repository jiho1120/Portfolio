using System;
using System.Collections;
using System.Collections.Generic;
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
        if (Enum.TryParse(objName, true, out StatList value))
        {
            switch (value)
            {
                case StatList.maxHealth:
                    GameManager.Instance.player.Stat.maxHp += effect;
                    break;
                case StatList.attack:
                    GameManager.Instance.player.Stat.attack += effect;
                    break;
                case StatList.defense:
                    GameManager.Instance.player.Stat.defense += effect;
                    break;
                case StatList.criticalChance:
                    GameManager.Instance.player.Stat.critical += effect;
                    break;
                case StatList.movementSpeed:
                    GameManager.Instance.player.Stat.speed += effect;
                    break;
                case StatList.experience:
                    GameManager.Instance.player.Stat.experience += effect;
                    break;
                case StatList.maxMana:
                    GameManager.Instance.player.Stat.maxMp += effect;
                    break;
                case StatList.luck:
                    GameManager.Instance.player.Stat.luck += effect;
                    break;
                case StatList.maxUltimateGauge:
                    GameManager.Instance.player.Stat.maxUltimateGauge += effect;
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.LogError($"Enum 파싱에 실패했습니다. 유효하지 않은 PlyerStat: ");
        }
    }
    void ApplyItemPower()
    {
        
        if (Enum.TryParse(objName, true, out ItemList value))
        {
            if (DataManager.Instance.gameData.invenDatas.EquipItemDatas[value].index == -1)
            {
                Debug.Log("장착하고 강화해");
                return;
            }
            switch (value)
            {
                case ItemList.Head:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Head].luck += effect;
                    break;
                case ItemList.Top:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Gloves].critical += effect;
                    break;
                case ItemList.Gloves:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Head].luck += effect;
                    break;
                case ItemList.Weapon:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Weapon].attack += effect;
                    break;
                case ItemList.Belt:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Belt].maxMp += effect;
                    break;
                case ItemList.Bottom:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Bottom].defense += effect;
                    break;
                case ItemList.Shoes:
                    DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Shoes].speed += effect;
                    break;
                default:
                    break;
            }
        }
        GameManager.Instance.player.ApplyEquipmentStat();
    }
    void ApplySkillPower()
    {
        if (Enum.TryParse(objName, true, out SkillName value))
        {
            DataManager.Instance.gameData.playerData.skillDict[value].effect += effect;

        }
        else
        {
            Debug.LogError($"Enum 파싱에 실패했습니다. 유효하지 않은 SkillName");
        }
    }


}
