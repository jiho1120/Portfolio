using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterPre; // �ν����Ϳ��� ���

    public GameObject[] SkillObject;
    public XMLAccess XMLAccess {get; private set; }

    public void Init()
    {
        SkillObject = Resources.LoadAll<GameObject>("Skill");
        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }
    
   
}
