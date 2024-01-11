using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
// �� ���� �ȷ� ����

//���� ����
// �߷� ȿ�� ��ġ��
// �׶��� ���� ��ġ��
// �������� ȭ�� �ٹ̱�
// ����ġ 0���� �ϰ� �ƽ� �ø���
// �ɷ�ġ ������ ��ų ��ȭ ���� // �� ��� �ܿ��� � ��쿡�� ��ȭ �Ұ�
// ��� ���̴� ȭ�� ����� ��ȭ�ö��� ����

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
        ItemManager.Instance.Init();
        UiManager.Instance.Init();

    }
    private void Update()
    {
        UiManager.Instance.SetUI();
    }
}