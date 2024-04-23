public class SceneLoadController : Singleton<SceneLoadController>
{
    public void GoHomeScene()
    {
        if (GameManager.instance.stageStart)
        {
            UIManager.Instance.StartPopCor("스테이지 시작시에는 처음화면으로 갈 수 없습니다", 1.5f);
        }
        else
        {
            GameManager.Instance.player.gameObject.SetActive(false);
            GameManager.Instance.DeactivateWating();
            UIManager.Instance.OnStartUI();
            UIManager.Instance.WaitingUI.gameObject.SetActive(false);
            UIManager.Instance.MenuUI.gameObject.SetActive(false);
            DataManager.Instance.select.Init();
            LoadingSceneController.LoadScene("Home");
        }
    }

    public void GoGame()    // 게임씬으로 이동
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.Save();

            //DataManager.Instance.SaveData(); // 현재 정보를 저장함.
        }
        UIManager.Instance.OffStartUI();
        UIManager.Instance.WaitingUI.gameObject.SetActive(true);
        GameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // 게임씬으로 이동
        GameManager.Instance.player.Init();
        
        UIManager.Instance.SetPlayerUI();
    }

}
