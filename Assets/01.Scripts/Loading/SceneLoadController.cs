using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadController : Singleton<SceneLoadController>
{
    



    public void GoMainScene()
    {
        GameManager.Instance. LoadMain();
        LoadingSceneController.LoadScene("Main");
    }
    public void GoStartScene()
    {
        GameManager.Instance.LoadStart();
        LoadingSceneController.LoadScene("Start");
    }
}
