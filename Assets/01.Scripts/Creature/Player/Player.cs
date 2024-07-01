using UnityEngine;
using static AllEnum;

public class Player : UseSKillCharacter
{
    public Transform characterBody;
    public Transform cameraArm;
    public Transform skillPos;
    PlayerAnimator playerAnimator;


    private bool isRun = false;

    #region 공격
    float attackSpeed = 1;
    float lastClickTime = 0f;
    float attackCooldown = 1.5f;
    bool isLeft = false;
    #endregion

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 스킬 매니저에 스킬 사용 요청
            SkillManager.Instance.UseSkill(this, SkillName.AirSlash); // 1번 스킬 사용
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 스킬 매니저에 스킬 사용 요청
            SkillManager.Instance.UseSkill(this, SkillName.AirCircle); // 2번 스킬 사용
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // 스킬 매니저에 스킬 사용 요청
            SkillManager.Instance.UseSkill(this, SkillName.Ground); // 3번 스킬 사용
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // 스킬 매니저에 스킬 사용 요청
            SkillManager.Instance.UseSkill(this, SkillName.Gravity); // 4번 스킬 사용
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[0].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[1].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.Instance.uIPlayer.uIPosionSlots[2].UsePosion();
        }


        
    }

    void FixedUpdate()
    {
        Move();
    }
    public void Init()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<PlayerAnimator>();
        }
        playerAnimator.Init();
        Stat = new StatData(DataManager.Instance.gameData.playerData.playerStat);
        playerAnimator.SetAttackSpeed(attackSpeed);
        AttackRange = 1f;
        EnemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
        SkillManager.Instance.SetSkillData(ObjectType.Player);

    }
    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    #region 능력치
    public override void SetHp(float hp)
    {
        base.SetHp(hp);
        UIManager.Instance.SetPlayerHPUI();
        if (hp <= 0)
        {
            Die();
        }
    }
    public override void SetMp(float value)
    {
        base.SetMp(value);
        UIManager.Instance.SetPlayerMPUI();

    }
    public void SetUltimate(float value)
    {
        Stat.ultimateGauge = Mathf.Clamp(Stat.ultimateGauge + value, 0, Stat.maxUltimateGauge);
    }
    public override void GetAttToData()
    {
        Stat.attack = DataManager.Instance.gameData.playerData.playerStat.attack;
    }

    #endregion
    public void ApplyEquipmentStat() //플레이어 능력은 기본 + 장비  -> HP랑 MP가 변하면 안되서 없음 // 장착할때 부르면 됨
                                     // 해제를 해도 어차피 0일테니 사용가능
    {
        DataManager.Instance.gameData.playerData.playerStat.luck += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Head].luck;
        DataManager.Instance.gameData.playerData.playerStat.maxHp += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Top].maxHp;
        DataManager.Instance.gameData.playerData.playerStat.maxMp += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Belt].maxMp;
        DataManager.Instance.gameData.playerData.playerStat.defense += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Bottom].defense;
        DataManager.Instance.gameData.playerData.playerStat.speed += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Shoes].speed;
        DataManager.Instance.gameData.playerData.playerStat.critical += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Gloves].critical;
        DataManager.Instance.gameData.playerData.playerStat.attack += DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Weapon].attack;
    }
    private void Move()
    {
        float speed = (isRun) ? (Stat.speed * 1.5f) : Stat.speed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((isRun) ? 1 : 0.5f) * moveInput.magnitude;
        playerAnimator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        playerAnimator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * speed * Time.deltaTime;
    }

    #region 공격 & 피격
    void BasicAttack()
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
                playerAnimator.RightAttack();
                isLeft = true;

            }
            lastClickTime = Time.time;

        }
    }
    
    public override void implementTakeDamage()
    {
        playerAnimator.SetHit();
    }
    #endregion


    #region 죽음
    public override void Die()
    {

    }
    #endregion


}
