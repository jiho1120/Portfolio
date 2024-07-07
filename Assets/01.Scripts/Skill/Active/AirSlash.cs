using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AirSlash : ActiveSkill
{
    // ����Ʈ ��� �ؽ����� ����ϴ� ���� 
    // �ߺ� ����, �����˻� �� �߰�
    // ������ ������ �߿��ϸ� ����Ʈ�� ����ϱ� 
    private HashSet<int> hitMonsters = new HashSet<int>();
    public override void Activate()
    {
        base.Activate();
    }
    public override void Deactivate()
    {
        base.Deactivate();
        hitMonsters.Clear();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre = other.GetComponent<Creature>();
        if (cre != null && !hitMonsters.Contains(cre.id))
        {
            hitMonsters.Add(cre.id);
            cre.TakeDamage(skilldata.effect);
            Debug.Log("����������");
        }
    }
}
