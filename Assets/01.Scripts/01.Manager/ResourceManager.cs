using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterPre; // 인스펙터에서 등록
    public Sprite nullEquipSprite;
    public Sprite[] ItemSprite;

    public GameObject[] SkillObject;

    public XMLAccess XMLAccess { get; private set; }

    public void Init()
    {
        ItemSprite = Resources.LoadAll<Sprite>("Object/Item");
        SkillObject = Resources.LoadAll<GameObject>("Skill");
        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }


}
