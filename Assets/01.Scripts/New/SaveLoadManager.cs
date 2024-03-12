using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    public GameData gameData;
    string path;
    public void Save()
    {
        gameData.SetGameData();
        SetPath(NewGameManager.Instance.nowGameIdx);
        string saveData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, saveData);
        Debug.Log("File saved: " + path);
        SceneLoadController.Instance.GoGameScene();

    }

    public void Load(int idx)
    {
        SetPath(idx);
        if (File.Exists(path))
        {
            string loadData = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(loadData);
            Debug.Log("File loaded: " + path);
            SlotManager.Instance.slotDatas[idx] = gameData.slotData;

        }
        else
        {
            Debug.LogWarning("File not found: " + path);
        }
    }

    public void SetPath(int index)
    {
        path = Application.persistentDataPath + "/data" + index + ".json";
    }
}
