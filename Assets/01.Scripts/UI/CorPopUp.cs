using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CorPopUp : BasicPopUp
{
    Coroutine popUpCor = null;
    public Text txt;

    public void SetText(string text)
    {
        txt.text = text;
    }

    public virtual void PopCor(string text, float time)
    {
        if (popUpCor == null)
        {
            popUpCor = StartCoroutine(PopUpCor(text, time));
        }
    }
    IEnumerator PopUpCor(string text, float time)
    {
        SetText(text);
        yield return new WaitForSeconds(time);
        Close();
        popUpCor = null;
    }
}
