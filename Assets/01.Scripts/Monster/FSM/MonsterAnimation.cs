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
    }
    public void Walk(bool isMove)
    {
        anim.SetBool("isMove", true);
    }

    public void Attack()
    {
    
    }
    public void Hit()
    {

    }
    public void Die()
    {

    }
}
