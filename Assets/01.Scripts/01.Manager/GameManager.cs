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
            gameTime += Time.deltaTime;
            startGame();
        }
    }
    public void startGame()
    {
        gameRound = (countGame / maxStage) + 1;
        gameStage = (countGame % maxStage) +1;
        UiManager.instance.wating.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked; // ���Ŵ������� ���ö� �ѹ�
        //Cursor.visible = false;
        if (gameStage == 5)
        {
            MonsterManager.Instance.SpawnMonster();
        }
        // ������ countGame ���� �ø���
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
}