using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ���������� ���ܼ� �װ� ȿ���� �ִ� ��ų
public class TerrainEffectSkill : ActiveSkill
{

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        
    }
    // ���Ϳ��� �� ���� ��Ʈ��
    protected virtual void OnCollisionStay(Collision collision)
    {
        base.OnCollisionEnter(collision);

    }
}
