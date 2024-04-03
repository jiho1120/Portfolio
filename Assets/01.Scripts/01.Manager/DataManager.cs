using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Select select { get; private set; }
    public GameData gameData = new GameData();

    public SOStat SOPlayerStat;
    public SOStat SOMonsterStat;
    public SOStat SOBossStat;
    public SOItem[] Posion;
    public SOItem[] Equipment;
    public NewSOSkill[] activeSkill;
    public NewSOSkill[] passiveSkill;



    public bool[] savefile { get; private set; }   // ���̺����� �������� ����
    public string path; // ���
    public string nowPath; // ���
    public int nowSlot; // ���� ���Թ�ȣ

    protected override void Awake()
    {
        base.Awake();
        savefile = new bool[3];
        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
        select = GetComponent<Select>();
    }


    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        nowPath = path + nowSlot.ToString();
        File.WriteAllText(nowPath, data);
        Debug.Log("����Ǿ����ϴ�.");
    }

    public void LoadData()
    {
        nowPath = path + nowSlot.ToString();
        string data = File.ReadAllText(nowPath);
        gameData = JsonUtility.FromJson<GameData>(data);
        Debug.Log("������ �ҷ��Խ��ϴ�.");

    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }

    public SOItem GetRandomItemData()
    {
        int randamItemType = Random.Range(0, (int)AllEnum.ItemType.End);
        int randamId;
        switch (randamItemType)
        {
            case 0:
                randamId = Random.Range(0, Posion.Length);
                return Posion[randamId];
            case 1:
                randamId = Random.Range(0, Equipment.Length); // ��ȭ�� �������� �ָ� �ȵ�
                return Equipment[randamId];
            default:
                return null;
        }
    }
    
    public void SaveInvenInfo(InvenData newData)
    {
        // JSON ���� �б�
        string json = File.ReadAllText(nowPath);

        // JSON ���ڿ��� ��ü�� ��ȯ
        GameData data = JsonUtility.FromJson<GameData>(json);

        // ���ϴ� ��ü ����
        data.invenDatas = newData;

        // ��ü�� JSON ���ڿ��� �ٽ� ���ڵ�
        string updatedJson = JsonUtility.ToJson(data);

        // ���Ͽ� ����
        File.WriteAllText(nowPath, updatedJson);
    }
    public void LoadInvenInfo()
    {
        nowPath = path + nowSlot.ToString();
        string data = File.ReadAllText(nowPath);
        GameData newGameData = JsonUtility.FromJson<GameData>(data);
        gameData.invenDatas = newGameData.invenDatas;
    }

}