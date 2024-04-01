using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterPre; // 인스펙터에서 등록

    public GameObject[] SkillObject;
    public XMLAccess XMLAccess { get; private set; }

    public void Init()
    {
        SkillObject = Resources.LoadAll<GameObject>("Skill");
        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }


}
