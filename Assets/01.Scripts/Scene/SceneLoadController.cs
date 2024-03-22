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

    public void GoGame()    // 게임씬으로 이동
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.SaveData(); // 현재 정보를 저장함.
        }
        UIManager.Instance.OffStartUI();
        UIManager.Instance.WaitingUI.gameObject.SetActive(true);
        GameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // 게임씬으로 이동
        GameManager.Instance.player.Init();
    }

}
