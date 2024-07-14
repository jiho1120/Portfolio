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
        
        GetComponent<BehaviorTree>().SetInit();
    }
    public override void Activate()
    {
        base.Activate();
        MonsterManager.Instance.SetEnemyPos(this);
        availableCor = null;
        isAvailableSkill = true;
        LevelUp();
        Stat.SetStat(DataManager.Instance.gameData.bossData.bossStat);
        UIManager.Instance.uIBoss.SetBossUI();

        UIManager.Instance.uIBoss.SetHPUI();
        UIManager.Instance.uIBoss.SetMPUI();

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
    private void OnEnable()
    {
        if (UIManager.Instance != null && UIManager.Instance.uIBoss != null)
        {
            UIManager.Instance.uIBoss.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (UIManager.Instance != null && UIManager.Instance.uIBoss != null)
        {
            UIManager.Instance.uIBoss.gameObject.SetActive(false);
        }
    }

    public override void SetHp(float hp)
    {
        base.SetHp(hp);
        UIManager.Instance.uIBoss.SetHPUI();
        if (hp <= 0)
        {
            Die();
        }
    }
    public override void SetMp(float value)
    {
        base.SetMp(value);
        UIManager.Instance.uIBoss.SetMPUI();
    }
    #region ���� ����
    public override void LevelUp()
    {
        base.LevelUp();

    }
    public override void StatUp()
    {
        DataManager.Instance.gameData.bossData.bossStat.StatUp(1, 1000, 1000, 30, 10, 0.5f, 0.2f, 200, 300, 200, 200, 0, 0, 0, 0);
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
