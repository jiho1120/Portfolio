using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndPanel : MonoBehaviour
{
    public Text mainText;
    public Text CloseBtnText;
    public Text SaveBtnText;

    public void SetPanel()
    {
        if (GameManager.Instance.gameOver)
        {
            mainText.text = "GameOver";
            SaveBtnText.text = "ó�� ȭ������";
        }
        else if (GameManager.Instance.gameClear)
        {
            mainText.text = "GameClear";
            SaveBtnText.text = "���� �� �̾��ϱ�";
        }
        CloseBtnText.text = "���� ����";
    }
    
    public void SaveBtn()
    {
        GameManager.Instance.pools.SetActive(false);
        if (GameManager.Instance.gameOver)
        {
            Time.timeScale = 1f;
            ItemManager.Instance.ReturnAllObjectToPool();
            MonsterManager.Instance.StopSpawnMonster();
            MonsterManager.Instance.CleanMonster();//=>�� ����ִ� �ֵ鸸 ����. (�ܼ��� ����. 
            SceneLoadController.Instance.GoStartScene();
        }
        else if (GameManager.Instance.gameClear)
        {
            // ���� �� ���� �̾��ϱ�
            Time.timeScale = 1f;
            GameManager.Instance.SetGameClear(false);
            GameManager.Instance.GoWatingRoom();
        }
        GameManager.Instance.StopNum--;
        GameManager.Instance.pools.SetActive(true);
        gameObject.SetActive(false);
    }
    public void CloseBtn()
    {
        Application.Quit();
        Debug.Log("���� ����");
    }
}
