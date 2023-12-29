using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    SOSkill soSkill;

    public void GetActiveInfo()
    {
        switch (soSkill.skillType)
        {
            case AllEnum.SkillType.Damage:
                break;
            case AllEnum.SkillType.Heal:
                PassiveHeal();
                break;
            case AllEnum.SkillType.Buff:
                break;
            case AllEnum.SkillType.DeBuff:
                break;
            case AllEnum.SkillType.End:
                break;
            default:
                break;
        }
    }

    public void PassiveHeal()
    {

    }






}
