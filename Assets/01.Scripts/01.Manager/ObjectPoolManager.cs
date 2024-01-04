using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public ObjectPool<Monster> monsterPool { get; private set; }
    int monsterRange = 20;

    public List<Skill> skillPool { get; private set; }

    void Start()
    {
        // ObjectPool �ν��Ͻ� ����
        
    }
    public void Init()
    {
        monsterPool = new ObjectPool<Monster>();
        skillPool = new List<Skill>();
        MakeMonster();
        MakeSkill();



    }
    void MakeMonster()
    {
        // ���� Ǯ �ʱ�ȭ
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
            // �� ���� ������ ���͸� �������� �����Ͽ� ��ȯ
            Monster monstersc = monsterPool.GetObjectFromPool(ResourceManager.Instance.monsterAll);
            monstersc.Init();
            yield return new WaitForSeconds(1f);
        }
    }
    void MakeSkill()
    {
        for (int i = 0; i < SkillManager.Instance.perfectSkillDict.Count; i++)
        {
            Skill skill = Instantiate(SkillManager.Instance.perfectSkillDict[(AllEnum.SkillName)i]);
            
            skillPool.Add(skill);
            skill.gameObject.SetActive(false);
        }
    }
    //void MakeSkill()
    //{
    //    for (int i = 0; i < SkillManager.Instance.perfectSkillDict.Count; i++)
    //    {
    //        Skill skill = Instantiate(SkillManager.Instance.perfectSkillDict[(AllEnum.SkillName)i]);
    //        skillPool.objectPool.Enqueue(skill);
    //        skillPool.InfoList.Add(skill);
    //        skill.gameObject.SetActive(false);
    //    }
    //}
}
