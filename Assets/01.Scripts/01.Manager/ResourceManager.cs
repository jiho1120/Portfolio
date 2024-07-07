using System.Collections.Generic;
using UnityEngine;
using static AllEnum;


public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<DictName, Dictionary<string, GameObject>> prefabDictionary;
    public Dictionary<DictName, Dictionary<string, Sprite>> spriteDictionary;

    public Sprite nullEquipSprite;
    public Sprite playerPowerUpIcon; // 강화창에서 플레이어 강화 아이콘 -> 이거 하나만 쓸꺼임




    public XMLAccess XMLAccess { get; private set; }
   
    public void Init()
    {
        prefabDictionary = new Dictionary<DictName, Dictionary<string, GameObject>>();
        spriteDictionary = new Dictionary<DictName, Dictionary<string, Sprite>>();
        LoadAllResources();

        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }
    // 모든 리소스를 로드하는 메서드
    private void LoadAllResources()
    {
        LoadPrefabs("Prefabs/Monster", DictName.MonsterDict);
        LoadPrefabs("Prefabs/Item", DictName.ItemDict);
        LoadPrefabs("Prefabs/Skill", DictName.SkillDict);
        LoadSprites("Sprites/Item", DictName.ItemSpriteDict);
        LoadSprites("Sprites/Skill", DictName.SkillSpriteDict);
        //PrintDict();
    }

    // 특정 경로의 프리팹을 로드하고 딕셔너리에 저장하는 메서드
    private void LoadPrefabs(string path, DictName category)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(path);
        if (!prefabDictionary.ContainsKey(category))
        {
            prefabDictionary[category] = new Dictionary<string, GameObject>();
        }

        foreach (var prefab in prefabs)
        {
            prefabDictionary[category][prefab.name] = prefab;
        }
    }
    // 특정 경로의 프리팹을 로드하고 딕셔너리에 저장하는 메서드
    private void LoadSprites(string path, DictName category)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        if (!spriteDictionary.ContainsKey(category))
        {
            spriteDictionary[category] = new Dictionary<string, Sprite>();
        }

        foreach (var sprite in sprites)
        {
            spriteDictionary[category][sprite.name] = sprite;
        }
    }

    /// <summary>
    /// 프리팹 가져오기 메서드
    /// </summary>
    /// <param name="category">딕셔너리 enum</param>
    /// <param name="prefabName">해당 enumtype을 스트링으로 변환후 사용</param>
    /// <returns></returns>
    public GameObject GetPrefab(DictName category, string prefabName)
    {
        if (prefabDictionary.ContainsKey(category) && prefabDictionary[category].ContainsKey(prefabName))
        {
            return prefabDictionary[category][prefabName];
        }
        Debug.LogWarning($"프리팹 {prefabName}을(를) {category} 카테고리에서 찾을 수 없습니다.");
        return null;
    }
    public Sprite GetSprite(DictName category, string prefabName)
    {
        if (spriteDictionary.ContainsKey(category) && spriteDictionary[category].ContainsKey(prefabName))
        {
            return spriteDictionary[category][prefabName];
        }
        Debug.LogWarning($"스프라이트 {prefabName}을(를) {category} 카테고리에서 찾을 수 없습니다.");
        return null;
    }


    void PrintDict()
    {
        foreach (var category in prefabDictionary)
        {
            Debug.Log($"Category: {category.Key}");
            foreach (var prefab in category.Value)
            {
                Debug.Log($"Prefab Name: {prefab.Key}");
            }
        }
    }
}
