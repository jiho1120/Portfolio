using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public string skillName;
    public AllEnum.SkillType skillType;
    public Sprite icon;
    public float effect;
    public float duration;
    public float cool;
    public float mana;

    public void SetSkillData(SOSkill skill)
    {
        this.skillName = skill.skillName;
        this.skillType = skill.skillType;
        this.icon = skill.icon;
        this.effect = skill.effect;
        this.duration = skill.duration;
        this.cool = skill.cool;
        this.mana = skill.mana;
    }
    public void PrintSkillData()
    {
        Debug.Log("Skill Name: " + skillName);
        Debug.Log("Skill Type: " + skillType);
        Debug.Log("Effect: " + effect);
        Debug.Log("Duration: " + duration);
        Debug.Log("cool: " + cool);
        Debug.Log("Mana Cost: " + mana);
    }

}
