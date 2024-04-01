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
    public int nowSlot; // ���� ���Թ�ȣ

    protected override void Awake()
    {
        base.Awake();
        savefile = new bool[3];
        path = Application.persistentDataPath + "/save";    // ��� ����
        Debug.Log(path);
        select = GetComponent<Select>();
    }


    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("����Ǿ����ϴ�.");
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        gameData = JsonUtility.FromJson<GameData>(data);
        Debug.Log("������ �ҷ��Խ��ϴ�.");

    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }

}