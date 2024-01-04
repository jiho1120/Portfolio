using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Monster[] monsterAll;
    public GameObject[] objectAll;
    public SOSkill[] skillDataAll;
    private void Start()
    {

    }
    public void LoadResources()
    {
        monsterAll = Resources.LoadAll<Monster>("Object/Monster");
        objectAll = Resources.LoadAll<GameObject>("Skill");
        skillDataAll = Resources.LoadAll<SOSkill>("SOData/SkillData");
    }
}
