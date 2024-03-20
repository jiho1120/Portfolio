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

    public void GoGame()    // ���Ӿ����� �̵�
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.SaveData(); // ���� ������ ������.
        }
        NewUIManager.Instance.OffStartUI();
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(true);
        NewGameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // ���Ӿ����� �̵�
        NewGameManager.Instance.player.Init();
    }

}
