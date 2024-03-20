using UnityEngine.SceneManagement;

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
        NewGameManager.Instance.player.gameObject.SetActive(false);
        NewGameManager.Instance.DeactivateWating();
        NewUIManager.Instance.OnStartUI();
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(false);
        NewUIManager.Instance.MenuUI.gameObject.SetActive(false);
        DataManager.Instance.select.Init();
        LoadingSceneController.LoadScene("Home");

    }

    public void GoGame()    // 게임씬으로 이동
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.SaveData(); // 현재 정보를 저장함.
        }
        NewUIManager.Instance.OffStartUI();
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(true);
        NewGameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // 게임씬으로 이동
        NewGameManager.Instance.player.Init();
    }

}
