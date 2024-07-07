using UnityEngine;


public class BasicPopUp : MonoBehaviour
{
    public bool active { get; private set; } = false;
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public void onoff()
    {
        active = !active;
        if (active)
        {
            Open();
            GameManager.Instance.VisibleCursor();
        }
        else
        {
            Close();
            GameManager.Instance.LockedCursor();
        }
    }
}
