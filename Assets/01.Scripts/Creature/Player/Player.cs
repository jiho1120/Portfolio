using System;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : Creature, Initialize
{
    public Transform characterBody;
    public Transform cameraArm;
    PlayerAnimator playerAnimator;


    private bool isRun = false;

    #region ����
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

    #region �ɷ�ġ

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
    public void ApplyEquipmentStat() //�÷��̾� �ɷ��� �⺻ + ���  -> HP�� MP�� ���ϸ� �ȵǼ� ���� // �����Ҷ� �θ��� ��
                                     // ������ �ص� ������ 0���״� ��밡��
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

    #region ���� & �ǰ�
    void BasicAttack()
    {
        //Ŭ���Ҷ����� �����ð��� ���ؼ� ���Ӱ��ݻ��¸� ���� �ָ����� �����ϰ�
        //���Ӱ��ݳ��� �ð��� �ƴϸ� ù�ָ�����.
        float TimeDifference = Time.time - lastClickTime;

        // 1�ʵ����� �ٵ� ���ǵ尡 ������
        // �ִϸ��̼� ���ǵ尡 �ö󰡼� �ִϸ��̼ǵ� ���� ����
        float animTime = 1f / attackSpeed; // �ٲ� �ִϸ��̼� �ð� = �ִϸ��̼� �ð�(1��) / �ִϸ��̼� ���ǵ�

        //�����ϴ� ������ �ð��̸� �ǵ���������
        if (TimeDifference <= animTime)
        {
            return;
        }
        else //�װ� �ƴ϶��
        {
            if (TimeDifference <= attackCooldown) //���Ӱ���
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
    public override void TakeDamage() // �÷��̾��ǰ� �ٴ°�
    {
        if (!isDead)
        {
            playerAnimator.SetHit();
            float damage = Mathf.Max(CriticalDamage() - (Stat.defense * 0.5f), 1f); // �ּ� ������ 1
            Stat.hp -= damage;
            UIManager.Instance.SetPlayerHPUI();
            if (Stat.hp < 0)
            {
                Stat.hp = 0;
            }
        }
        else
        {
            Debug.Log("player �̹� �׾���");
        }
    }

    #endregion



    #region ����
    public override void Die()
    {

    }
    #endregion


}
