using UnityEngine;
using static AllEnum;
public class HumanCharacter : Creature
{
    protected Coroutine passiveCor = null;
    protected SkillName nowPassiveSKillName;
    public Transform skillPos;

    #region 공격
    protected float attackSpeed = 1;
    float lastClickTime = 0f;
    float attackCooldown = 1.5f;
    bool isLeft = false;
    #endregion

    protected PlayerAnimator animator;

   
    public override void Init()
    {
        base.Init();
        if (animator == null)
        {
            animator = GetComponent<PlayerAnimator>();
            animator.Init();
            animator.SetAttackSpeed(attackSpeed);
        }
        AttackRange = 1f;
    }

    public override void Activate()
    {
        base.Activate();
        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(SkillManager.Instance.StartPassiveCor(this));
        }
    }

    public override void Deactivate()
    {
        SkillManager.Instance.DeactivateAllSkills(this);
        StopPassiveCorNull();
        base.Deactivate();
    }
   
    public void SetEnemyLayer(int layer)
    {
        EnemyLayerMask = layer;
    }
    #region 레벨 관련

    public override void StatUp()
    {
    }
    #endregion
    #region 공격 & 피격
    public void BasicAttack()
    {
        //클릭할때마다 이전시간과 비교해서 연속공격상태면 다음 주먹으로 변경하고
        //연속공격내의 시간이 아니면 첫주먹으로.
        float TimeDifference = Time.time - lastClickTime;

        // 1초동안함 근데 스피드가 증가함
        // 애니메이션 스피드가 올라가서 애니메이션도 빨리 끝남
        float animTime = 1f / attackSpeed; // 바뀐 애니메이션 시간 = 애니메이션 시간(1초) / 애니메이션 스피드

        //동작하는 동안의 시간이면 되돌려보내고
        if (TimeDifference <= animTime)
        {
            return;
        }
        else //그게 아니라면
        {
            if (TimeDifference <= attackCooldown) //연속공격
            {
                if (isLeft)
                {
                    animator.LeftAttack();
                }
                else
                {
                    animator.RightAttack();
                }
                isLeft = !isLeft;
            }
            else
            {
                animator.RightAttack();
                isLeft = true;

            }
            lastClickTime = Time.time;
        }
    }

    #endregion

    public void SetMoveAnim(float speed, float z, float x)
    {
        animator.WalkOrRun(speed); // 1or 1.5
        animator.MoveAnim(z, x);
    }
    public override void Die()
    {
    }

    public override void GetAttToData()
    {
        throw new System.NotImplementedException();
    }

    public override void ImplementTakeDamage()
    {
        throw new System.NotImplementedException();
    }

    public void StopPassiveCorNull()
    {
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
        }
        passiveCor = null;
    }


}