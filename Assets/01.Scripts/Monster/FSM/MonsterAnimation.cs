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
        anim.SetBool("isMove",true);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    public void Hit()
    {
        anim.SetTrigger("isHit");
    }
    public void Die()
    {
        anim.SetBool("isDead",true);

    }
}
