using UnityEngine;
using static AllEnum;

public abstract class Skill : MonoBehaviour,Initialize
{
    //protected LayerMask enemyLayer;

    public SkillData skilldata;
    [SerializeField] protected NewSkillType skillType;// 스킬 타입
    [SerializeField] protected SkillName skillName; // 스킬 이름
    [SerializeField] protected bool setParent; // 스킬이 플레이어를 따라다닐지 // 그래비티랑 그라운드만 flase
    [SerializeField] protected bool inUse =false; //false일시 스킬나감
    public bool InUse {  get { return inUse; } set {  inUse = value; } }


    public void Init(SkillData _skillData)
    {
        skilldata = _skillData;
    }

    // 스킬 레벨업시 이걸로 
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

    //물리적 스킬 구현 x ex) 디버프
    protected abstract void OnTriggerEnter(Collider other);


    //물리적 스킬 구현 ex) 넉백
    protected abstract void OnCollisionEnter(Collision collision);
}
