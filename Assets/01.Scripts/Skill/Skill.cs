using UnityEngine;
using static AllEnum;

public class Skill : MonoBehaviour
{
    public SkillName skillName { get; private set; }

    // ��ų�� ����ϴ� ��ü (ĳ������ ��������) �ν����Ϳ��� ����
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
