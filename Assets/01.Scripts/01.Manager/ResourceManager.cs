using System.Collections.Generic;
using UnityEngine;
using static AllEnum;


public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<DictName, Dictionary<string, GameObject>> prefabDictionary;

    public Sprite nullEquipSprite;
    public Sprite[] ItemSprite;


    public XMLAccess XMLAccess { get; private set; }

    
    public void Init()
    {
        prefabDictionary = new Dictionary<DictName, Dictionary<string, GameObject>>();
        LoadAllPrefabs();
        
        //XMLAccess = GetComponent<XMLAccess>();
        //XMLAccess.Init();
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
    // 모든 프리팹을 로드하는 메서드
    private void LoadAllPrefabs()
    {
        LoadPrefabs("Prefabs/Monster", DictName.MonsterDict);
        LoadPrefabs("Prefabs/Item", DictName.ItemDict);
        LoadPrefabs("Prefabs/Skill", DictName.SkillDict);
        //PrintDict();
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
