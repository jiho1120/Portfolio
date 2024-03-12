using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadController : Singleton<SceneLoadController>
{
    public void GoStartScene()
    {
        GameManager.Instance.AllScriptsCorReset();
        LoadingSceneController.LoadScene("Start");
        GameManager.Instance.StartMenu();
    }
    public void GoMainScene()
    {
        LoadingSceneController.LoadScene("Main");
        GameManager.Instance.LoadMain();
    }
    public void GoHomeScene()
    {
        NewUIManager.Instance.StartUI.gameObject.SetActive(true);
        NewUIManager.Instance.MenuUI.SetActive(false);
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(false);
        LoadingSceneController.LoadScene("Home");

    }


    public void GoGameScene()
    {
        NewUIManager.Instance.StartUI.gameObject.SetActive(false);
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(true);
        NewGameManager.Instance.Wating();
        LoadingSceneController.LoadScene("Game");
    }

}
