using UnityEngine;

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
            Cursor.lockState = CursorLockMode.Confined; // 여기서는 뭔짓을 하던 커서가 풀려야해서
            GameManager.Instance.SetCursorCount(0); // 그다음 무조건 초기화
        }
    }

    public void GoGame()    // 게임씬으로 이동
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.Save();
        }
        UIManager.Instance.OffStartUI();
        UIManager.Instance.WaitingUI.gameObject.SetActive(true);
        GameManager.Instance.InitWating();
        LoadingSceneController.LoadScene("Game"); // 게임씬으로 이동
        GameManager.Instance.player.Init();
        GameManager.Instance.boss.Init();

        UIManager.Instance.SetPlayerUI();
    }
}
