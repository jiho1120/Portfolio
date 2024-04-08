using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BasicPopUp : MonoBehaviour
{
    protected Coroutine popUpCor = null;
    public Text txt;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public void SetText(string text)
    {
        txt.text = text;
    }

    public virtual void PopCor(string text, float time)
    {
        if (popUpCor == null)
        {
            popUpCor = StartCoroutine(PopUpCor(text,time));
        }
    }
    IEnumerator PopUpCor(string text,float time)
    {
        SetText(text);
        yield return new WaitForSeconds(time);
        Close();
        popUpCor = null;
    }
}
