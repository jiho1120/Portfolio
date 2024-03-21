using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>, ReInitialize
{
    public Monster[] monsterAll;

    public GameObject[] SkillObjectAll;
    public SOSkill[] skillDataAll;

    public SOItem[] itemDataAll;
    public XMLAccess XMLAccess {get; private set; }

    public void Init()
    {
        monsterAll = Resources.LoadAll<Monster>("Object/Monster");
        SkillObjectAll = Resources.LoadAll<GameObject>("Skill");
        skillDataAll = Resources.LoadAll<SOSkill>("SOData/SkillData");
        itemDataAll = Resources.LoadAll<SOItem>("SOData/ItemData");
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
   

    public SOSkill GetSkillData(int index)
    {
        for (int i = 0; i < skillDataAll.Length; i++)
        {
            if (skillDataAll[i].index == index)
            {
                return skillDataAll[i];
            }            
        }
        return null;
    }
}
