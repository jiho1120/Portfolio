using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public GameObject[] effects;
    public Transform[] effetTransform;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            animator.SetInteger("SkillNumber", 0);
            animator.SetTrigger("PlaySkill");
        }
        if (Input.GetKeyDown("1"))
        {
            animator.SetInteger("SkillNumber", 1);
            animator.SetTrigger("PlaySkill");
        }
        if (Input.GetKeyDown("2"))
        {
            animator.SetInteger("SkillNumber", 2);
            animator.SetTrigger("PlaySkill");
        }
    }

    public void UseEffect(int number) // 애니메이션 이벤트에 추가
    {
        Instantiate(effects[number], effetTransform[number].position, effetTransform[number].rotation);

    }
}
