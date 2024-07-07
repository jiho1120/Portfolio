using System.Collections;
using UnityEngine;
using static AllEnum;

public class ActiveSkill : Skill
{
    [SerializeField] protected bool setParent; // ��ų�� �÷��̾ ����ٴ��� // �׷���Ƽ�� �׶��常 flase
    [SerializeField] protected bool isAvailable = true; //true�Ͻ� ��ų����
    public bool IsAvailable { get { return isAvailable; } set { isAvailable = value; } }
    protected Coroutine actSkillCor = null;
    protected Coroutine actSkillCoolCor = null;


    public override void Activate()
    {
        base.Activate();
        if (actSkillCor == null)
        {
            actSkillCor = StartCoroutine(SetSkillOffTime());
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

    }
    IEnumerator SetSkillOffTime()
    {
        isAvailable = false; // ��������� false
        yield return new WaitForSeconds(skilldata.duration);
        actSkillCor = null;
        Deactivate();
    }
    
    public virtual bool CheckUsableSkill(Creature caster)
    {
        return isAvailable && caster.Stat.mp >= skilldata.mana ? true : false;
    }
}
