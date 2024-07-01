using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지형지물을 안 남기는 스킬
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
            // 처음맞으면 스킬 딜
            Attack(cre);
        }
        else if (hitObj.Contains(cre.id))
        {
            //1대 맞고는 도트딜 아니면 지형에 막히게
        }
    }

}
