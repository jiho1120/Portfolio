using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ActiveSkill : Skill
{
    private float colCenter = 7f;
    private float colSize = 12f;
    private float duration = 1f;
    float skillSpeed = 3;
    Coroutine boxCor = null;



    public override void DoSkill()
    {
        if (this == null)
        {
            Debug.LogError("����");
        }
        else
        {
            skillStat.SetInUse(true);
            if (skillStat.index == 1) // ������
            {
            }
            else if (skillStat.index == 2) // ��
            {
                KnockBackAttack();
            }
            else if (skillStat.index == 3) //��
            {
                if (boxCor == null)
                {
                    boxCor = StartCoroutine(GrowInBoxCollider());
                }
            }
            else if (skillStat.index == 4) // �߷�
            {
                //if (gravityCor == null)
                //{
                //    gravityCor = StartCoroutine(GravityAttack());
                //}
                GravityAttack();
            }
            GameManager.Instance.player.SetMp(GameManager.Instance.player.Mp - skillStat.mana);
            UiManager.Instance.SetUseSKillCoolImg(skillStat.index);
        }
        StartCoroutine(DieTimer());
       
    }

    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(skillStat.duration);
        SkillManager.Instance.SetOffSkill(this);
        yield return new WaitForSeconds(skillStat.cool);
        skillStat.SetInUse(false);
    }

    public void KnockBackAttack()
    {
        Player plyer = GameManager.Instance.player.GetComponent<Player>();
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 8f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Monster"))
            {
                Monster monster = colliders[i].GetComponent<Monster>();
                monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.skillStat.effect);

                Vector3 direction = colliders[i].transform.position - this.transform.position;

                Rigidbody enemyRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.AddForce(direction.normalized * 10, ForceMode.Impulse);
                }
            }
        }
    }

    public IEnumerator GrowInBoxCollider()
    {
        BoxCollider col = transform.GetComponent<BoxCollider>();
        if (col != null)
        {
            float elapsedTime = 0;
            float timecal = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime * skillSpeed;
                timecal = elapsedTime / duration;
                col.center = new Vector3(0, 0, colCenter * timecal); // �����Ѱ� ���� , ����� �����ص���
                col.size = new Vector3(1, 1, colSize * timecal);
                //Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
                yield return null; // �����Ӵ� �þ
            }
        }
        boxCor = null; // �˾Ƽ� ����
    }
    //public void GravityAttack()
    //{
    //    Player plyer = GameManager.Instance.player.GetComponent<Player>();
    //    List<Monster> monsterList = new List<Monster>(); 
    //    Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5f);
    //    // �ݶ��̴��� ������ �� �Ÿ��� ���Ÿ� ���ؼ� �ݴ�� �о��
    //    for (int i = 0; i < colliders.Length; i++)
    //    {
    //        if (colliders[i].CompareTag("Monster"))
    //        {
    //            Monster monster = colliders[i].GetComponent<Monster>();
    //            monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
    //            monsterList.Add(monster);
    //            monster.Agent.baseOffset = 2f;
    //            Vector3 velo = Vector3.zero;
    //            monster.transform.position = Vector3.SmoothDamp(transform.position, this.transform.position, ref velo, 10f);
    //        }
    //    }
    //    for (int i = 0; i < monsterList.Count; i++)
    //    {
    //        monsterList[i].Agent.baseOffset = 0f;
    //    }
    //}
    public void GravityAttack()
    {
        Player plyer = GameManager.Instance.player.GetComponent<Player>();
        List<Monster> monsterList = new List<Monster>();
        float duTime = 0;
        while(duTime < skillStat.duration)
        {
            duTime += Time.deltaTime;
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Monster"))
                {
                    Monster monster = colliders[i].GetComponent<Monster>();
                    monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.skillStat.effect);
                    if (!monsterList.Contains(monster))
                    {
                        monsterList.Add(monster);
                    }
                    monster.Agent.baseOffset = 2f;
                    Vector3 velo = Vector3.zero;
                    monster.transform.position = Vector3.SmoothDamp(transform.position, this.transform.position, ref velo, 10f);
                }
            }
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].Agent.baseOffset = 0f;
        }
    }

    public override void DoReset() // DieTimer���� �ڷ�ƾ �༭ �ð� �� ���൵ ������
    {
        skillStat.SetInUse(false);
    }


}
