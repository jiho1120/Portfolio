using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour // 얘는 교체 없음
{
    public AllEnum.SkillName skill;
    public Image icon;
    public Text coolTime;
    [SerializeField]
    Skill ownSkill; // 스킬 매니저에서 스킬 가져와서 사용하기, 스킬데이터가 아닌이유는 바뀌는 데이터가 스킬에있어서 원본을 가져오면 안됨

    
    public void SetSkill()
    {
        ownSkill = SkillManager.Instance.GetSKillFromDict(skill);
    }
    public void SetIcon()
    {
        icon.sprite = ownSkill.orgInfo.icon;
    }
}
