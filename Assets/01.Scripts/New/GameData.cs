using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int gameDataIdx;
    public SlotData slotData;
    public PlayerData playerData;
    
    public void SetGameData()
    {
        gameDataIdx = NewGameManager.Instance.nowGameIdx;
        slotData = SlotManager.Instance.slotDatas[gameDataIdx];

        if (slotData == null)
        {
            slotData = new SlotData();

        }
        if (playerData == null)
        {
            playerData = new PlayerData();
        }
        
        slotData.SetSlotData(NewUIManager.Instance.inputField.text);

        playerData.SetPlayerData();
    }
}

[System.Serializable]
public class SlotData
{
    public string playerName;
    public string date; // ��¥ ������ ����
    public string time; // �ð� ������ ����

    public void SetSlotData(string name)
    {
        playerName = name;
        // ���� ��¥�� date �ʵ忡 ����
        date = DateTime.Now.ToString("yyyy-MM-dd");
        // ���� �ð��� time �ʵ忡 ����
        time = DateTime.Now.ToString("HH:mm:ss");
    }
    public void SetSlotData(SlotData data)
    {
        playerName = data.playerName;
        date = data.date;
        time = data.time;
    }
}
[System.Serializable]
public class PlayerData
{
    public void SetPlayerData()
    {

    }
}
