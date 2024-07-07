using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AirSlash : ActiveSkill
{
    // 리스트 대신 해쉬셋을 사용하는 이유 
    // 중복 방지, 빠른검색 및 추가
    // 하지만 순서가 중요하면 리스트로 사용하기 
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
            Debug.Log("슬래쉬맞음");
        }
    }
}
