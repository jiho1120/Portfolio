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



    public bool[] savefile { get; private set; }   // 세이브파일 존재유무 저장
    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호

    protected override void Awake()
    {
        base.Awake();
        savefile = new bool[3];
        path = Application.persistentDataPath + "/save";    // 경로 지정
        Debug.Log(path);
        select = GetComponent<Select>();
    }


    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("저장되었습니다.");
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        gameData = JsonUtility.FromJson<GameData>(data);
        Debug.Log("파일을 불러왔습니다.");

    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }

}