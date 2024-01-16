using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScene : MonoBehaviour
{
    public void GoMainScene()
    {
        LoadingSceneController.LoadScene("Main");
    }
}
