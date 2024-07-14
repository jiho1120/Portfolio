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

    // Knockback ���� ���� �� �Լ�
    #region Knockback ���� �Լ�
    private bool isKnockback = false;
    public bool IsKnockback { get => isKnockback; set => isKnockback = value; }
    private Vector3 knockbackDirection;
    public Vector3 KnockbackDirection { get => knockbackDirection; set => knockbackDirection = value; }
    private float knockbackPower;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }
    #endregion

    #region pull ���� �Լ�

    bool isPull = false;
    public bool IsPull { get => isPull; set => isPull = value; }
    private Vector3 pullPosition;
    public Vector3 PullPosition { get => pullPosition; set => pullPosition = value; }
    private float pullPower;
    public float PullPower { get => pullPower; set => pullPower = value; }
    private float startTime;
    private float journeyLength;
    #endregion

    #region �ڷ�ƾ ����
    protected Coroutine deActiveCor = null;
    protected Coroutine knockbackCor = null;
    protected Coroutine pullCor = null;

    protected Coroutine deCreaseAttCor = null;
    protected Coroutine hpCor = null;
    protected Coroutine hitCor = null;

    #endregion


    #region �ʱ�ȭ
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

    #region Set�Լ�
    public void SetAtt(float effect)
    {
        Stat.attack = effect;
        if (Stat.attack <= 0)
        {
            // �ּ� ���ݷ�
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

    #region ���� ����
    public virtual void LevelUp()
    {
        StatUp();
    }
    public abstract void StatUp();

    #endregion

    #region �þ�
    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - transform.position;
        dir.y = 0;

        return dir;
    }
    #endregion

    #region ����

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

        float damage = Mathf.Max(CriticalDamage(att) - (Stat.defense * 0.5f), 1f); // �ּ� ������ 1
        SetHp(Stat.hp - damage);
    }

    public abstract void ImplementTakeDamage();
    #endregion


    #region �����̻�
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

            // Mathf.SmoothStep�� ����Ͽ� �ε巴�� �̵�
            float smoothFraction = Mathf.SmoothStep(0, 1, fractionOfJourney);
            transform.position = Vector3.Lerp(transform.position, pullPosition, smoothFraction);
            yield return null;
        }

    }

    public void StopPull()
    {
        // �ܺ������� �ȿ����̰���
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

    // ���ݷ� ��ȭ�� �ٽ� ����(�����Ϳ� �����) ���ݷ� �������� �Լ�
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

    public void StartHitAttCor(float effect, float seconds, int count = 1000)// Ƚ�� �ȳ����� ��ų ���������� ���� 1000������ �����
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

    #region ����
    public abstract void Die();
    public bool SetDead(bool active)
    {
        return isDead = active;
    }

    #endregion
}
