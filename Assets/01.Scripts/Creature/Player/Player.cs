using System.Collections;
using UnityEngine;


//Time.timeScale = 0f; �ð��� �帧�� ���� , //�Ƚ���, �ڷ�ƾ �ȵǰ�, ������Ʈ�ǰ� , �巡�׵� ����
public class Player : Creature
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
    public int PlayerLayer;
    public int PassiveCurrentNum;
    public Coroutine passiveCor { get; private set; }



    public float Luck { get; private set; }
    public float MaxHp { get; private set; }
    public float Hp { get; private set; }

    public float MaxMp { get; private set; }
    public float Mp { get; private set; }

    public float Def { get; private set; }
    public float Speed { get; private set; }
    public float Cri { get; private set; }
    public float Att { get; private set; }

    #region ReInitialize
    public override void Init()
    {
        isDead = false;
        isLeft = false;
        run = false;
        attackSpeed = 1;
        lastClickTime = 0f;
        attackCooldown = 1.5f;
        playerStat = new PlayerStat(soOriginPlayer); //�÷��̾� ������ ����
        CalcPlayerStat();
        UiManager.Instance.SetGameUI(); //�����Ϳ� ���� ui����
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<PlayerAnimator>();
        }
        if (fist == null)
        {
            fist = transform.GetChild(0).GetChild(3);
        }
        playerAnimator.Starts();
        playerAnimator.SetAttackSpeed(attackSpeed);
        PlayerLayer = 1 << LayerMask.NameToLayer("Enemy");
    }

    public override void ReInit()
    {

    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    #endregion
    public void AllCorReset()
    {
        PassiveCorReset();

    }
    public void PassiveCorReset()
    {
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
    }
    public void DoPassive()
    {
        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(DoPassive(true));
        }
    }
    IEnumerator DoPassive(bool isPlayer)
    {
        while (GameManager.Instance.stageStart)
        {
            PassiveSkill ps = SkillManager.Instance.CallPassiveSkill(isPlayer);
            yield return new WaitForSeconds(ps.skillStat.duration);
            ps.DoReset();
        }
    }

 
    public void StageStartInit()
    {
        PassiveCurrentNum = Random.Range((int)AllEnum.SkillName.Fire, (int)AllEnum.SkillName.End); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (GameManager.Instance.stageStart)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                playerStat.AddExp(10);
            }
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

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SkillManager.Instance.UseActiveSKill(AllEnum.SkillName.AirSlash, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SkillManager.Instance.UseActiveSKill(AllEnum.SkillName.AirCircle, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SkillManager.Instance.UseActiveSKill(AllEnum.SkillName.Ground, true);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (playerStat.ultimateGauge >= playerStat.maxUltimateGauge)
                {
                    SkillManager.Instance.UseActiveSKill(AllEnum.SkillName.Gravity, true);
                }
                else
                {
                    Debug.Log("��ų ����");
                }
            }
            
            if (Input.anyKeyDown)
            {
                // ���� �ƹ� Ű�� �Էµ��� �ʾ��� ��쿡�� '\0'�� ��ȯ null��Ȱ
                char keyPressed = Input.inputString.Length > 0 ? Input.inputString[0] : '\0';

                // ScreenOnOff �Լ��� ���� Ű�� �Ű������� �����Ͽ� ȣ��
                UiManager.Instance.ScreenOnOff(keyPressed);
            }
        }
    }
    public void CatchMonster(float monsterexp, int _money)
    {
        playerStat.KillMonster(monsterexp, _money, 10);
        if (playerStat.experience >= playerStat.maxExperience)
        {
            LevelUp();
            UiManager.Instance.ShowPowerUpPanel();
        }
    }
    #region ��ȭ �ɷ�
    public void CalculateLuck()
    {
        Luck = playerStat.luck + InventoryManager.Instance.equipList[0].itemStat.luck;
    }

    public void CalculateMaxHp()
    {
        MaxHp = playerStat.maxHealth + InventoryManager.Instance.equipList[1].itemStat.maxHealth;
    }
    public void SetHp(float hp)
    {
        Hp = hp;
        if (Hp > MaxHp)
        {
            playerStat.SetHealth(MaxHp);
        }
        UiManager.Instance.playerConditionUI.SetUI();

    }
    public void CalculateMaxMp()
    {
        MaxMp = playerStat.maxMana + InventoryManager.Instance.equipList[2].itemStat.maxMana;
    }
    public void SetMp(float mana)
    {
        Mp = mana;
        if (Mp > MaxMp)
        {
            playerStat.SetMana(MaxMp);
        }
        UiManager.Instance.playerConditionUI.SetUI();

    }

    public void CalculateDef()
    {
        Def = playerStat.defense + InventoryManager.Instance.equipList[3].itemStat.defence;
    }

    public void CalculateSpeed()
    {
        Speed = playerStat.movementSpeed + InventoryManager.Instance.equipList[4].itemStat.speed;
    }

    public void CalculateCri()
    {
        Cri = playerStat.criticalChance + InventoryManager.Instance.equipList[5].itemStat.critical;
    }

    public void CalculateAtt()
    {
        Att = playerStat.attack + InventoryManager.Instance.equipList[6].itemStat.attack;
    }
    public void CalcPlayerStat()
    {
        CalculateLuck();
        CalculateMaxHp();
        Hp = MaxHp;
        CalculateMaxMp();
        Mp = MaxMp;
        CalculateDef();
        CalculateSpeed();
        CalculateCri();
        CalculateAtt();
    }
    #endregion
    public IEnumerator HealHpMp()
    {
        while (true)
        {
            SetHp(Hp + 10);
            SetMp(Mp + 10);
            UiManager.Instance.playerConditionUI.SetUI();
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

    #region ������
    public override void LevelUp()
    {
        if (playerStat != null)
        {
            playerStat.SetExp(playerStat.maxExperience - playerStat.experience);
            playerStat.LevelUp(); // ���� 1�� �ø��� �Լ�
            StatUp();
        }
    }

    public override void StatUp()
    {
        playerStat.AddMaxHealth(100f);
        playerStat.AddMaxMana(100f);
        playerStat.AddMaxExperience(100f);
        CalcPlayerStat();
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

    public override void Attack(Vector3 Tr, float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, PlayerLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(Cri, Att);
            }
            else if (colliders[i].CompareTag("Boss"))
            {
                colliders[i].GetComponent<Boss>().TakeDamage(Cri, Att);
            }
        }
    }

    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(fist.position, 1f);
    }


   
    #endregion

    #region �°� �װ�
    public override void TakeDamage(float critical, float attack) // �÷��̾��ǰ� �ٴ°�
    {
        if (!isDead)
        {
            Hit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (Def * 0.5f), 1f); // �ּ� ������ 1
            float hp = Hp - damage;
            SetHp(hp);
            if (Hp < 0)
            {
                SetHp(0);
                Dead(false);
            }
        }
        else
        {
            Debug.Log("player �̹� �׾���");
        }
        UiManager.Instance.playerConditionUI.SetUI();
    }

    public void Hit()
    {
        playerAnimator.SetHit();
    }

    public override void Dead(bool force)
    {
        isDead = true;
        GameManager.Instance.SetGameOver();
        MonsterManager.Instance.StopSpawnMonster();
        MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
        InventoryManager.Instance.AllDataRemove();
        UiManager.Instance.ActiveEndPanel();
        //GameManager.Instance.StopBGM();
    }

        
    

    #endregion

}
