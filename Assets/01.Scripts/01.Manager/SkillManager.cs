using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public Dictionary<AllEnum.SkillName, Skill> skillDict { get; private set; }
    public Dictionary<AllEnum.SkillName, Skill> bossSkillDict { get; private set; }

    public void Init()
    {
        skillDict = new Dictionary<AllEnum.SkillName, Skill>();
        bossSkillDict = new Dictionary<AllEnum.SkillName, Skill>();
        foreach (var item in ResourceManager.Instance.SkillObject)
        {
            Debug.Log(item.name);
        }
    }

    public void SetAllSKill()
    {

    }
}
