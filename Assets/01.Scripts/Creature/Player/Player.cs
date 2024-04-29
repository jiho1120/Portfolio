using System;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : Creature, Initialize
{
    public Transform characterBody;
    public Transform cameraArm;
    PlayerAnimator playerAnimator;


    private bool isRun = false;

    #region 공격
    float attackSpeed = 1;
    float lastClickTime = 0f;
    float attackCooldown = 1.5f;
    bool isLeft = false;
    #endregion


    public override void Init()
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

    }
    public override void Activate()
    { }
    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
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
    }

    void FixedUpdate()
    {
        Move();
    }

    #region 능력치

    public void SetLuck(float value)
    {
        Stat.luck = value;
    }

    public void SetMaxHp(float value)
    {
        Stat.maxHp = value;
    }

    public void SetHp(float value)
    {
        Stat.hp = value;
        if (Stat.hp > Stat.maxHp)
        {
            Stat.hp = Stat.maxHp;
        }
        Debug.Log(Stat.hp);
        UIManager.Instance.SetPlayerHPUI();
    }

    public void SetMaxMp(float value)
    {
        Stat.maxMp = value;
    }

    public void SetMp(float value)
    {
        Stat.mp = value;
        if (Stat.mp > Stat.maxMp)
        {
            Stat.mp = Stat.maxMp;
        }
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

    public void SetAtt(float value)
    {
        Stat.attack = value;
    }

    public void SetUltimate(float value)
    {
        Stat.ultimateGauge += value;
        if (Stat.ultimateGauge > Stat.maxUltimateGauge)
        {
            Stat.ultimateGauge = Stat.maxUltimateGauge;
        }
    }

    #endregion
    public void ApplyEquipmentStat() //플레이어 능력은 기본 + 장비  -> HP랑 MP가 변하면 안되서 없음 // 장착할때 부르면 됨
                                     // 해제를 해도 어차피 0일테니 사용가능
    {
        DataManager.Instance.gameData.playerData.playerStat.luck += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Head].luck;
        DataManager.Instance.gameData.playerData.playerStat.maxHp += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Top].maxHp;
        DataManager.Instance.gameData.playerData.playerStat.maxMp += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Belt].maxMp;
        DataManager.Instance.gameData.playerData.playerStat.defense += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Bottom].defense;
        DataManager.Instance.gameData.playerData.playerStat.speed += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Shoes].speed;
        DataManager.Instance.gameData.playerData.playerStat.critical += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Gloves].critical;
        DataManager.Instance.gameData.playerData.playerStat.attack += DataManager.Instance.gameData.invenDatas.EquipItemDatas[AllEnum.ItemList.Weapon].attack;
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
    public override void TakeDamage() // 플레이어피가 다는거
    {
        if (!isDead)
        {
            playerAnimator.SetHit();
            float damage = Mathf.Max(CriticalDamage() - (Stat.defense * 0.5f), 1f); // 최소 데미지 1
            Stat.hp -= damage;
            UIManager.Instance.SetPlayerHPUI();
            if (Stat.hp < 0)
            {
                Stat.hp = 0;
            }
        }
        else
        {
            Debug.Log("player 이미 죽었어");
        }
    }

    #endregion



    #region 죽음
    public override void Die()
    {

    }
    #endregion


}
