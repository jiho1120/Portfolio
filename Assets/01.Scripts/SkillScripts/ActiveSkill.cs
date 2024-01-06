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
            Debug.LogError("없어");
        }
        else
        {
            orgInfo.inUse = true;
            if (orgInfo.index == 1) // 원
            {
                KnockBackAttack();
            }
            else if (orgInfo.index == 2) // 슬래쉬
            {

            }
            else if (orgInfo.index == 3) //땅
            {

                if (boxCor == null)
                {
                    boxCor = StartCoroutine(GrowInBoxCollider());
                }

            }
            else if (orgInfo.index == 4) // 중력
            {
                //if (gravityCor == null)
                //{
                //    gravityCor = StartCoroutine(GravityAttack());
                //}
                GravityAttack();
            }
        }
        StartCoroutine(DieTimer());
       
    }

    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(orgInfo.duration);
        SkillManager.Instance.SetOffSkill(this);
        yield return new WaitForSeconds(orgInfo.cool);
        orgInfo.inUse = false;
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
                monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);

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
        Debug.Log("함수들어옴");
        BoxCollider col = transform.GetComponent<BoxCollider>();
        if (col != null)
        {
            float elapsedTime = 0;
            float timecal = 0;
            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime * skillSpeed;
                timecal = elapsedTime / duration;
                col.center = new Vector3(0, 0, colCenter * timecal); // 누적한걸 적용 , 여기다 누적해도됨
                col.size = new Vector3(1, 1, colSize * timecal);
                //Debug.Log("Counter1: " + col.center + " | Counter2: " + col.size);
                yield return null; // 프레임당 늘어남
            }
        }
        boxCor = null; // 알아서 끝남
    }
    //public void GravityAttack()
    //{
    //    Player plyer = GameManager.Instance.player.GetComponent<Player>();
    //    List<Monster> monsterList = new List<Monster>(); 
    //    Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5f);
    //    // 콜라이더에 닿으면 내 거리랑 상대거리 구해서 반대로 밀어내기
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
        while(duTime < orgInfo.duration)
        {
            duTime += Time.deltaTime;
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5f);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Monster"))
                {
                    Monster monster = colliders[i].GetComponent<Monster>();
                    monster.TakeDamage(plyer.playerStat.criticalChance, plyer.playerStat.attack * this.orgInfo.effect);
                    if (monsterList.Contains(monster))
                    {
                        Debug.Log("이미 있는애");
                    }
                    else
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

    public override void DoReset() // DieTimer에서 코루틴 줘서 시간 텀 안줘도 괜찮음
    {
        orgInfo.inUse = false;
    }


}
