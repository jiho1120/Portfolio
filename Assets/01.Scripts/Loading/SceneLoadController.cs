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
        NewUIManager.Instance.StartUI.gameObject.SetActive(true);
        NewUIManager.Instance.MenuUI.SetActive(false);
        NewUIManager.Instance.WaitingUI.gameObject.SetActive(false);
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
        NewGameManager.Instance.Wating();
        LoadingSceneController.LoadScene("Game"); // ���Ӿ����� �̵�
    }

}
