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
        if (GameManager.Instance.gameOver)
        {
            SceneLoadController.Instance.GoStartScene();
        }
        else if (GameManager.Instance.gameClear)
        {
            // ���� �� ���� �̾��ϱ�
            GameManager.Instance.SetGameClear(false);
        }
        gameObject.SetActive(false);
    }
    public void CloseBtn()
    {
        Application.Quit();
        Debug.Log("���� ����");
    }
}
