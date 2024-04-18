using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterPre; // �ν����Ϳ��� ���
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
