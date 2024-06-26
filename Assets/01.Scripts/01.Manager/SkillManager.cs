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

    public int ChangeNameToIndex(AllEnum.SkillName name)
    {
        switch (name)
        {
            case AllEnum.SkillName.AirSlash:
                return 1;
            case AllEnum.SkillName.AirCircle:
                return 2;
            case AllEnum.SkillName.Ground:
                return 3;
            case AllEnum.SkillName.Gravity:
                return 4;
            case AllEnum.SkillName.Fire:
                return 101;
            case AllEnum.SkillName.Heal:
                return 101;
            case AllEnum.SkillName.Love:
                return 102;
            case AllEnum.SkillName.Wind:
                return 103;
            case AllEnum.SkillName.End:
                return 104;
            default: return -1;
        }
    }
}
