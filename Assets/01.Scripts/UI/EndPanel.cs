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
            SaveBtnText.text = "처음 화면으로";
        }
        else if (GameManager.Instance.gameClear)
        {
            mainText.text = "GameClear";
            SaveBtnText.text = "저장 후 이어하기";
        }
        CloseBtnText.text = "게임 종료";
    }
    
    public void SaveBtn()
    {
        GameManager.Instance.pools.SetActive(false);
        if (GameManager.Instance.gameOver)
        {
            Time.timeScale = 1f;
            ItemManager.Instance.ReturnAllObjectToPool();
            MonsterManager.Instance.StopSpawnMonster();
            MonsterManager.Instance.CleanMonster();//=>딱 살아있던 애들만 죽임. (단순히 죽임. 
            SceneLoadController.Instance.GoStartScene();
        }
        else if (GameManager.Instance.gameClear)
        {
            // 저장 후 게임 이어하기
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
        Debug.Log("게임 종료");
    }
}
