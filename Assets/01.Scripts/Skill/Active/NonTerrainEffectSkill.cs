using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������� �� ����� ��ų
public class NonTerrainEffectSkill : ActiveSkill
{
    private HashSet<int> hitObj = new HashSet<int>();
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre = other.GetComponent<Creature>();
        if (cre != null)
        {
            return;
        }
        if (!hitObj.Contains(cre.id))
        {
            // ó�������� ��ų ��
            Attack(cre);
        }
        else if (hitObj.Contains(cre.id))
        {
            //1�� �°�� ��Ʈ�� �ƴϸ� ������ ������
        }
    }

}
