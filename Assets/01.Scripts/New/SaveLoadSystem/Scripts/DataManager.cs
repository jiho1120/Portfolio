using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    public GameData gameData = new GameData();

    public SOPlayer SO;
    public string path; // ���
    public int nowSlot; // ���� ���Թ�ȣ

    protected override void Awake()
    {
        base.Awake();
        path = Application.persistentDataPath + "/save";    // ��� ����
        Debug.Log(path);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("����Ǿ����ϴ�.");
        Debug.Log(gameData.playerData.playerStat.name);
        Debug.Log(gameData.playerData.playerStat.level);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        gameData = JsonUtility.FromJson<GameData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }
    
}