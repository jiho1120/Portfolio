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
            collision.gameObject.GetComponent<Monster>().TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
            collision.gameObject.GetComponent<Monster>().isHit = true;
        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Player plyer = GameManager.Instance.player.GetComponent<Player>();
            other.GetComponent<Monster>().TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
            other.GetComponent<Monster>().isHit = true;
        }
    }
    

    public virtual void DoSkill() /* 스킬로써해야할일들*/
    {

    }

    public abstract void DoReset();
}
