using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using static AllEnum;

public class DataManager : Singleton<DataManager>
{
    public Select select { get; private set; }
    public GameData gameData;
    public SOStat SOPlayerStat;
    public SOStat SOMonsterStat;
    public SOStat SOBossStat;
    public SOItem[] soItem;
    public NewSOSkill[] skillArr;

    public bool[] savefile { get; private set; }   // 세이브파일 존재유무 저장
    public string path; // 경로
    public string nowPath; // 경로
    public int nowSlot; // 현재 슬롯번호

    private void Start()
    {
        savefile = new bool[3];
        path = Application.persistentDataPath + "/save";
        Debug.Log(path);
    }
    public void Init()
    {
        gameData = new GameData();
        select = GetComponent<Select>();
        select.Init();
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
        try
        {
            nowPath = path + nowSlot.ToString();
            if (File.Exists(nowPath))
            {
                string jsonData = File.ReadAllText(nowPath);

                // JSON 데이터를 게임 데이터 객체로 deserialize
                gameData = JsonConvert.DeserializeObject<GameData>(jsonData);
                Debug.Log("파일을 불러왔습니다.");

                // 로드된 데이터의 유효성 확인
                if (gameData == null)
                {
                    Debug.LogError("로드된 데이터가 null입니다.");
                    return;
                }
                if (gameData.invenDatas == null)
                {
                    Debug.LogError("로드된 인벤토리 데이터가 null입니다.");
                    return;
                }
                // 이하 필요한 필드 및 객체의 유효성을 확인하는 코드 추가
            }
            else
            {
                Debug.LogWarning("로드할 파일이 존재하지 않습니다.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("로드 중 오류 발생: " + ex.Message);
        }
    }

    public void SaveInvenInfo()
    {
        nowPath = path + nowSlot.ToString();
        // JSON 파일 읽기
        string json = File.ReadAllText(nowPath);

        // JSON 문자열을 객체로 변환
        GameData data = JsonConvert.DeserializeObject<GameData>(json);

        // 원하는 객체 수정
        data.invenDatas = gameData.invenDatas;

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

    #region get 함수들
    public NewSOSkill GetSkillData(int idx)
    {

        for (int i = 0; i < skillArr.Length; i++)
        {
            if (skillArr[i].index == idx)
            {
                return skillArr[i];
            }
        }

        return null;

    }
    
    #endregion
}

//public class SpriteConverter : JsonConverter<Sprite>
//{
//    public override Sprite ReadJson(JsonReader reader, System.Type objectType, Sprite existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        string textureName = reader.Value as string;
//        if (!string.IsNullOrEmpty(textureName))
//        {
//            Sprite sprite = Resources.Load<Sprite>(textureName);
//            return sprite;
//        }
//        return null;
//    }

//    public override void WriteJson(JsonWriter writer, Sprite value, JsonSerializer serializer)
//    {
//        if (value != null && value.texture != null)
//        {
//            writer.WriteValue(value.texture.name);
//        }
//        else
//        {
//            writer.WriteNull();
//        }
//    }

//}


