using System;
using UnityEngine;
using static AllEnum;

public class Player : HumanCharacter
{
    public Transform characterBody;
    public Transform cameraArm;
    private bool isRun = false;

    
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
            // ��ų �Ŵ����� ��ų ��� ��û
            SkillManager.Instance.UseSkill(this, SkillName.AirSlash); // 1�� ��ų ���
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ��ų �Ŵ����� ��ų ��� ��û
            SkillManager.Instance.UseSkill(this, SkillName.AirCircle); // 2�� ��ų ���

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ��ų �Ŵ����� ��ų ��� ��û
            SkillManager.Instance.UseSkill(this, SkillName.Ground); // 3�� ��ų ���

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // ��ų �Ŵ����� ��ų ��� ��û
            SkillManager.Instance.UseSkill(this, SkillName.Gravity); // 4�� ��ų ���

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.Instance.uIPlayer.uiPosionSlots[0].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.uIPlayer.uiPosionSlots[1].UsePosion();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.Instance.uIPlayer.uiPosionSlots[2].UsePosion();
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    
    public override void Init()
    {
        base.Init();
        Stat = new StatData(DataManager.Instance.gameData.playerData.playerStat);
        SkillManager.Instance.UpdateSkillData(this);

    }

    public override void Activate()
    {
        base.Activate();
        Stat.SetStat(DataManager.Instance.gameData.playerData.playerStat);
        ApplyEquipmentStat();
        SetHp(Stat.maxHp);
        SetMp(Stat.maxMp);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    #region �ɷ�ġ. ���� ����
    public override void LevelUp()
    {
        StatUp();
        UIManager.Instance.PowerUpPanelOn();
    }
    public override void StatUp()
    {
        DataManager.Instance.gameData.playerData.playerStat.StatUp(1, 200, 200, 5, 3, 0.5f, 0.2f, 0, 0, 50, 50, 0.5f, 100, 0, 10);
    }

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
        if (value == 0)
        {
            Stat.ultimateGauge = 0;
        }
        else
        {
            Stat.ultimateGauge = Mathf.Clamp(Stat.ultimateGauge + value, 0, Stat.maxUltimateGauge);
        }
        DataManager.Instance.gameData.playerData.playerStat.ultimateGauge = Stat.ultimateGauge;
        UIManager.Instance.SetPlayerUltimateUI();
    }

    public void AddExp(float value)
    {
        Stat.experience += value;
        if (Stat.experience >= Stat.maxExperience)
        {
            Stat.experience -= Stat.maxExperience;
            LevelUp();
        }
        UIManager.Instance.SetPlayerEXPUI();
    }
    public override void GetAttToData()
    {
        
        Stat.attack = DataManager.Instance.gameData.playerData.playerStat.attack;
    }

    #endregion

    public void ApplyEquipmentStat() //�÷��̾� �ɷ��� �⺻ + ���  -> HP�� MP�� ���ϸ� �ȵǼ� ���� // �����Ҷ� �θ��� �� // ������ �ص� ������ 0���״� ��밡��
    {
        Stat.luck = DataManager.Instance.gameData.playerData.playerStat.luck + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Head].luck;
        Stat.maxHp = DataManager.Instance.gameData.playerData.playerStat.maxHp + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Top].maxHp;
        Stat.maxMp = DataManager.Instance.gameData.playerData.playerStat.maxMp + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Belt].maxMp;
        Stat.defense = DataManager.Instance.gameData.playerData.playerStat.defense + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Bottom].defense;
        Stat.speed = DataManager.Instance.gameData.playerData.playerStat.speed + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Shoes].speed;
        Stat.critical = DataManager.Instance.gameData.playerData.playerStat.critical + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Gloves].critical;
        Stat.attack = DataManager.Instance.gameData.playerData.playerStat.attack + DataManager.Instance.gameData.invenDatas.EquipItemDatas[ItemList.Weapon].attack;
    }
    private void Move()
    {
        float speed = (isRun) ? (Stat.speed * 1.5f) : Stat.speed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((isRun) ? 1 : 0.5f) * moveInput.magnitude;
        animator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        animator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * speed * Time.deltaTime;
    }

    #region ���� & �ǰ�
    public override void ImplementTakeDamage()
    {
        animator.SetHit();
    }
    #endregion


    #region ����
    public override void Die()
    {
        GameManager.Instance.SetstageStart(false);
        UIManager.Instance.ActivePlayerEndPanel();
    }
    #endregion


}
