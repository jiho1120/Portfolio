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
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ResourceManager.Instance.LoadResources();        
        SkillManager.Instance.Init();
        MonsterManager.Instance.Init();
        MonsterManager.Instance.SpawnMonster();
        InventoryManager.Instance.Init();
        ItemManager.Instance.Init();
        UiManager.Instance.Init();
        //ResourceManager.Instance.XMLAccess.ShowListInfo();
    }
    private void Update()
    {
        UiManager.Instance.SetUI();
    }
}