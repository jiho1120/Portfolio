using System.Collections.Generic;
using UnityEngine;
using static AllEnum;


public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<DictName, Dictionary<string, GameObject>> prefabDictionary;
    public Dictionary<DictName, Dictionary<string, Sprite>> spriteDictionary;

    public Sprite nullEquipSprite;
    public Sprite playerPowerUpIcon; // ��ȭâ���� �÷��̾� ��ȭ ������ -> �̰� �ϳ��� ������




    public XMLAccess XMLAccess { get; private set; }
   
    public void Init()
    {
        prefabDictionary = new Dictionary<DictName, Dictionary<string, GameObject>>();
        spriteDictionary = new Dictionary<DictName, Dictionary<string, Sprite>>();
        LoadAllResources();

        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }
    // ��� ���ҽ��� �ε��ϴ� �޼���
    private void LoadAllResources()
    {
        LoadPrefabs("Prefabs/Monster", DictName.MonsterDict);
        LoadPrefabs("Prefabs/Item", DictName.ItemDict);
        LoadPrefabs("Prefabs/Skill", DictName.SkillDict);
        LoadSprites("Sprites/Item", DictName.ItemSpriteDict);
        LoadSprites("Sprites/Skill", DictName.SkillSpriteDict);
        //PrintDict();
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
    // Ư�� ����� �������� �ε��ϰ� ��ųʸ��� �����ϴ� �޼���
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
    public Sprite GetSprite(DictName category, string prefabName)
    {
        if (spriteDictionary.ContainsKey(category) && spriteDictionary[category].ContainsKey(prefabName))
        {
            return spriteDictionary[category][prefabName];
        }
        Debug.LogWarning($"��������Ʈ {prefabName}��(��) {category} ī�װ����� ã�� �� �����ϴ�.");
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
