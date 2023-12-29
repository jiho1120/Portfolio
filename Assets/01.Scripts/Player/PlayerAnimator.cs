using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    public void Starts()
    {
        anim = GetComponent<Animator>();

    }

    public void MoveAnim(float z, float x)
    {
        anim.SetFloat("PosZ", z);
        anim.SetFloat("PosX", x);
    }
    public void WalkOrRun(float percent)
    {
        anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
    }
    public void LeftAttack()
    {
        anim.SetTrigger("isLeftPunch");

    }
    public void RightAttack()
    {
        anim.SetTrigger("isRightPunch");

    }
    public void SetAttackSpeed(float _attackSpeed)
    {
        anim.SetFloat("AttackSpeed", _attackSpeed);
    }
}
