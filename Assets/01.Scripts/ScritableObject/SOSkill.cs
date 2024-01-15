using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOSkill : ScriptableObject
{
    public int index; // ������ȣ
    public AllEnum.SkillType skillType; // ��ų Ÿ��
    public AllEnum.PassiveSkillType passiveSkillType; // ��ų ����
    public AllEnum.SkillName skillName; // ��ų �̸�
    public Sprite icon; // �׸�
    public float effect; // ȿ�� �����̸� ���ݷ� ���̸� ���ϴ¾� ... 
    public float duration; // ��ų ���� �ð�
    public float cool; // ��Ÿ��
    public float mana; // �Ҹ� ����
    public bool setParent; // ��ų�� �÷��̾ ����ٴ���
    public bool inUse; //false�Ͻ� ��ų���� 

}
