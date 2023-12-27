using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Camera cam;
    Rigidbody rb;

    public float speed = 5;
    public float runSpeed = 8f;
    public float finalSpeed;

    public bool toggleCameraRotation;
    public bool run;

    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true; // 둘러보기 활성화
        }
        else
        {
            toggleCameraRotation = false; // 둘러보기 비활성화
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }
    }
    void FixedUpdate()
    {
        InputMovement();
    }


    private void LateUpdate()
    {
        if (!toggleCameraRotation)
        {
            Vector3 playerRotate = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1));
            Quaternion targetRotation = Quaternion.LookRotation(playerRotate);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
        }
    }

    void InputMovement()
    {

        finalSpeed = (run) ? runSpeed : speed;
        Vector3 forward = transform.TransformDirection(Vector3.forward); // 캐릭터가 보는 전방의 벡터를 가져옴
        Vector3 right = transform.TransformDirection(Vector3.right);
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        rb.velocity = Vector3.Lerp(rb.velocity, moveDirection.normalized * finalSpeed, Time.deltaTime * smoothness);        

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
    //    anim.SetFloat("Blend", percent, 0.1f, Time.deltaTime);

        //if (Input.GetAxisRaw("Vertical") < 0)
        //{
        //    anim.SetFloat("PosZ", -1);
        //}
        //else if (Input.GetAxisRaw("Vertical") > 0)
        //{
        //    anim.SetFloat("PosZ", 1);
        //}

        //if (Input.GetAxisRaw("Horizontal") < 0)
        //{
        //    anim.SetFloat("PosX", -1);
        //}
        //else if (Input.GetAxisRaw("Horizontal") > 0)
        //{
        //    anim.SetFloat("PosX", 1);
        //}

        
    }
}

