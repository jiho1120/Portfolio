using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���� ����
// ��ų ��Ÿ�ӵ��� ������ �ϱ�
// �߷� ȿ�� ��ġ��
// �׶��� ���� ��ġ��
// ������ ����� �κ� 
public class Player : MonoBehaviour, IAttack, IDead
{
    public SOPlayer soOriginPlayer;
    public PlayerStat playerStat { get; private set; }
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
        passiveCor = StartCoroutine(TimeLapseAttack(2.8f, 1f));

        playerStat.SetValues(soOriginPlayer);
        //playerStat.ShowInfo();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("fixed update");
        Move();
    }

    private void Update() 
    {
        //Move();
        //Debug.Log("������ : "+Input.GetKey(KeyCode.W));
            

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
            SkillManager.Instance.UseSKill(AllEnum.SkillName.Ground);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.AirSlash);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.AirCircle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.Gravity);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AllEnum.ItemType name = AllEnum.ItemType.Head;
            for (int i = 0; i < InventoryManager.Instance.equipList.Length; i++)
            {
                Equip eq = InventoryManager.Instance.equipList[i];
                if (eq.itemType == name)
                {
                    eq.exp += 5;
                }
                if (eq.exp >= eq.maxExp)
                {
                    eq.LevelUp();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager.Instance.invenOn = !InventoryManager.Instance.invenOn;
            InventoryManager.Instance.InvenOnOff();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 0f; // �ð��� �帧�� ���� , //�Ƚ���, �ڷ�ƾ �ȵǰ�, ������Ʈ�ǰ� , �巡�׵� ����
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Time.timeScale = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 1f;
        }
    }
    private void Move()
    {
        speed = (run) ? (playerStat.movementSpeed * 1.5f) : playerStat.movementSpeed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));        
        Debug.Log("dddd" +moveInput);
        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        playerAnimator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        playerAnimator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * speed * 2f;
        //transform.position += moveDir * speed * Time.deltaTime;


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
    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(fist, 1f);
    }

    public IEnumerator TimeLapseAttack(float attackRange, float delayTime)
    {
        while (true)
        {
            Attack(this.transform, attackRange);
            yield return new WaitForSeconds(delayTime);
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
        }
        else
        {
            criticalDamage = attack;
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
