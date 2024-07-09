using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static AllEnum;

public class Boss : HumanCharacter
{
    [Header("NowState�� �ν����� â���� �ǵ��� ������")]
    public StateEnum NowState = StateEnum.End;//�������
    public NavMeshAgent agent { get; private set; }

    public bool isAvailableSkill { get; private set; } = true;
    
    public float actualDistance { get; private set; }
    public float ActualDistance { get => actualDistance; set => actualDistance = value; }
    float skillWaitTime = 3;

    Coroutine availableCor = null;



    public override void Init()
    {
        base.Init();
        if (agent == null)
        {
            agent=GetComponent<NavMeshAgent>();
        }
        Stat = new StatData(DataManager.Instance.gameData.bossData.bossStat);
        
        SkillManager.Instance.UpdateSkillData(this);
        GetComponent<BehaviorTree>().SetInit();
    }
    public override void Activate()
    {
        base.Activate();
        MonsterManager.Instance.SetEnemyPos(this);
        Stat.SetStat(DataManager.Instance.gameData.bossData.bossStat);
        LevelUp(); // �Ϻη� ������ �����ų���� �ڿ��� ������ �ٽ� ��Ƽ��ɶ� �����
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (availableCor != null)
        {
            StopCoroutine(availableCor);
            availableCor = null;
        }
    }
    #region ���� ����
    public override void LevelUp()
    {
        base.LevelUp();

    }
    public override void StatUp()
    {
        DataManager.Instance.gameData.bossData.bossStat.StatUp(1, 500, 500, 10, 10, 0.5f, 0.2f, 200, 300, 200, 200, 0, 0, 0, 0);
    }
    #endregion
    public void LookPlayer()
    {
        Vector3 direction = GameManager.Instance.player.transform.position - transform.position;
        direction.y = 0; // ���� �������θ� ȸ���ϵ��� Y���� 0���� ����
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
           
    }
    public override void Die()
    {
        agent.isStopped = true;
        GameManager.Instance.player.AddMoney(Stat.money); // ������ ���� ���� ��û ��
        GameManager.Instance.player.AddExp(Stat.experience);
        //UIManager.Instance.note.SetActive(false); ���� �����ϸ� Ű��
        StopAllCoroutines();
        SkillManager.Instance.DeactivateAllSkills(this);
        GameManager.Instance.SetKillMon(DataManager.Instance.gameData.killGoal); // �ٷ� Ŭ����
        UIManager.Instance.ActiveBossEndPanel();// ������ �г�
    }

    public override void ImplementTakeDamage()
    {
    }
    public override void GetAttToData()
    {
        Stat.attack = DataManager.Instance.gameData.bossData.bossStat.attack;
    }

    public void StartIsAvailableSkillCor()
    {
        if (availableCor == null)
        {
            availableCor = StartCoroutine(IsAvailableSkillCor());
        }
    }

    IEnumerator IsAvailableSkillCor()
    {
        isAvailableSkill = false;
        yield return new WaitForSeconds(skillWaitTime);
        isAvailableSkill = true;
        availableCor = null;
    }
}
