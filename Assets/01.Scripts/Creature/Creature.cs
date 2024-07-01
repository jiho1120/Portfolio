using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Creature : MonoBehaviour, Initialize, IAttack, IStatusEffect, IBuffAndDebuff
{
    public Rigidbody rb { get; protected set; }
    public int id;

    public Transform attackPos;
    protected float AttackRange;
    protected int EnemyLayerMask;
    public StatData Stat; // { get; protected set; }
    public bool isDead { get; protected set; } = false;

    // Knockback 관련 변수 및 함수
    #region Knockback 관련 함수
    private bool isKnockback = false;
    public bool IsKnockback { get => isKnockback; set => isKnockback = value; }
    private Vector3 knockbackDirection;
    public Vector3 KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    private float knockbackPower;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    private Coroutine knockbackCor = null;
    #endregion

    #region pull 관련 함수

    bool isPull = false;
    public bool IsPull { get => isPull; set => isPull = value; }
    private Vector3 pullDirection;
    public Vector3 PullDirection { get => pullDirection; set => pullDirection = value; }
    private float pullPower;
    public float PullPower { get => pullPower; set => pullPower = value; }
    Coroutine forceCor = null;


    #endregion
    #region 패시브 함수 관련 코루틴
    protected Coroutine deCreaseAttCor = null;
    protected Coroutine hpCor = null;
    protected Coroutine hitCor = null;

    #endregion


    #region 초기화
    public virtual void Activate()
    {
        this.gameObject.SetActive(true);
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
    public void SetLuck(float value)
    {
        Stat.luck = value;
    }
    public virtual void SetHp(float hp)
    {
        Stat.hp = Mathf.Clamp(hp, 0, Stat.maxHp);
        if (Stat.hp <= 0)
        {
            isDead = true;
        }
    }
    public void SetMaxHp(float value)
    {
        Stat.maxHp = value;
    }
    public void SetMaxMp(float value)
    {
        Stat.maxMp = value;
    }

    public virtual void SetMp(float value)
    {
        Stat.mp = Mathf.Clamp(value, 0, Stat.maxMp);
        UIManager.Instance.SetPlayerMPUI();
    }

    public void SetDef(float value)
    {
        Stat.defense = value;
    }

    public void SetSpeed(float value)
    {
        Stat.speed = value;
    }

    public void SetCri(float value)
    {
        Stat.critical = value;
    }
    public void AddMoney(int value)
    {
        Stat.money += value;
    }
    #endregion


    #region 레벨 관련
    public void LevelUp()
    {

    }
    public void StatUp()
    {

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
        if (isDead) return;

        implementTakeDamage();

        float damage = Mathf.Max(CriticalDamage(att) - (Stat.defense * 0.5f), 1f); // 최소 데미지 1
        SetHp(Stat.hp - damage);
    }

    public abstract void implementTakeDamage();
    #endregion


    #region 상태이상
    public void Knockback()
    {
        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackPower, ForceMode.Impulse);
        }
        forceCor = StartCoroutine(StopForceMove(1f));
    }
    public void Pull(Vector3 targetPosition)
    {
        rb.isKinematic = false;
        StartCoroutine(PullToPosition(targetPosition));
    }

    private IEnumerator PullToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Debug.Log("끌려간다");
            transform.position = Vector3.Lerp(transform.position, targetPosition, pullPower * Time.deltaTime);
            yield return null;
        }

    }
    public IEnumerator StopForceMove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IsKnockback = false;
        forceCor = null;
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
            Debug.Log(Stat.attack);
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
            Debug.Log(Stat.hp);
            yield return new WaitForSeconds(seconds);
        }
    }

    public void StartHitAttCor(float effect, float seconds)
    {
        if (hitCor == null)
        {
            hitCor = StartCoroutine(HitPerSeconds(effect, seconds));
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
    public IEnumerator HitPerSeconds(float effect, float seconds)
    {
        while (true)
        {
            TakeDamage(effect);
            Debug.Log(Stat.hp);
            yield return new WaitForSeconds(seconds);
        }

    }

    #endregion

    #region 죽음
    public abstract void Die();
    #endregion



}
