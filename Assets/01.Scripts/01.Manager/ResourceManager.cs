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

    // Ư�� ����� �������� �ε��ϰ� ��ųʸ��� �����ϴ� �޼���
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
    // ��� �������� �ε��ϴ� �޼���
    private void LoadAllPrefabs()
    {
        LoadPrefabs("Prefabs/Monster", DictName.MonsterDict);
        LoadPrefabs("Prefabs/Item", DictName.ItemDict);
        LoadPrefabs("Prefabs/Skill", DictName.SkillDict);
        //PrintDict();
    }

    /// <summary>
    /// ������ �������� �޼���
    /// </summary>
    /// <param name="category">��ųʸ� enum</param>
    /// <param name="prefabName">�ش� enumtype�� ��Ʈ������ ��ȯ�� ���</param>
    /// <returns></returns>
    public GameObject GetPrefab(DictName category, string prefabName)
    {
        if (prefabDictionary.ContainsKey(category) && prefabDictionary[category].ContainsKey(prefabName))
        {
            return prefabDictionary[category][prefabName];
        }
        Debug.LogWarning($"������ {prefabName}��(��) {category} ī�װ����� ã�� �� �����ϴ�.");
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
