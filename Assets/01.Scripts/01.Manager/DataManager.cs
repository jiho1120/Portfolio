using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class DataManager : Singleton<DataManager>
{
    public Select select { get; private set; }
    public GameData gameData = new GameData();

    public SOStat SOPlayerStat;
    public SOStat SOMonsterStat;
    public SOStat SOBossStat;
    public SOItem[] soItem;

    //public NewSOSkill[] activeSkill;
    //public NewSOSkill[] passiveSkill;



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


    public void Save()
    {
        nowPath = path + nowSlot.ToString();

        // JSON 직렬화를 위한 설정 생성
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // GameData 객체를 JSON 문자열로 변환
        string jsondata = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);

        // 파일에 쓰기
        File.WriteAllText(nowPath, jsondata);

        Debug.Log("저장되었습니다.");
    }



    public void Load()
    {
        nowPath = path + nowSlot.ToString();
        string jsonData = File.ReadAllText(nowPath);

        // JSON 데이터를 게임 데이터 객체로 deserialize
        gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
        Debug.Log("파일을 불러왔습니다.");
    }

    public void SaveInvenInfo(InvenData newData)
    {
        // JSON 파일 읽기
        string json = File.ReadAllText(nowPath);

        // JSON 문자열을 객체로 변환
        GameData data = JsonConvert.DeserializeObject<GameData>(json);

        // 원하는 객체 수정
        data.invenDatas = newData;

        // JSON 직렬화를 위한 설정 생성
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        // GameData 객체를 JSON 문자열로 변환
        string jsondata = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);

        // 파일에 쓰기
        File.WriteAllText(nowPath, jsondata);
        Debug.Log("저장되었습니다.");

    }
    public void LoadInvenInfo()
    {
        nowPath = path + nowSlot.ToString();
        string data = File.ReadAllText(nowPath);
        GameData newGameData = JsonConvert.DeserializeObject<GameData>(data);
        gameData.invenDatas = newGameData.invenDatas;
    }

    public void DataClear()
    {
        nowSlot = -1;
        gameData = new GameData();
    }

    public ItemData GetRandomItemData()
    {
        int randamId;
        ItemData item = new ItemData();
        randamId = Random.Range(0, (int)AllEnum.ItemList.End);
        item.SetItemData(soItem[randamId]);
        return item;
    }
}

public class SpriteConverter : JsonConverter<Sprite>
{
    public override Sprite ReadJson(JsonReader reader, System.Type objectType, Sprite existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        string textureName = reader.Value as string;
        if (!string.IsNullOrEmpty(textureName))
        {
            Sprite sprite = Resources.Load<Sprite>(textureName);
            return sprite;
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, Sprite value, JsonSerializer serializer)
    {
        if (value != null && value.texture != null)
        {
            writer.WriteValue(value.texture.name);
        }
        else
        {
            writer.WriteNull();
        }
    }

}

