using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NewSOSkill : ScriptableObject
{
    public int index; // ������ȣ
    public AllEnum.NewSkillType newSkillType; // ��ų Ÿ��
    public AllEnum.SkillName skillName; // ��ų �̸�
    public Sprite icon; // �׸�
    public float effect; // ȿ�� �����̸� ���ݷ� ���̸� ���ϴ¾� ... 
    public float duration; // ��ų ���� �ð�
    public float cool; // ��Ÿ��
    public float mana; // �Ҹ� ����
    public bool setParent; // ��ų�� �÷��̾ ����ٴ���
    public bool inUse; //false�Ͻ� ��ų���� 
}
