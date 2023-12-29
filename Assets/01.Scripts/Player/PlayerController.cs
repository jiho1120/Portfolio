using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���ݹ��
// �ݶ��̴� �ָԿ� �־���� ���ݽ� �Ѽ� ������ ���ֱ�
// ��Ÿ� ����, ���鿡 ���̸� ���� ���̿� �����ְ� ���̰� ��Ÿ� ���̸� �� �ֱ�


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
        if (Input.GetKeyDown(KeyCode.Q)) // ü�� �ȹٲ�
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
            Debug.Log("ũ�� ��");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("ũ�� �� ��");

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
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (playerStat.defense * 0.5f), 0f); // �ּ� ������ 0
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
        
    }

    public virtual void Dead()
    {
        isDead = true;
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); ���ľ���
        Debug.Log("����");
    }
}
