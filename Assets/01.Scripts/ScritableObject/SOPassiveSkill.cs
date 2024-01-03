using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPassiveSkill : ScriptableObject
{
    public AllEnum.SkillType skillType; // ��ų Ÿ��
    public AllEnum.PassiveSkillType PassiveSkillType; // ��ų ����
    public AllEnum.PassiveSkillName PassiveSkillName; // ��ų �̸� 
    public Sprite icon; // �׸�
    public float effect; // ȿ�� �����̸� ���ݷ� ���̸� ���ϴ¾� ... 
    public float duration; // ��ų ���� �ð�
    public float cool; // ��Ÿ��
    public float mana; // �Ҹ� ����
}
