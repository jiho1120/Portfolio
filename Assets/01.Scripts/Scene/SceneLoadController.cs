using UnityEngine.SceneManagement;

public class SceneLoadController : Singleton<SceneLoadController>
{
    public void GoHomeScene()
    {
        GameManager.Instance.player.gameObject.SetActive(false);
        GameManager.Instance.DeactivateWating();
        UIManager.Instance.OnStartUI();
        UIManager.Instance.WaitingUI.gameObject.SetActive(false);
        UIManager.Instance.MenuUI.gameObject.SetActive(false);
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
        UIManager.Instance.OffStartUI();
        UIManager.Instance.WaitingUI.gameObject.SetActive(true);
        GameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // ���Ӿ����� �̵�
        GameManager.Instance.player.Init();
    }

}
