using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���� ����
// ����ġ�� �� �ְ� �޴� �Լ� �����
// ��ų �����Ű�� ���̳� �˹� �����Ű��
//1�϶� 0.5�� �þ  7, 14
// ��ų �ð��� �� �Ǹ� ������(false)
// ��ų �ɷ� ����

public class Player : MonoBehaviour, IAttack, IDead
{
    public SOPlayer soOriginPlayer;
    PlayerStat playerStat;
    PlayerAnimator playerAnimator;
    Rigidbody rb;

    public Transform characterBody;
    public Transform cameraArm;
    public Transform fist;

    private bool run;
    private float speed;

    private float attackSpeed = 1;
    private float lastClickTime = 0f;
    private float attackCooldown = 1.5f;
    bool isLeft = false;
    bool isDead = false;
    Coroutine passiveCor;

    void Start()
    {
        playerStat = new PlayerStat();
        playerAnimator = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody>();
        fist = transform.GetChild(0).GetChild(3);
        playerAnimator.Starts();
        playerAnimator.SetAttackSpeed(attackSpeed);
        passiveCor = StartCoroutine(PassiveSkill());

        playerStat.SetValues(soOriginPlayer);
        //playerStat.ShowInfo();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackSpeed += 0.1f;
            playerAnimator.SetAttackSpeed(attackSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (passiveCor != null)
            {
                StopCoroutine(passiveCor);
                Debug.Log("����");
                passiveCor = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Skill skill = SkillManager.Instance.SetSkillPos(AllEnum.SkillName.Ground, transform.position);
            Debug.Log("��ų �ߵ�"+skill.gameObject.GetHashCode());                        
            Debug.Log("��ų " + skill.GetHashCode());
            skill.gameObject.SetActive(true);

            Debug.Log("��ų �ߵ�???? " + skill.gameObject.activeSelf);
            skill.DoSkill();
            //StartCoroutine(SkillManager.Instance.UseSkill(skill)); //�ڱⰡ ������ �� �ð��� �Ŵ������� �� �����ٰ� �θ���...
        }
        
    }




    private void Move()
    {
        speed = (run) ? (playerStat.movementSpeed * 1.5f) : playerStat.movementSpeed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        playerAnimator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        playerAnimator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * speed * Time.deltaTime;

    }

    void BasicAttack()
    {
        //Ŭ���Ҷ����� �����ð��� ���ؼ� ���Ӱ��ݻ��¸� ���� �ָ����� �����ϰ�
        //���Ӱ��ݳ��� �ð��� �ƴϸ� ù�ָ�����.

        float timeSinceLastClick = Time.time - lastClickTime;

        // 1�ʵ����� �ٵ� ���ǵ尡 ������
        // �ִϸ��̼� ���ǵ尡 �ö󰡼� �ִϸ��̼ǵ� ���� ����
        float animTime = 1f / attackSpeed; // �ٲ� �ִϸ��̼� �ð� = �ִϸ��̼� �ð�(1��) / �ִϸ��̼� ���ǵ�

        //�����ϴ� ������ �ð��̸� �ǵ���������
        if (timeSinceLastClick <= animTime)
        {
            return;
        }
        else //�װ� �ƴ϶��
        {
            if (timeSinceLastClick <= attackCooldown) //���Ӱ���
            {
                if (isLeft)
                {
                    playerAnimator.LeftAttack();
                }
                else
                {
                    playerAnimator.RightAttack();
                }
                isLeft = !isLeft;
            }
            else
            {
                isLeft = false;
                playerAnimator.RightAttack();
            }
            lastClickTime = Time.time;

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(fist.position, 1f);

    //}

    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(fist, 1f);
        //Debug.Log("��Ÿ");
    }

    public virtual void Attack(Transform Tr, float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(Tr.position, Range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(playerStat.criticalChance, playerStat.attack);
                colliders[i].GetComponent<Monster>().isHit = true;
            }
        }
    }

    public IEnumerator PassiveSkill()
    {
        while (true)
        {
            Attack(this.transform, 2.8f);
            //Debug.Log("�нú�");
            yield return new WaitForSeconds(1f);
        }
    }

    public bool CheckCritical(float critical)
    {
        bool isCritical = Random.Range(0f, 100f) < critical;
        return isCritical;

    }
    public float CriticalDamage(float critical, float attack)
    {
        float criticalDamage = 0;
        if (CheckCritical(critical))
        {
            criticalDamage = attack * 2;
            //Debug.Log("ũ�� ��");
        }
        else
        {
            criticalDamage = attack;
            //Debug.Log("ũ�� �� ��");
        }

        return criticalDamage;
    }

    public virtual void TakeDamage(float critical, float attack) // �÷��̾��ǰ� �ٴ°�
    {
        if (!isDead)
        {
            Hit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (playerStat.defense * 0.5f), 0f); // �ּ� ������ 0
            float hp = playerStat.health - damage;
            playerStat.SetHealth(hp);
            if (playerStat.health < 0)
            {
                playerStat.SetHealth(0);
                Dead();
            }
        }
        else
        {
            Debug.Log("�̹� �׾���");
        }
        print("�÷��̾� ü��" + playerStat.health);
    }

    public void Hit()
    {
        playerAnimator.SetHit();
    }

    public virtual void Dead()
    {
        isDead = true;
        Debug.Log("����");
    }

    public bool IsDead()
    {
        return isDead;

    }
}
