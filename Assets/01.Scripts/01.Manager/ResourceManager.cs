using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>, ReInitialize
{
    public Monster[] monsterPre; // 인스펙터에서 등록

    public GameObject[] SkillObject;
    public XMLAccess XMLAccess {get; private set; }

    public void Init()
    {
        SkillObject = Resources.LoadAll<GameObject>("Skill");
        XMLAccess = GetComponent<XMLAccess>();
        XMLAccess.Init();
    }
    public void ReInit()
    {
        throw new System.NotImplementedException();
    }
    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
   
}
