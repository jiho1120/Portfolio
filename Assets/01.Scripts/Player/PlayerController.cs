using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����
// �¾����� ü�� �ȹٲ�

//���ݹ��
// �ݶ��̴� �ָԿ� �־���� ���ݽ� �Ѽ� ������ ���ֱ�
// ��Ÿ� ����, ���鿡 ���̸� ���� ���̿� �����ְ� ���̰� ��Ÿ� ���̸� �� �ֱ�


public class PlayerController : MonoBehaviour
{
    Player player;
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
    public float attackCooldown = 1.5f;
    bool isLeft = false;

    
    void Start()
    {
        playerStat = new PlayerStat();
        playerAnimator =GetComponent<PlayerAnimator>();
        player = GetComponent<Player>();
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
            player.TakeDamage(playerStat.criticalChance, playerStat.attack, playerStat.health, playerStat.defense);
            Debug.Log(playerStat.health);
        }

        if (playerStat.health <= 0)
        {
            player.Dead();
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
        // 1�ʵ����� �ٵ� ���ǵ尡 ������
        float animTime = 0f;
        animTime = 1f / attackSpeed; // �ٲ� �ִϸ��̼� �ð� = �ִϸ��̼� �ð�(1��) / �ִϸ��̼� ���ǵ�

        //�����ϴ� ������ �ð��̸� �ǵ���������
        if (Time.time - lastClickTime <= animTime) // �ִϸ��̼� ���ǵ尡 �ö󰡼� �ִϸ��̼ǵ� ���� ����
        {
            return;
        }
        else //�װ� �ƴ϶��
        {
            if (Time.time - lastClickTime <= attackCooldown) //���Ӱ���
                                                             //���� ����� �����ϱ�
            {
                if (isLeft)
                {
                    playerAnimator.LeftAttack();
                    isLeft = false;
                }
                else
                {
                    playerAnimator.RightAttack();
                    isLeft = true;
                }
            }
            else
            {
                isLeft = false;
                playerAnimator.RightAttack();
            }
            lastClickTime = Time.time;
        }
    }
}
