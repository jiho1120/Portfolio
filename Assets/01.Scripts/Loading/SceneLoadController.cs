using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadController : Singleton<SceneLoadController>
{
    public void GoStartScene()
    {
        GameManager.Instance.AllScriptsCorReset();
        LoadingSceneController.LoadScene("Start");
        GameManager.Instance.LoadStartScene();
    }
    public void GoMainScene()
    {
        LoadingSceneController.LoadScene("Main");
        GameManager.Instance.LoadMain();
    }

    
}
