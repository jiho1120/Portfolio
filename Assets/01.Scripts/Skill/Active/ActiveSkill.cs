using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static AllEnum;

public class ActiveSkill : Skill
{
    [SerializeField] protected bool setParent; // 스킬이 플레이어를 따라다닐지 // 그래비티랑 그라운드만 flase
    [SerializeField] protected bool isAvailable = true; //true일시 스킬나감
    public bool IsAvailable { get { return isAvailable; } set { isAvailable = value; } }
    protected Coroutine actSkillCor = null;


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
    private void OnDisable()
    {
        // 간혹 스킬이 안돌아갈때가 있음
        actSkillCor = null;
    }

    IEnumerator SetSkillOffTime()
    {
        isAvailable = false; // 사용했으니 false
        yield return new WaitForSeconds(skilldata.duration);
        actSkillCor = null;
        Deactivate();
    }
    
    public virtual bool CheckUsableSkill(Creature caster)
    {
        return isAvailable && caster.Stat.mp >= skilldata.mana ? true : false;
    }
}
