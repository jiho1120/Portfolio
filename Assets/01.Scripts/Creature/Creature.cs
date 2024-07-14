using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Creature : MonoBehaviour, Initialize, IAttack, IStatusEffect, IBuffAndDebuff
{
    public Rigidbody rb { get; protected set; }
    public int id = -1; /*{ get; protected set; }*/
    public Vector3 dir;


    public Transform attackPos;
    protected float AttackRange;
    public int EnemyLayerMask { get; protected set; }
    //public StatData Stat { get; protected set; }
    public StatData Stat;

    #region die, deAct
    public bool isDead { get; protected set; } = false;
    public bool isDeActive { get; protected set; } = false;
    #endregion

    // Knockback 관련 변수 및 함수
    #region Knockback 관련 함수
    private bool isKnockback = false;
    public bool IsKnockback { get => isKnockback; set => isKnockback = value; }
    private Vector3 knockbackDirection;
    public Vector3 KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    private float knockbackPower;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    #endregion

    #region pull 관련 함수

    bool isPull = false;
    public bool IsPull { get => isPull; set => isPull = value; }
    private Vector3 pullPosition;
    public Vector3 PullPosition { get => pullPosition; set => pullPosition = value; }
    private float pullPower;
    public float PullPower { get => pullPower; set => pullPower = value; }
    private float startTime;
    private float journeyLength;
    #endregion

    #region 코루틴 모음
    protected Coroutine deActiveCor = null;
    protected Coroutine knockbackCor = null;
    protected Coroutine pullCor = null;

    protected Coroutine deCreaseAttCor = null;
    protected Coroutine hpCor = null;
    protected Coroutine hitCor = null;

    #endregion


    #region 초기화
    public virtual void Init()
    {
        if (id == -1)
        {
            id = GameManager.Instance.CreatureId;
            GameManager.Instance.CreatureId++;
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

    }

    public virtual void Activate()
    {
        this.gameObject.SetActive(true);
        isDead = false;
        isDeActive = false;
        isKnockback = false;
        IsPull = false;
        startTime = 0;
        journeyLength = 0;
        deActiveCor = null;
        knockbackCor = null;
        pullCor = null;
        deCreaseAttCor = null;
        hpCor = null;
        hitCor = null;

    }

    public virtual void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Set함수
    public void SetAtt(float effect)
    {
        Stat.attack = effect;
        if (Stat.attack <= 0)
        {
            // 최소 공격력
            Stat.attack = 1f;
        }
    }

    public virtual void SetHp(float hp)
    {
        Stat.hp = Mathf.Clamp(hp, 0, Stat.maxHp);
        if (Stat.hp <= 0)
        {
            isDead = true;
        }
    }

    public virtual void AddMoney(int value)
    {
        Stat.money += value;
    }
    #endregion

    #region 레벨 관련
    public virtual void LevelUp()
    {
        StatUp();
    }
    public abstract void StatUp();

    #endregion

    #region 시야
    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - transform.position;
        dir.y = 0;

        return dir;
    }
    #endregion

    #region 공격

    public bool CheckCritical()
    {
        return Random.Range(0f, 100f) < Stat.critical;
    }
    public float CriticalDamage(float att)
    {
        return CheckCritical() ? att * 1.5f : att;
    }
    public virtual void Attack()
    {
        Collider[] colliders = GetAttackRange();
        if (colliders.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<IBuffAndDebuff>().TakeDamage(Stat.attack);
        }
    }
    public Collider[] GetAttackRange()
    {
        return Physics.OverlapSphere(attackPos.position, AttackRange, EnemyLayerMask);
    }
    public void TakeDamage(float att)
    {
        if (isDead || isDeActive) return;

        ImplementTakeDamage();

        float damage = Mathf.Max(CriticalDamage(att) - (Stat.defense * 0.5f), 1f); // 최소 데미지 1
        SetHp(Stat.hp - damage);
    }

    public abstract void ImplementTakeDamage();
    #endregion


    #region 상태이상
    public void Knockback()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(knockbackDirection * knockbackPower, ForceMode.Impulse);
        }
        if (knockbackCor == null)
        {
            knockbackCor = StartCoroutine(StopForceMove(1f));
        }
    }
    public IEnumerator StopForceMove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IsKnockback = false;
        knockbackCor = null;
    }
    public void Pull()
    {
        if (pullCor == null)
        {
            rb.isKinematic = false;
            pullCor = StartCoroutine(PullToPosition());
        }
    }

    private IEnumerator PullToPosition()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(pullPosition, transform.position);
        while (true)
        {
            float distCovered = (Time.time - startTime) * 3;
            float fractionOfJourney = distCovered / journeyLength;

            // Mathf.SmoothStep를 사용하여 부드럽게 이동
            float smoothFraction = Mathf.SmoothStep(0, 1, fractionOfJourney);
            transform.position = Vector3.Lerp(transform.position, pullPosition, smoothFraction);
            yield return null;
        }

    }

    public void StopPull()
    {
        // 외부힘으로 안움직이게함
        rb.isKinematic = true;
        IsPull = false;
        if (pullCor != null)
        {
            StopCoroutine(pullCor);
            pullCor = null;
        }
    }

    public void StartDecreaseAttCor(float effect, float seconds)
    {

        if (deCreaseAttCor == null)
        {
            deCreaseAttCor = StartCoroutine(DecreaseAttPerSeconds(effect, seconds));
        }

    }
    public void StopDecreaseAttCor()
    {

        if (deCreaseAttCor != null)
        {
            StopCoroutine(deCreaseAttCor);
            GetAttToData();
            deCreaseAttCor = null;
        }

    }
    public IEnumerator DecreaseAttPerSeconds(float effect, float seconds)
    {
        while (true)
        {
            Stat.attack -= effect;
            if (Stat.attack <= 1)
                Stat.attack = 1;
            yield return new WaitForSeconds(seconds);
        }

    }

    // 공격력 변화시 다시 원래(데이터에 저장된) 공격력 가져오는 함수
    public abstract void GetAttToData();


    public void StartSetHPCor(float effect, float seconds)
    {
        if (hpCor == null)
        {
            hpCor = StartCoroutine(SetHPCor(effect, seconds));
        }
    }
    public void StopSetHpCor()
    {
        if (hpCor != null)
        {
            StopCoroutine(hpCor);
            hpCor = null;
        }
    }
    IEnumerator SetHPCor(float effect, float seconds)
    {
        while (true)
        {
            SetHp(Stat.hp + effect);
            yield return new WaitForSeconds(seconds);
        }
    }

    public void StartHitAttCor(float effect, float seconds, int count = 1000)// 횟수 안넣으면 스킬 끝날때까지 때림 1000정도면 충분함
    {
        if (hitCor == null)
        {
            hitCor = StartCoroutine(HitPerSeconds(effect, seconds, count));
        }

    }
    public void StopHitCor()
    {
        if (hitCor != null)
        {
            StopCoroutine(hitCor);
            hitCor = null;
        }

    }
    public IEnumerator HitPerSeconds(float effect, float seconds, int count)
    {
        int _count = 0;
        while (count > _count)
        {
            TakeDamage(effect);
            _count += 1;
            yield return new WaitForSeconds(seconds);
        }
        StopHitCor();
    }

    #endregion

    #region 죽음
    public abstract void Die();
    public bool SetDead(bool active)
    {
        return isDead = active;
    }

    #endregion
}
