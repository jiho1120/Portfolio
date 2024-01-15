using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStat
{
    public int index { get; private set; }
    public AllEnum.SkillType skillType { get; private set; }
    public AllEnum.PassiveSkillType passiveSkillType { get; private set; }
    public AllEnum.SkillName skillName { get; private set; }
    public Sprite icon { get; private set; }
    public float effect { get; private set; }
    public float duration { get; private set; }
    public float cool { get; private set; }
    public float mana { get; private set; }
    public bool setParent { get; private set; }
    public bool inUse { get; private set; }

    public SkillStat(SOSkill soSkill)
    {
        this.index = soSkill.index;
        this.skillType = soSkill.skillType;
        this.passiveSkillType = soSkill.passiveSkillType;
        this.skillName = soSkill.skillName;
        this.icon = soSkill.icon;
        this.effect = soSkill.effect;
        this.duration = soSkill.duration;
        this.cool = soSkill.cool;
        this.mana = soSkill.mana;
        this.setParent = soSkill.setParent;
        this.inUse = soSkill.inUse;
    }

    public void  SetInUse(bool isOn)
    {
        inUse = isOn;
    }
    public void SetEffect(float effect)
    {
        this.effect += effect;
    }
}
