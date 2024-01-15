using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Time.timeScale = 0f; �ð��� �帧�� ���� , //�Ƚ���, �ڷ�ƾ �ȵǰ�, ������Ʈ�ǰ� , �巡�׵� ����
public class Player : MonoBehaviour, IAttack, IDead, ILevelUp
{
    public SOPlayer soOriginPlayer;
    [SerializeField]
    public PlayerStat playerStat { get; private set; }
    PlayerAnimator playerAnimator;

    public Transform characterBody;
    public Transform cameraArm;
    public Transform fist;

    private bool run;
    float speed;

    private float attackSpeed = 1;
    private float lastClickTime = 0f;
    private float attackCooldown = 1.5f;
    bool isLeft = false;
    bool isDead = false;
    Coroutine passiveCor;
    Coroutine HealHpMpCor;


    public float Luck => playerStat.luck + InventoryManager.Instance.equipList[0].itemStat.luck;
    public float MaxHp => playerStat.maxHealth + InventoryManager.Instance.equipList[1].itemStat.maxHealth;
    public float MaxMp => playerStat.maxMana + InventoryManager.Instance.equipList[2].itemStat.maxMana;
    public float Def => playerStat.defense + InventoryManager.Instance.equipList[3].itemStat.defence;
    public float Speed => playerStat.movementSpeed + InventoryManager.Instance.equipList[4].itemStat.speed;
    public float Cri => playerStat.criticalChance + InventoryManager.Instance.equipList[5].itemStat.critical;
    public float Att => playerStat.attack + InventoryManager.Instance.equipList[6].itemStat.attack;



    void Start()
    {
        playerStat = new PlayerStat(soOriginPlayer);
        playerAnimator = GetComponent<PlayerAnimator>();
        fist = transform.GetChild(0).GetChild(3);
        playerAnimator.Starts();
        playerAnimator.SetAttackSpeed(attackSpeed);
        passiveCor = StartCoroutine(TimeLapseAttack(2.8f, 1f));

        //playerStat.SetValues(soOriginPlayer);
        playerStat.ShowInfo();
        HealHpMpCor = StartCoroutine(HealHpMp());

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InventoryManager.Instance.UseItem(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InventoryManager.Instance.UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackSpeed += 0.1f;
            playerAnimator.SetAttackSpeed(attackSpeed);
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (passiveCor != null)
        //    {
        //        StopCoroutine(passiveCor);
        //        Debug.Log("����");
        //        passiveCor = null;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (HealHpMpCor != null)
            {
                StopCoroutine(HealHpMpCor);
                Debug.Log("ȸ�� ����");
                HealHpMpCor = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerStat.AddExp(100);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.AirSlash);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.AirCircle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillManager.Instance.UseSKill(AllEnum.SkillName.Ground);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (playerStat.ultimateGauge >= playerStat.maxUltimateGauge)
            {
                Debug.Log("��ų ��� ����");

                SkillManager.Instance.UseSKill(AllEnum.SkillName.Gravity);
            }
            else
            {
                Debug.Log("��ų ����");
            }
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
        if (Input.anyKeyDown)
        {
            // ���� �ƹ� Ű�� �Էµ��� �ʾ��� ��쿡�� '\0'�� ��ȯ null��Ȱ
            char keyPressed = Input.inputString.Length > 0 ? Input.inputString[0] : '\0';

            // ScreenOnOff �Լ��� ���� Ű�� �Ű������� �����Ͽ� ȣ��
            UiManager.Instance.ScreenOnOff(keyPressed);
        }
        if (playerStat.experience >= playerStat.maxExperience) // �̰� ������Ʈ �������� ������
        {
            LevelUp();
            UiManager.Instance.ShowPowerUpPanel();
        }
    }
    public IEnumerator HealHpMp()
    {
        while (true)
        {
            playerStat.AddHp(10);
            playerStat.AddMp(10);
            yield return new WaitForSeconds(2);
        }
    }
    private void Move()
    {
        speed = (run) ? (Speed * 1.5f) : Speed;
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
    public void UseMana(float mana)
    {
        playerStat.MinusMana(mana);
    }

    #region ������
    public void LevelUp()
    {
        if (playerStat != null)
        {
            playerStat.SetExp(playerStat.maxExperience - playerStat.experience);
            playerStat.LevelUp(); // ���� 1�� �ø��� �Լ�
            StatUp();
        }
    }

    public void StatUp()
    {
        playerStat.AddMaxHealth(100f);
        playerStat.AddMaxMana(100f);
        playerStat.AddMaxExperience(100f);
    }
    #endregion

    #region ����
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

    public void Attack(Vector3 pos, float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(pos, Range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(Cri, Att);
                colliders[i].GetComponent<Monster>().isHit = true;
            }
        }
    }

    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(fist.position, 1f);
    }

    public IEnumerator TimeLapseAttack(float attackRange, float delayTime)
    {
        while (true)
        {
            Attack(transform.position, attackRange);
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
    #endregion

    #region �°� �װ�
    public virtual void TakeDamage(float critical, float attack) // �÷��̾��ǰ� �ٴ°�
    {
        if (!isDead)
        {
            Hit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (Def * 0.5f), 1f); // �ּ� ������ 1
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



    #endregion

}
