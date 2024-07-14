using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    public void Active()
    {
        gameObject.SetActive(true);
        GameManager.Instance.VisibleCursor();
    }
    public void Continue()
    {
        gameObject.SetActive(false);
        GameManager.Instance.boss.gameObject.SetActive(false);
        GameManager.Instance.LockedCursor();

    }
    public void Restart()
    {
        gameObject.SetActive(false);
        SceneLoadController.Instance.GoHomeScene();
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
