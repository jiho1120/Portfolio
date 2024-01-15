using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Time.timeScale = 0f; 시간의 흐름이 멈춤 , //픽스드, 코루틴 안되고, 업데이트되고 , 드래그도 가능
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
        //        Debug.Log("멈춤");
        //        passiveCor = null;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (HealHpMpCor != null)
            {
                StopCoroutine(HealHpMpCor);
                Debug.Log("회복 멈춤");
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
                Debug.Log("스킬 사용 가능");

                SkillManager.Instance.UseSKill(AllEnum.SkillName.Gravity);
            }
            else
            {
                Debug.Log("스킬 못씀");
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
            // 만약 아무 키도 입력되지 않았을 경우에는 '\0'가 반환 null역활
            char keyPressed = Input.inputString.Length > 0 ? Input.inputString[0] : '\0';

            // ScreenOnOff 함수에 누른 키를 매개변수로 전달하여 호출
            UiManager.Instance.ScreenOnOff(keyPressed);
        }
        if (playerStat.experience >= playerStat.maxExperience) // 이걸 업데이트 조건으로 뺴야함
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

    #region 레벨업
    public void LevelUp()
    {
        if (playerStat != null)
        {
            playerStat.SetExp(playerStat.maxExperience - playerStat.experience);
            playerStat.LevelUp(); // 레벨 1만 올리는 함수
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

    #region 공격
    void BasicAttack()
    {
        //클릭할때마다 이전시간과 비교해서 연속공격상태면 다음 주먹으로 변경하고
        //연속공격내의 시간이 아니면 첫주먹으로.

        float timeSinceLastClick = Time.time - lastClickTime;

        // 1초동안함 근데 스피드가 증가함
        // 애니메이션 스피드가 올라가서 애니메이션도 빨리 끝남
        float animTime = 1f / attackSpeed; // 바뀐 애니메이션 시간 = 애니메이션 시간(1초) / 애니메이션 스피드

        //동작하는 동안의 시간이면 되돌려보내고
        if (timeSinceLastClick <= animTime)
        {
            return;
        }
        else //그게 아니라면
        {
            if (timeSinceLastClick <= attackCooldown) //연속공격
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

    public void AttackRange() // 애니메이션에 넣음
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

    #region 맞고 죽고
    public virtual void TakeDamage(float critical, float attack) // 플레이어피가 다는거
    {
        if (!isDead)
        {
            Hit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (Def * 0.5f), 1f); // 최소 데미지 1
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
            Debug.Log("이미 죽었어");
        }
        print("플레이어 체력" + playerStat.health);
    }

    public void Hit()
    {
        playerAnimator.SetHit();
    }

    public virtual void Dead()
    {
        isDead = true;
        Debug.Log("죽음");
    }

    public bool IsDead()
    {
        return isDead;
    }



    #endregion

}
