using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform characterBody;
    public Transform cameraArm;
    Animator anim;
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
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        SetAttackSpeed(attackSpeed);


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
            SetAttackSpeed(attackSpeed);
        }

    }


    private void Move()
    {
        finalSpeed = (run) ? runSpeed : speed;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float percent = ((run) ? 1 : 0.5f) * moveInput.magnitude;
        anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        characterBody.forward = lookForward;
        transform.position += moveDir * Time.deltaTime * 5f;
        anim.SetFloat("PosZ", moveInput.y);
        anim.SetFloat("PosX", moveInput.x);
    }
    
    void BasicAttack()
    {
        //Ŭ���Ҷ����� �����ð��� ���ؼ� ���Ӱ��ݻ��¸� ���� �ָ����� �����ϰ�
        //���Ӱ��ݳ��� �ð��� �ƴϸ� ù�ָ�����.

        //�����ϴ� ������ �ð��̸� �ǵ���������
        if (Time.time - lastClickTime < 0.9f)
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
                    LeftAttack();
                    isLeft = false;
                }
                else
                {
                    RightAttack();
                    isLeft = true;
                }
            }
            else
            {
                isLeft = false;
                RightAttack();
            }
            lastClickTime = Time.time;
        }
    }

    void LeftAttack()
    {
        anim.SetTrigger("isLeftPunch");

    }
    void RightAttack()
    {
        anim.SetTrigger("isRightPunch");

    }
    private void SetAttackSpeed(float _attackSpeed)
    {
        attackSpeed = _attackSpeed;

        anim.SetFloat("AttackSpeed", attackSpeed);
    }

    
}
