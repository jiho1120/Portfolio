using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>, ReInitialize
{
    //public Monster[] monsterAll;

    public GameObject[] SkillObject;
    public XMLAccess XMLAccess {get; private set; }

    public void Init()
    {
        //monsterAll = Resources.LoadAll<Monster>("Object/Monster");
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
