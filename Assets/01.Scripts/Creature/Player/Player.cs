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

    #region 공격
    float attackSpeed = 1;
    float lastClickTime = 0f;
    float attackCooldown = 1.5f;
    bool isLeft = false;
    #endregion

    #region 플레이어 능력치 + 아이템 
    public float Luck { get; private set; }
    public float MaxHp { get; private set; }
    public float Hp { get; private set; }

    public float MaxMp { get; private set; }
    public float Mp { get; private set; }

    public float Def { get; private set; }
    public float Speed { get; private set; } // 진짜 이동속도
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

    #region 능력치
    void ApplyEquipmentStat() // 장비 능력 추가해야함
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
        //        Debug.Log("아무도 없음");
        //    }
        //}
    }

    public void AttackRange() // 애니메이션에 넣음
    {
        Attack(fist.position, 1f);
    }

    public void TakeDamage(float critical, float attack) // 플레이어피가 다는거
    {
        if (!isDead)
        {
            playerAnimator.SetHit();
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (Def * 0.5f), 1f); // 최소 데미지 1
            Hp -= damage;
            if (Hp < 0)
            {
                Hp = 0;
            }
        }
        else
        {
            Debug.Log("player 이미 죽었어");
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
            Debug.Log("치명타 터짐");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("치명타 안 터짐");

        }

        return criticalDamage;
    }

    #endregion
}
