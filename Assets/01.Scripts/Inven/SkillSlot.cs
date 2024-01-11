using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour // ��� ��ü ����
{
    public AllEnum.SkillName skill;
    public Image icon;
    public Text coolTime;
    [SerializeField]
    Skill ownSkill; // ��ų �Ŵ������� ��ų �����ͼ� ����ϱ�, ��ų�����Ͱ� �ƴ������� �ٲ�� �����Ͱ� ��ų���־ ������ �������� �ȵ�

    
    public void SetSkill()
    {
        ownSkill = SkillManager.Instance.GetSKillFromDict(skill);
    }
    public void SetIcon()
    {
        icon.sprite = ownSkill.orgInfo.icon;
    }
}
