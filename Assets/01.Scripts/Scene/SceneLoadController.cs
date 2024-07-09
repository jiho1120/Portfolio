using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoadController : Singleton<SceneLoadController>
{
    public void GoHomeScene()
    {
        if (GameManager.instance.stageStart)
        {
            UIManager.Instance.StartPopCor("�������� ���۽ÿ��� ó��ȭ������ �� �� �����ϴ�", 1.5f);
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

    public void GoGame()    // ���Ӿ����� �̵�
    {
        if (!DataManager.Instance.savefile[DataManager.Instance.nowSlot])    // ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            DataManager.Instance.gameData.SetGameData();
            DataManager.Instance.Save();
        }
        GameManager.Instance.InitWating();
        GameManager.Instance.player.Init();
        GameManager.Instance.boss.Init();
        LoadingSceneController.LoadScene("Game"); // ���Ӿ����� �̵�
        
    }


}
