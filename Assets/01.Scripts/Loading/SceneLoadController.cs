using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadController : MonoBehaviour
{
    public void GoMainScene()
    {
        LoadingSceneController.LoadScene("Main");
    }
    public void GoStartScene()
    {
        LoadingSceneController.LoadScene("Start");
    }
}
