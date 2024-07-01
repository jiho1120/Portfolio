using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : ActiveSkill, IPull
{
    // 다시만들기
    float power = 10f;
    public override void Activate()
    {
        base.Activate();
        this.transform.position = GameManager.Instance.player.transform.position + Vector3.forward * 5;
    }
    public void SetPullCondition(Creature cre)
    {
        Vector3 vec = (cre.transform.position - transform.position).normalized;
        vec.y = 0; // 수평 방향으로만 넉백

        cre.PullDirection = vec;
        cre.PullPower = power;
        cre.IsPull = true;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre = other.GetComponent<Creature>();
        if (cre != null)
        {
            if (cre.IsKnockback == true)
            {
                return;
            }
            Debug.Log("컨디션함수 실행");
            SetPullCondition(cre);
            cre.Pull(this.transform.position);
        }
    }
}
