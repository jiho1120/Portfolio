using UnityEngine;
using static AllEnum;

public class Skill : MonoBehaviour
{
    public SkillName skillName { get; private set; }

    // 스킬을 사용하는 객체 (캐릭인지 보스인지) 인스펙터에서 설정
    public ObjectType objectType;



    public void Init()
    {

    }



    public void GetDamage()
    {
    }

    public virtual void ActiveSkill()
    {
        this.gameObject.SetActive(false);

    }
    public virtual void DeActiveSkill()
    {
        this.gameObject.SetActive(false);
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (objectType == ObjectType.Player)
        {
            
        }
        else if (objectType == ObjectType.Boss)
        {

        }
    }
    protected void OnCollisionEnter(Collision collision)
    {
        if (objectType == ObjectType.Player)
        {

        }
        else if (objectType == ObjectType.Boss)
        {

        }
    }
}
