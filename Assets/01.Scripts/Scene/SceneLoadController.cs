using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
            if (GameManager.Instance.player != null)
            {
                GameManager.Instance.player.Deactivate();
            }
            if (GameManager.Instance.boss != null)
            {
                GameManager.Instance.boss.Deactivate();
            }
            MonsterManager.Instance.ClearObjectPool();
            //ItemManager.Instance
            DataManager.Instance.Init();
            GameManager.Instance.InitHome();
            LoadingSceneController.LoadScene("Home");
        }
    }

    public void GoGame()    // 게임씬으로 이동
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.Save();
        }
        GameManager.Instance.InitWating();
        GameManager.Instance.player.Init();
        GameManager.Instance.boss.Init();
        LoadingSceneController.LoadScene("Game"); // 게임씬으로 이동
        
    }


}
