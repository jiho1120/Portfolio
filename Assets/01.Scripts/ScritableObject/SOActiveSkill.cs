using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOActiveSkill : ScriptableObject
{
    public AllEnum.SkillType skillType; // ��ų Ÿ��
    public AllEnum.ActiveSkillName ActiveSkillName; // ��ų Ÿ��
    public Sprite icon; // �׸�
    public float effect; // ȿ�� �����̸� ���ݷ� ���̸� ���ϴ¾� ... 
    public float duration; // ��ų ���� �ð�
    public float cool; // ��Ÿ��
    public float mana; // �Ҹ� ����
}
