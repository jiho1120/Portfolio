using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 문제점
// 또 같은 팔로 공격

//공격방식
// 콜라이더 주먹에 넣어놓고 공격시 켜서 닿으면 딜넣기
// 사거리 설정, 정면에 레이를 쏴서 레이에 맞은애가 적이고 사거리 안이면 딜 넣기


public class PlayerController : MonoBehaviour, IAttack
{
    PlayerStat playerStat;
    PlayerAnimator playerAnimator;

    public Transform characterBody;
    public Transform cameraArm;
    Rigidbody rb;


    public bool run;
    public float speed = 5;
    public float runSpeed = 8f;
    public float finalSpeed;

    private float attackSpeed = 1;
    private float lastClickTime = 0f;
    private float attackCooldown = 1.5f;
    bool isLeft = false;

    bool isDead = false;


    void Start()
    {
        playerStat = new PlayerStat();
        playerAnimator =GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody>();
        playerAnimator.SetAttackSpeed(attackSpeed);


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
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            attackSpeed += 0.1f;
            playerAnimator.SetAttackSpeed(attackSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Q)) // 체력 안바뀜
        {
            TakeDamage(playerStat.criticalChance, playerStat.attack);
            Debug.Log(playerStat.health);
        }

        
    }


    private void Move()
    {
        finalSpeed = (run) ? runSpeed : speed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        playerAnimator.WalkOrRun(percent);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        playerAnimator.MoveAnim(moveInput.y, moveInput.x);

        characterBody.forward = lookForward;
        transform.position += moveDir * Time.deltaTime * 5f;
        
    }
    
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
            Debug.Log("크리 뜸");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("크리 안 뜸");

        }

        return criticalDamage;
    }
    public virtual void Hit(float critical, float attack)
    {
        TakeDamage(playerStat.criticalChance, playerStat.attack);
    }
    public virtual void TakeDamage(float critical, float attack)
    {
        if (!isDead)
        {
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (playerStat.defense * 0.5f), 0f); // 최소 데미지 0
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
        
    }

    public virtual void Dead()
    {
        isDead = true;
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); 고쳐야함
        Debug.Log("죽음");
    }
}
