using UnityEngine;

public class Player : MonoBehaviour,IAttack
{
    public Transform characterBody;
    public Transform cameraArm;
    public Transform fist;
    PlayerAnimator playerAnimator;
    public int PlayerLayer { get; private set; }

    private bool isRun = false;
    protected bool isDead = false;

    #region ����
    float attackSpeed = 1;
    float lastClickTime = 0f;
    float attackCooldown = 1.5f;
    bool isLeft = false;
    #endregion

    #region �÷��̾� �ɷ�ġ + ������ 
    public float Luck { get; private set; }
    public float MaxHp { get; private set; }
    public float Hp { get; private set; }

    public float MaxMp { get; private set; }
    public float Mp { get; private set; }

    public float Def { get; private set; }
    public float Speed { get; private set; } // ��¥ �̵��ӵ�
    public float Cri { get; private set; }
    public float Att { get; private set; }
    #endregion

    public void Init()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<PlayerAnimator>();
        }
        playerAnimator.Init();
        ApplyEquipmentStat();
        playerAnimator.SetAttackSpeed(attackSpeed);
        PlayerLayer = 1 << LayerMask.NameToLayer("Enemy");
        
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
    void ApplyEquipmentStat() // ��� �ɷ� �߰��ؾ���
    {
    Luck = DataManager.Instance.gameData.playerData.playerStat.luck;
        MaxHp = DataManager.Instance.gameData.playerData.playerStat.maxHp;
        Hp = DataManager.Instance.gameData.playerData.playerStat.hp;

        MaxMp = DataManager.Instance.gameData.playerData.playerStat.maxMp;
        Mp = DataManager.Instance.gameData.playerData.playerStat.mp;

    Def = DataManager.Instance.gameData.playerData.playerStat.defense;
        Speed = DataManager.Instance.gameData.playerData.playerStat.speed;
        Cri = DataManager.Instance.gameData.playerData.playerStat.critical;
    Att = DataManager.Instance.gameData.playerData.playerStat.attack;
    }


    #endregion

    private void Move()
    {
        float speed = (isRun) ? (Speed * 1.5f) : Speed;
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
    public void Attack(Vector3 Tr, float Range)
    {
        
        Collider[] colliders = Physics.OverlapSphere(GameManager.Instance.player.transform.position, Range, PlayerLayer);

        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    if (colliders[i].CompareTag("Monster"))
        //    {
        //        colliders[i].GetComponent<Monster>().TakeDamage(Cri, Att);
        //    }
        //    else if (colliders[i].CompareTag("Boss"))
        //    {
        //        colliders[i].GetComponent<Boss>().TakeDamage(Cri, Att);
        //    }
        //    else
        //    {
        //        Debug.Log("�ƹ��� ����");
        //    }
        //}
    }

    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(fist.position, 1f);
    }

    public void TakeDamage(float critical, float attack) // �÷��̾��ǰ� �ٴ°�
    {
        if (!isDead)
        {
            playerAnimator.SetHit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (Def * 0.5f), 1f); // �ּ� ������ 1
            Hp -= damage;
            if (Hp < 0)
            {
                Hp = 0;
            }
        }
        else
        {
            Debug.Log("player �̹� �׾���");
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
            Debug.Log("ġ��Ÿ ����");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("ġ��Ÿ �� ����");

        }

        return criticalDamage;
    }

    #endregion
}
