using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator anim;
    public void SetInit()
    {
        anim = GetComponent<Animator>();
    }

    public void Idle()
    {
        anim.SetBool("isMove", false);
    }
    public void Walk()
    {
        anim.SetBool("isMove", true);
    }

    public void AttackAnim(bool b)
    {
        //anim.SetTrigger("Attack");
        anim.SetBool("Attack", b);

    }
    public void StopAttack()
    {
        anim.ResetTrigger("Attack");
    }
    public void Hit()
    {
        anim.SetTrigger("isHit");
    }
    public void Die()
    {
        anim.SetTrigger("isDead");
    }
}
