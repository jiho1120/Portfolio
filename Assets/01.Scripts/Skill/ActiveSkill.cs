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
    Coroutine ColCor = null;

    public override void DoSkill(bool isPlayer)
    {
        if (this == null)
        {
            Debug.LogError("없어");
        }
        else
        {
            skillStat.SetInUse(true);
            if (skillStat.index == 1) // 슬래쉬
            {
                if (ColCor == null)
                {
                    //ColCor = StartCoroutine(OnOffCol());
                }
            }
            else if (skillStat.index == 2) // 원
            {
                KnockBackAttack(isPlayer);
            }
            else if (skillStat.index == 3) //땅
            {
                if (boxCor == null)
                {
                    boxCor = StartCoroutine(GrowInBoxCollider(isPlayer));
                }
            }
            else if (skillStat.index == 4 && isPlayer) // 중력
            {
                //if (gravityCor == null)
                //{
                //    gravityCor = StartCoroutine(GravityAttack());
                //}
                GravityAttack();
            }
            if (isPlayer)
            {
                GameManager.Instance.player.SetMp(GameManager.Instance.player.Mp - skillStat.mana);
                UiManager.Instance.SetUseSKillCoolImg(skillStat.index);
            }
            else
            {
                GameManager.Instance.boss.bossStat.SetMana(GameManager.Instance.boss.bossStat.mana - skillStat.mana);

            }
        }
        StartCoroutine(DieTimer());


    }

    IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(skillStat.duration);
        //if (ColCor != null)
        //{
        //    StopCoroutine(ColCor);
        //    ColCor = null;
        //}
        SetOffSkill();
        yield return new WaitForSeconds(skillStat.cool);
        skillStat.SetInUse(false);
    }
    IEnumerator OnOffCol()
    {
        SphereCollider[] sp = GetComponents<SphereCollider>();
        while (true)
        {
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < sp.Length; i++)
            {
                sp[i].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void KnockBackAttack(bool isPlayer)
    {
        if (isPlayer)
        {
            Player player = GameManager.Instance.player;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 8f, player.PlayerLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                IAttack attm = colliders[i].GetComponent<IAttack>();
                if (attm !=null)
                {
                    Vector3 direction = colliders[i].transform.position - transform.position;
                    Rigidbody enemyRigidbody = colliders[i].GetComponent<Rigidbody>();
                    if (colliders[i].GetComponent<Boss>() != null)
                    {
                        if (enemyRigidbody != null)
                        {
                            enemyRigidbody.AddForce(direction.normalized * 10, ForceMode.Impulse);
                            colliders[i].GetComponent<Boss>().SetStopAndMove();
                        }
                    }
                    else
                    {
                        if (enemyRigidbody != null)
                        {
                            Monster monster = colliders[i].GetComponent<Monster>();
                            if (!monster.isDead)
                            {
                                monster.SetStopAndMove();
                                enemyRigidbody.AddForce(direction.normalized * 10, ForceMode.Impulse);
                            }
                            else
                            {
                                return;
                            }
                            
                        }
                    }
                    attm.TakeDamage(player.Cri, player.Att + skillStat.effect); // 이걸 뒤에 둬야 밀림(앞에두면 죽음상태가 먼저 될경우 안밀리고 죽음)
                }                                
            }
        }
        else
        {
            Boss boss = GameManager.Instance.boss;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 8f, boss.bossLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                    GameManager.Instance.player.TakeDamage(boss.bossStat.criticalChance, boss.bossStat.attack + skillStat.effect);
                
                Vector3 direction = colliders[i].transform.position - transform.position;

                Rigidbody enemyRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.AddForce(direction.normalized * 10, ForceMode.Impulse);
                }
            }
        }
        
    }

    public IEnumerator GrowInBoxCollider(bool isPlayer)
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
                col.center = new Vector3(0, 0, colCenter * timecal); // 누적한걸 적용 , 여기다 누적해도됨
                col.size = new Vector3(1, 1, colSize * timecal);
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
        while (duTime < skillStat.duration)
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

    public override void DoReset() // DieTimer에서 코루틴 줘서 시간 텀 안줘도 괜찮음
    {
        skillStat.SetInUse(false);
    }


}
