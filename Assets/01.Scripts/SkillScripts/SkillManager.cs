using System.Collections;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public Skill skill; 
    //액티브
    public SOActiveSkill[] ActiveSkillDatas;
    public GameObject[] ActiveSkillPre;


    // 패시브
    public SOPassiveSkill[] PassiveSkillDatas;
    public GameObject[] PassiveSkillPre;




    private void Start()
    {
        for (int i = 0; i < ActiveSkillPre.Length; i++)
        {
            if (i < ActiveSkillDatas.Length)
            {
                skill.ActiveSkillPreDict.Add(ActiveSkillPre[i], ActiveSkillDatas[i]);
            }
            else
            {
                Debug.LogError("ActiveSkillDatas array is shorter than ActiveSkillPre array.");
                break;
            }
        }

    }


}
