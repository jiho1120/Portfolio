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
    public string date; // 날짜 정보만 저장
    public string time; // 시간 정보만 저장

    public void SetSlotData(string name)
    {
        playerName = name;
        // 현재 날짜를 date 필드에 저장
        date = DateTime.Now.ToString("yyyy-MM-dd");
        // 현재 시간을 time 필드에 저장
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
