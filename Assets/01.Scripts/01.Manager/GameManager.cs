using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���� ����
// �߷� ȿ�� ��ġ��
// �׶��� ���� ��ġ��
// �ɷ�ġ ������ ��ų ��ȭ ���� // �� ��� �ܿ��� � ��쿡�� ��ȭ �Ұ�
// ������ �Լ� �ϼ��ϱ�
// ������ ȭ�� ��ư ��ɵ� �����
// ������ ������ ����� �ֱ�

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public bool gameStart = false;
    public bool runTime = true;
    public int monsterGoal = 0;
    public int killMonster = 0;
    private int countGame = 0; //�̰� ������ ��� �������� gameRound, gameStage
    private int maxStage = 6; // 1����� 5����������
    public int gameRound=0; //gameRound - gameStage ����
    public int gameStage=0;
    public bool cursorLock = false;

    public float gameTime { get; private set; }
    public float countTime { get; private set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();
        player.FirstStart();
        //player.CalcPlayerStat();
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        InventoryManager.Instance.Init();
        ItemManager.Instance.Init();
        player.Init();
        UiManager.Instance.Init();
        //ResourceManager.Instance.XMLAccess.ShowListInfo();
        countTime = 5f;
        UiManager.instance.wating.SetActive(true);
        InvokeRepeating("CallPassiveSkill", 5.0f, 10.0f);
    }
    private void Update()
    {
        UiManager.Instance.SetUI();
        if (countTime > 0 && runTime)
        {
            countTime -= Time.deltaTime;
        }
        if (countTime <= 0)
        {
            gameStart = true;
        }
        if (gameStart)
        {
            LockCursor();
            runTime = false;
            gameTime += Time.deltaTime;
            startGame();
        }
    }
    public void startGame()
    {
        gameRound = (countGame / maxStage) + 1;
        gameStage = (countGame % maxStage) +1;
        monsterGoal = gameRound * 10;
        UiManager.instance.wating.SetActive(false);
        
        if (gameStage != 5)
        {
            if (gameRound != 1 && gameStage != 1)
            {
                MonsterManager.Instance.monstersLevelUp();
            }
            MonsterManager.Instance.SpawnMonster();
        }
        else
        {
            // ���� ����
        }
    }
    public void LockCursor()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }    
    }

    public void StopOnOffTime()
    {
        runTime = !runTime;
    }
    public void SkipTime()
    {
        countTime = 0;
        gameStart = true;
        gameTime = 0;
    }
    public void CallPassiveSkill()
    {
        if (SkillManager.Instance.passiveSkill != null)
        {
            SkillManager.Instance.passiveSkill.DoReset();

        }
        SkillManager.Instance.CallPassiveSkill();
    }
}