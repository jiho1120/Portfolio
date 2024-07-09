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
    private void OnEnable()
    {
        GameManager.Instance.VisibleCursor();
        active = true;
    }
    private void OnDisable()
    {
        GameManager.Instance.LockedCursor();
        active = false;
    }
    public void onoff()
    {
        active = !active;
        if (active)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}
