using UnityEngine;
using static AllEnum;

public abstract class Skill : MonoBehaviour,Initialize
{
    //protected LayerMask enemyLayer;

    public SkillData skilldata;
    [SerializeField] protected NewSkillType skillType;// ��ų Ÿ��
    [SerializeField] protected SkillName skillName; // ��ų �̸�
    [SerializeField] protected bool setParent; // ��ų�� �÷��̾ ����ٴ��� // �׷���Ƽ�� �׶��常 flase
    [SerializeField] protected bool inUse =false; //false�Ͻ� ��ų����
    public bool InUse {  get { return inUse; } set {  inUse = value; } }


    public void Init(SkillData _skillData)
    {
        skilldata = _skillData;
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

    public virtual void Activate()
    {
        this.gameObject.SetActive(true);
        inUse = true;

    }

    public virtual void Deactivate()
    {
        this.gameObject.SetActive(false);
        inUse = false;

    }
    public Skill CheckUsableSkill(Creature caster)
    {
        if (skillName == SkillName.Gravity)
        {
            if (caster.Stat.ultimateGauge < caster.Stat.maxUltimateGauge)
            {
                return null;
            }
        }
        return !inUse && caster.Stat.mp >= skilldata.mana ? this : null;
    }

    //������ ��ų ���� x ex) �����
    protected abstract void OnTriggerEnter(Collider other);


    //������ ��ų ���� ex) �˹�
    protected abstract void OnCollisionEnter(Collision collision);
}
