using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour // 얘는 교체 없음
{
    public AllEnum.SkillName skillName;
    public Image icon;
    public Text coolTime;
    public Image gauge;
    [SerializeField]
    public Skill ownSkill { get; private set; } // 스킬 매니저에서 스킬 가져와서 사용하기, 스킬데이터가 아닌이유는 바뀌는 데이터가 스킬에있어서 원본을 가져오면 안됨
    float useTime = 0;

    public void SetSkill()
    {
        ownSkill = SkillManager.Instance.GetSKillFromDict(skillName);
    }
    public void SetIcon()
    {
        icon.sprite = ownSkill.orgInfo.icon;
    }
    public void SetCoolTime()
    {
        if (coolTime != null)
        {
            coolTime.text = ownSkill.orgInfo.cool.ToString();
        }
    }
    public Skill GetSkill()
    {
        return ownSkill;
    }
    public void SetUseSKillTime()
    {
        useTime = 0;
        gauge.fillAmount = 1;
        StartCoroutine(SkillCoolCor());
    }
    private IEnumerator SkillCoolCor()
    {
        while (useTime <= GetSkill().orgInfo.cool)
        {
            useTime += Time.deltaTime;
            yield return null;
            gauge.fillAmount = 1 - useTime / ownSkill.orgInfo.cool;
        }
    }
    public void SetBeggginSuper()
    {
        gauge.fillAmount = 0;
        GameManager.Instance.player.playerStat.SetUltimateGauge(0);
    }
}
