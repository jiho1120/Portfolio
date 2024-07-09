using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static AllEnum;

public class Skill : MonoBehaviour,Initialize
{
    protected int enemyLayer;


    public SkillData skilldata;
    [SerializeField] protected NewSkillType skillType;// ��ų Ÿ��
    public SkillName skillName { get; private set; } // ��ų �̸�

    public virtual void SetEnemyLayer(int enemyLayer)
    {
        this.enemyLayer = enemyLayer;
    }

    // ��ų �������� �̰ɷ� 
    public void SetSkillData(SkillData _skillData)
    {
        skilldata = _skillData;
    }
    public SkillData GetSkillData()
    {
        return skilldata;
    }

    // ��ų ���ӽð���ŭ �����ϴٰ� ����
    public virtual void Activate()
    {
        gameObject.SetActive(true);
        ImplementEffects();

    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
    

    /// <summary>
    /// active�Ҷ� �ѹ� �����Ű�� �Լ� �߰����� ����
    /// </summary>
    protected virtual void ImplementEffects()
    {
    } 
    
    public virtual void SetSKillPos()
    {
        Vector3 pos = GameManager.Instance.player.transform.position;
        Quaternion rot = GameManager.Instance.player.transform.rotation;
        if (skillName == SkillName.Gravity)
        {
            Vector3 spawnOffset = transform.rotation * new Vector3(0, 0.5f, 1) * 10f;
            rot = Quaternion.Euler(transform.rotation.eulerAngles);
            pos += spawnOffset;
        }
    }
    //������ ��ų ���� x ex) �����
    protected virtual void OnTriggerEnter(Collider other)
    {

    }


    //������ ��ų ���� ex) �˹�
    protected virtual void OnCollisionEnter(Collision collision)
    {

    }
}
