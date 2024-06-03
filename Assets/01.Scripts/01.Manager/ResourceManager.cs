using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterPre; // �ν����Ϳ��� ���
    public Sprite nullEquipSprite;
    public Sprite[] ItemSprite;

    public Skill[] SkillObject;

    public XMLAccess XMLAccess { get; private set; }

    public void Init()
    {
        SkillObject = Resources.LoadAll<Skill>("Skill");
        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }


}
