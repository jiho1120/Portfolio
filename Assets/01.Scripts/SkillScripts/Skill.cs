using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int Index;
    public SOSkill orgInfo;

    public void SetInfo(SOSkill _Info)
    {
        orgInfo = _Info;
    }

    private void OnCollisionEnter(Collision collision) // 1번,3번
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Player plyer = GameManager.Instance.player.GetComponent<Player>();
            
        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Player plyer = GameManager.Instance.player.GetComponent<Player>();
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
        }
    }
    

    public virtual void DoSkill() /* 스킬로써해야할일들*/
    {

    }

    public abstract void DoReset();
}
