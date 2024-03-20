using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : Singleton<NewUIManager>
{
    [Header("StartUI")]
    public GameObject StartUI;
    public Text newPlayerName;    // ���� �Էµ� �÷��̾��� �г���
    public GameObject create;    // �÷��̾� �г��� �Է�UI
    public Text[] slotText;        // ���Թ�ư �Ʒ��� �����ϴ� Text��

    [Header("MenuUI")]
    public GameObject MenuUI;


    [Header("Waiting")]
    public GameObject WaitingUI;
    public Text Stop;
    public Text Skip;
    public Text Count;

    [Header("InGame")]
    public GameObject InGameUI;
    public Text RunTime;



    public void OnStartUI()
    {
        StartUI.gameObject.SetActive(true);
    }

    public void SetStopTimer()
    {
        if (NewGameManager.Instance.isCountTime)
        {
            Stop.text = "Ÿ�̸� ����";
        }
        else
        {
            Stop.text = "Ÿ�̸� ����";
        }
    }
    public void SetWaitingUI()
    {
        SetStopTimer();
        Skip.text = "�ǳ� �ٱ�";
        Count.text = NewGameManager.Instance.countTime.ToString();
        if (NewGameManager.Instance.countTime <= 1)
        {
            Count.text = "���� ����";
        }
    }
    public void Create()    // �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
    {
        create.gameObject.SetActive(true);
    }
}
