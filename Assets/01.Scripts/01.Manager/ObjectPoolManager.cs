using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public ObjectPool<Monster> monsterPool { get; private set; }
    int monsterRange = 20;

    //Dictionary<AllEnum.SkillName, Skill> skillDict = new Dictionary<AllEnum.SkillName, Skill>();

    void Start()
    {
        // ObjectPool 인스턴스 생성
        
    }
    public void Init()
    {
        monsterPool = new ObjectPool<Monster>();
        MakeMonster();
    }
    //public Skill GetSkill(AllEnum.SkillName skill)
    //{
    //    return skillDict[skill];
    //}
    void MakeMonster()
    {
        // 몬스터 풀 초기화
        for (int i = 0; i < monsterRange; i++)
        {
            monsterPool.RandomInitializeObjectPool(ResourceManager.Instance.monsterAll);
        }
    }

    public void SpawnMonster()
    {
        StartCoroutine(GetMonster());
    }

    public IEnumerator GetMonster()
    {
        while (true)
        {
            // 두 가지 종류의 몬스터를 랜덤으로 선택하여 소환
            Monster monstersc = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
    //void MakeSkill()
    //{
    //    for (int i = 0; i < SkillManager.Instance.perfectSkillDict.Count; i++)
    //    {
    //        Skill skill = Instantiate(SkillManager.Instance.perfectSkillDict[(AllEnum.SkillName)i]);
    //        skillDict.Add((AllEnum.SkillName)i,skill);
    //        skill.gameObject.SetActive(false);
    //    }
    //}
   
}
