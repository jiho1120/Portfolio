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
    public string nowPath; // 경로
    public int nowSlot; // 현재 슬롯번호

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
        Debug.Log("저장되었습니다.");
    }

    public void LoadData()
    {
        nowPath = path + nowSlot.ToString();
        string data = File.ReadAllText(nowPath);
        gameData = JsonUtility.FromJson<GameData>(data);
        Debug.Log("파일을 불러왔습니다.");

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
                randamId = Random.Range(0, Equipment.Length); // 강화된 아이템을 주면 안됨
                return Equipment[randamId];
            default:
                return null;
        }
    }
    
    public void SaveInvenInfo(InvenData newData)
    {
        // JSON 파일 읽기
        string json = File.ReadAllText(nowPath);

        // JSON 문자열을 객체로 변환
        GameData data = JsonUtility.FromJson<GameData>(json);

        // 원하는 객체 수정
        data.invenDatas = newData;

        // 객체를 JSON 문자열로 다시 인코딩
        string updatedJson = JsonUtility.ToJson(data);

        // 파일에 쓰기
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