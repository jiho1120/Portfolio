using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int Index;
    public SOSkill orgInfo;
    public SkillStat skillStat;
    public bool isPlayer; // 나중에는 enum으로 관리
        
    public void Init(SOSkill _Info)
    {
        //monsterLayer = 1 << LayerMask.NameToLayer("Enemy");
        SetInfo(orgInfo);
        skillStat = new SkillStat(orgInfo);
    }
    public void SetInfo(SOSkill _Info)
    {
        orgInfo = _Info;
    }
   

    private void OnTriggerStay(Collider other)
    {
        // 플레이어가 썻을때
        if (isPlayer)
        {
            if (other.CompareTag("Monster"))
            {
                Monster monster = other.GetComponent<Monster>();
                monster.TakeDamage(GameManager.Instance.player.Cri, GameManager.Instance.player.Att * skillStat.effect);
            }

            if (other.CompareTag("Boss"))
            {
                other.GetComponent<Boss>().TakeDamage(GameManager.Instance.player.Cri, GameManager.Instance.player.Att * skillStat.effect);
            }
        }
        else
        {
            // 보스가 썻을때
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().TakeDamage(GameManager.Instance.boss.bossStat.criticalChance, GameManager.Instance.boss.bossStat.attack * skillStat.effect);
            }
        }
    }
    

    public virtual void DoSkill(bool isPlayer) /* 스킬로써해야할일들*/
    {

    }

    public abstract void DoReset();
}
