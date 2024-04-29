using UnityEngine;

public class Creature : MonoBehaviour
{
    public bool isDead { get; protected set; } = false;
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
}
